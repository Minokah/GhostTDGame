using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public StatisticsManager masterMoneyManager;
    public GameObject enemyPrefab;       // The red cylinder prefab
    public GameObject toughPrefab;       // The tough enemy prefab
    public GameObject fastPrefab;       // The fast enemy prefab
    public GameObject machoPrefab;       // The macho enemy prefab
    public GameObject balloonPrefab;       // The balloon enemy prefab
    public GameObject mixtapePrefab;       // The mixtape enemy prefab
    public GameObject cameraPrefab;       // The camera enemy prefab

    public Transform[] waypoints;        // Waypoints from start to end
	public Transform[] waypointsAlternate;        // Alternate set of waypoints

    public float spawnInterval = 0.4f;     // Time between spawns
    public int maxSpawnCount = 50; // number of standard enemies spawned until level is over
    public int spawnCount = 0;
    public List<GameObject> enemyList = new List<GameObject>();

    // TODO: We will use these to check which level the player is on, and adjust spawn interval, max spawn count, and special enemy spawns accordingly
    public int level;
	public int map;

    // will increase based on the selected level. In later levels more enemies spawn until a break occurs
    private float breakCounter;
    private int spawnsTillPause;
    private Boolean initialBreak = false;
    // Start paused for menus
    private Boolean paused = true;

    private float spawnTimer = 0f;
	
	public GoodGameOver victoryObject;

    // for the gateway tower to extend breaks between hordes
    private float breakTime = 1f;
    // for special gateway ability
    private Boolean lessSpecialEnemies = false;


    public void SetGameSpawner(int selectedMap, int selectedLevel)
    {
        level = selectedLevel;
		map = selectedMap;
        maxSpawnCount = 50 + selectedMap * 60 + 30 * level;
        spawnsTillPause = (int) Mathf.Floor(maxSpawnCount * 0.05f);
        breakCounter = spawnsTillPause;
    }

    public void SetGameState(bool active)
    {
        paused = !active;
    }

    IEnumerator PauseSpawning(float pauseAmount)
    {
        paused = true;
        yield return new WaitForSeconds(pauseAmount);
        paused = false;
        //Debug.Log("Done Waiting");
    }

    void Update()
    {
        if (paused == false)
        {
            // give the player some time to build defenses
            if (initialBreak == false)
            {
                initialBreak = true;
				StartCoroutine(PauseSpawning((10 + 5 * level)));
            }
            spawnTimer += Time.deltaTime;

            // default enemy spawning
            if (paused == false && spawnTimer >= spawnInterval && spawnCount < maxSpawnCount)
            {
                spawnTimer = 0f;
                SpawnEnemy();
                spawnCount++;
				if (spawnCount > (int)(maxSpawnCount/3)){ //start spawning special enemies once enough basic enemies have appeared
					if (lessSpecialEnemies == false)
					{
						spawnRngEnemy(map, level);
					}
					else
					{
						float randomValue = UnityEngine.Random.Range(0f, 1f);
						if (randomValue >= 0.25f)
						{
							spawnRngEnemy(map, level);
						}
					}
				}
            }

            // we give the player some "breaks" between "waves" by pausing spawning for some time
            // while at the same time ramping up the number of spawns over time between breaks
            if (spawnCount == breakCounter)
            {
                if (breakCounter <= maxSpawnCount)
                {
                    breakCounter = breakCounter + spawnsTillPause + 5;
                }
                StartCoroutine(PauseSpawning((10 + level + map) * breakTime));
            }
            // no need to run this loop anymore after all enemies that can spawn have been spawned
            if (spawnCount == maxSpawnCount)
            {
                paused = true;
            }
        }
        //start checking that all enemies have been killed after all enemies have been spawned
        else if (spawnCount >= maxSpawnCount)
        {
            bool allNull = enemyList.All(item => item == null);
			if (allNull == true){
				victoryObject.WonLevel();
			}	
        }
    }

    void SpawnEnemy()
    {
		Transform[] selectedWaypoints;
		float randomValue = UnityEngine.Random.Range(0f, 1f);
		if (randomValue >= 0.5f){
			selectedWaypoints = waypoints;
		}	
		else{
			selectedWaypoints = waypointsAlternate;
		}
		
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(enemyPrefab, selectedWaypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<Enemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
			enemy_class.waypoints = selectedWaypoints;
        }
    }

    void SpawnToughEnemy()
    {
		Transform[] selectedWaypoints;
		float randomValue = UnityEngine.Random.Range(0f, 1f);
		if (randomValue >= 0.5f){
			selectedWaypoints = waypoints;
		}	
		else{
			selectedWaypoints = waypointsAlternate;
		}
			
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(toughPrefab, selectedWaypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<Enemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogueTough();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
			enemy_class.waypoints = selectedWaypoints;
        }
    }

    void SpawnFastEnemy()
    {
		Transform[] selectedWaypoints;
		float randomValue = UnityEngine.Random.Range(0f, 1f);
		if (randomValue >= 0.5f){
			selectedWaypoints = waypoints;
		}	
		else{
			selectedWaypoints = waypointsAlternate;
		}
		
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(fastPrefab, selectedWaypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<Enemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogueFast();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
			enemy_class.waypoints = selectedWaypoints;
        }
    }

    void SpawnMachoEnemy()
    {
		Transform[] selectedWaypoints;
		float randomValue = UnityEngine.Random.Range(0f, 1f);
		if (randomValue >= 0.5f){
			selectedWaypoints = waypoints;
		}	
		else{
			selectedWaypoints = waypointsAlternate;
		}
		
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(machoPrefab, selectedWaypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<MachoEnemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
			enemy_class.waypoints = selectedWaypoints;
        }
    }

    void SpawnBalloonEnemy()
    {
		Transform[] selectedWaypoints;
		float randomValue = UnityEngine.Random.Range(0f, 1f);
		if (randomValue >= 0.5f){
			selectedWaypoints = waypoints;
		}	
		else{
			selectedWaypoints = waypointsAlternate;
		}
		
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(balloonPrefab, selectedWaypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<BalloonEnemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
			enemy_class.waypoints = selectedWaypoints;
        }
    }

    void SpawnMixtapeEnemy()
    {
		Transform[] selectedWaypoints;
		float randomValue = UnityEngine.Random.Range(0f, 1f);
		if (randomValue >= 0.5f){
			selectedWaypoints = waypoints;
		}	
		else{
			selectedWaypoints = waypointsAlternate;
		}
		
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(mixtapePrefab, selectedWaypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<MixtapeEnemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
			enemy_class.waypoints = selectedWaypoints;
        }
    }

    void SpawnCameraEnemy()
    {
		Transform[] selectedWaypoints;
		float randomValue = UnityEngine.Random.Range(0f, 1f);
		if (randomValue >= 0.5f){
			selectedWaypoints = waypoints;
		}	
		else{
			selectedWaypoints = waypointsAlternate;
		}
		
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(cameraPrefab, selectedWaypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<CameraEnemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
			enemy_class.waypoints = selectedWaypoints;
        }
    }

    public void reduceSpawnEffect(int reducedSpawn)
    {
        maxSpawnCount = maxSpawnCount - reducedSpawn;
    }

    public void longerBreakEffect(float slowerSpawnRate)
    {
        breakTime = breakTime + slowerSpawnRate;
    }

    public void setLessSpecialEnemies()
    {
        lessSpecialEnemies = true;
    }

    public void cameraManEffect()
    {
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
    }
	
	public void resetEnemySpawner(){
		StopAllCoroutines();
		
		spawnTimer = 0f;
		breakTime = 1f;
		lessSpecialEnemies = false;	
		spawnCount = 0;
		
		paused = true;
		initialBreak = false;
		
		foreach (GameObject enemy in enemyList){
			if (enemy != null){
				Destroy(enemy);
			}	
		}
		
		enemyList.Clear();
	}	

    void spawnRngEnemy(int map, int level)
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (map == 0 && level == 1)
        {
            if (randomValue < 0.85f)
            {

            }
            else if (randomValue >= 0.85f)
            {
                SpawnToughEnemy();
            }

        }
        else if (map == 1 && level == 0)
        {
            if (randomValue < 0.75f)
            {

            }
            else if (randomValue >= 0.75f)
            {
                SpawnToughEnemy();
            }

        }
        else if (map == 1 && level == 1)
        {
            if (randomValue < 0.65f)
            {

            }
            else if (randomValue >= 0.65f && randomValue < 0.8f)
            {
                SpawnToughEnemy();
            }
            else
            {
                SpawnFastEnemy();
            }
        }
        else if (map == 1 && level == 2)
        {
            if (randomValue < 0.65f)
            {

            }
            else if (randomValue >= 0.65f && randomValue < 0.75f)
            {
                SpawnToughEnemy();
            }
            else if (randomValue >= 0.75f && randomValue < 0.9f)
            {
                SpawnFastEnemy();
            }
            else
            {
                SpawnMachoEnemy();
            }
        }
        else if (map == 2 && level == 0)
        {
            if (randomValue < 0.55f)
            {

            }
            else if (randomValue >= 0.55f && randomValue < 0.6f)
            {
                SpawnToughEnemy();
            }
            else if (randomValue >= 0.7f && randomValue < 0.8f)
            {
                SpawnFastEnemy();
            }
            else if (randomValue >= 0.8f && randomValue < 0.9f)
            {
                SpawnBalloonEnemy();
            }
            else
            {
                SpawnMachoEnemy();
            }
        }
        else if (map == 2 && level == 1)
        {
            if (randomValue < 0.55f)
            {

            }
            else if (randomValue >= 0.55f && randomValue < 0.60f)
            {
                SpawnToughEnemy();
            }
            else if (randomValue >= 0.60f && randomValue < 0.75f)
            {
                SpawnFastEnemy();
            }
            else if (randomValue >= 0.75f && randomValue < 0.90f)
            {
                SpawnBalloonEnemy();
            }
            else
            {
                SpawnMachoEnemy();
            }
        }
        else if (map == 2 && level == 2)
        {
            if (randomValue < 0.55f)
            {

            }
            else if (randomValue >= 0.55f && randomValue < 0.65f)
            {
                SpawnToughEnemy();
            }
            else if (randomValue >= 0.65f && randomValue < 0.7f)
            {
                SpawnFastEnemy();
            }
            else if (randomValue >= 0.7f && randomValue < 0.85f)
            {
                SpawnBalloonEnemy();
            }
            else if (randomValue >= 0.85f && randomValue < 0.9f)
            {
                SpawnMixtapeEnemy();
            }
            else
            {
                SpawnMachoEnemy();
            }

        }
        else if (map == 3 && level == 0)
        {
            if (randomValue < 0.45f)
            {

            }
            else if (randomValue >= 0.45f && randomValue < 0.55f)
            {
                SpawnToughEnemy();
            }
            else if (randomValue >= 0.55f && randomValue < 0.65f)
            {
                SpawnFastEnemy();
            }
            else if (randomValue >= 0.65f && randomValue < 0.70f)
            {
                SpawnBalloonEnemy();
            }
            else if (randomValue >= 0.70f && randomValue < 0.80f)
            {
                SpawnMixtapeEnemy();
            }
            else if (randomValue >= 0.80f && randomValue < 0.90f)
            {
                SpawnCameraEnemy();
            }
            else
            {
                SpawnMachoEnemy();
            }

        }
        else if (map == 3 && level == 1)
        {
            if (randomValue < 0.35f)
            {

            }
            else if (randomValue >= 0.35f && randomValue < 0.40f)
            {
                SpawnToughEnemy();
            }
            else if (randomValue >= 0.40f && randomValue < 0.45f)
            {
                SpawnFastEnemy();
            }
            else if (randomValue >= 0.45f && randomValue < 0.5f)
            {
                SpawnBalloonEnemy();
            }
            else if (randomValue >= 0.5f && randomValue < 0.65f)
            {
                SpawnMixtapeEnemy();
            }
            else if (randomValue >= 0.65f && randomValue < 0.85f)
            {
                SpawnCameraEnemy();
            }
            else
            {
                SpawnMachoEnemy();
            }

        }
    }
}