using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // The red cylinder prefab
    public Transform[] waypoints;        // Waypoints from start to end
    public float spawnInterval = 0.5f;     // Time between spawns
    public int maxSpawnCount = 50;
    private int spawnCount = 0;
    public List<GameObject> enemyList = new List<GameObject>();

    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && spawnCount < maxSpawnCount)
        {
            spawnTimer = 0f;
            SpawnEnemy();
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