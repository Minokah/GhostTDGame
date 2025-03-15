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

    public float spawnInterval = 0.4f;     // Time between spawns
    public int maxSpawnCount = 50; // number of standard enemies spawned until level is over
    private int spawnCount = 0;
    public List<GameObject> enemyList = new List<GameObject>();

    // TODO: We will use this to check which level the player is on, and adjust spawn interval, max spawn count, and special enemy spawns accordingly
    public int level;
    // when replaying levels players can play harder challange mode to earn more upgrades and exp
    private int isChallangeMode;

    // will increase based on the selected level. In later levels more enemies spawn until a break occurs
    private float breakCounter;
    private int spawnsTillPause;
    private Boolean initialBreak = false;
    // Start paused for menus
    private Boolean paused = true;

    private float spawnTimer = 0f;

    // for the gateway tower to extend breaks between hordes
    private float breakTime = 1f;
    // for special gateway ability
    private Boolean lessSpecialEnemies = false;


    public void SetGameSpawner(int selectedLevel, int gameMode)
    {
        isChallangeMode = gameMode;
        level = selectedLevel;
        maxSpawnCount = 30 + 20 * level * isChallangeMode;
        spawnsTillPause = (int) Mathf.Floor(maxSpawnCount * 0.1f);
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
                paused = true;
                initialBreak = true;
                WaitUtility.Wait(10 + level, () => {
                    paused = false;
                }
                );
            }
            spawnTimer += Time.deltaTime;

            // default enemy spawning
            if (paused == false && spawnTimer >= spawnInterval && spawnCount < maxSpawnCount)
            {
                spawnTimer = 0f;
                SpawnEnemy();
                spawnCount++;
                if (lessSpecialEnemies == false)
                {
                    spawnRngEnemy(level);
                }
                else
                {
                    float randomValue = UnityEngine.Random.Range(0f, 1f);
                    if (randomValue >= 0.25f)
                    {
                        spawnRngEnemy(level);
                    }
                }
            }

            // we give the player some "breaks" between "waves" by pausing spawning for some time
            if (spawnCount == breakCounter)
            {
                if (breakCounter <= maxSpawnCount)
                {
                    breakCounter = breakCounter + spawnsTillPause;
                }
                //Debug.Log("Wait Time: " + (5 + level) * breakTime);
                StartCoroutine(PauseSpawning((5 + level) * breakTime));
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
            //if ()
            //{
                //bool allNull = myList.All(item => item == null);
            //}
        }
    }

    void SpawnEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<Enemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
        }
    }

    void SpawnToughEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(toughPrefab, waypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<Enemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogueTough();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
        }
    }

    void SpawnFastEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(fastPrefab, waypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<Enemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogueFast();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
        }
    }

    void SpawnMachoEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(machoPrefab, waypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<MachoEnemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
        }
    }

    void SpawnBalloonEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(balloonPrefab, waypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<BalloonEnemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
        }
    }

    void SpawnMixtapeEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(mixtapePrefab, waypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<MixtapeEnemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
        }
    }

    void SpawnCameraEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(cameraPrefab, waypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<CameraEnemy>();
        enemy_class.moneyManager = masterMoneyManager;
        enemy_class.EnemySpawnDialogue();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
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
		
		foreach (GameObject enemy in enemyList){
			if (enemy != null){
				Destroy(enemy);
			}	
		}
		
		enemyList.Clear();
	}	

    void spawnRngEnemy(int level)
    {
        // TODO add challange mode modifers that make the probability of tougher enemies more likely
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (level == 2)
        {
            if (randomValue < 0.85f)
            {

            }
            else if (randomValue >= 0.85f)
            {
                SpawnToughEnemy();
            }

        }
        else if (level == 3)
        {
            if (randomValue < 0.75f)
            {

            }
            else if (randomValue >= 0.75f)
            {
                SpawnToughEnemy();
            }

        }
        else if (level == 4)
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
        else if (level == 5)
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
        else if (level == 6)
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
        else if (level == 7)
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
        else if (level == 8)
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
        else if (level == 9)
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
        else if (level == 10)
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