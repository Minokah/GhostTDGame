using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // The red cylinder prefab
    public GameObject toughPrefab;       // The tough enemy prefab
    public GameObject fastPrefab;       // The fast enemy prefab
    public GameObject machoPrefab;       // The macho enemy prefab
    public GameObject balloonPrefab;       // The balloon enemy prefab
    public GameObject mixtapePrefab;       // The mixtape enemy prefab
    public GameObject cameraPrefab;       // The camera enemy prefab
    public Transform[] waypoints;        // Waypoints from start to end
    public float spawnInterval = 0.25f;     // Time between spawns
    public int maxSpawnCount = 50; // number of standard enemies spawned until level is over
    private int spawnCount = 0;
    public List<GameObject> enemyList = new List<GameObject>();

    // We will use this to check which level the player is on, and adjust spawn interval, max spawn count, and special enemy spawns accordingly
    public int level = 0;

    private int breakcounter = 0;

    private float spawnTimer = 0f;

    void Update()
    {
        // we give the player some "breaks" between "waves" by pausing spawning for some time
        //if

        spawnTimer += Time.deltaTime;
        // default enemy spawning
        if (spawnTimer >= spawnInterval && spawnCount < maxSpawnCount)
        {
            spawnTimer = 0f;
            SpawnEnemy();
            //SpawnToughEnemy();
            //SpawnFastEnemy();
            //SpawnMachoEnemy();
            //SpawnMixtapeEnemy();
            //SpawnBalloonEnemy();
            //SpawnCameraEnemy();
            spawnCount++;
        }

    }

    void SpawnEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity);

        //keep track of enemies
        enemyList.Add(newEnemy);

        Enemy enemy_class = newEnemy.GetComponent<Enemy>();

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

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
        }
    }

    public void FunnelEffect(int reducedSpawn, float slowerSpawnRate)
    {
        maxSpawnCount = maxSpawnCount - reducedSpawn;
        spawnInterval = spawnInterval * slowerSpawnRate;
    }

    public void cameraManEffect()
    {
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
    }
}