using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTower : Entity
{

    public GameObject spawner;

    void Start()
    {

        Debug.Log(speechBubblePrefab);
        if (speechBubblePrefab != null)
        {
            // Calculate the spawn position with the offset
            Vector3 spawnPosition = transform.position + speechBubbleOffset;
            // Instantiate the speech bubble at that position
            GameObject bubble = Instantiate(speechBubblePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Bubble instantiated");
            // Optionally, if your SpeechBubble prefab has a script that allows setting text,
            // get that component and set the desired text.
            SpeechBubble bubbleScript = bubble.GetComponent<SpeechBubble>();
            if (bubbleScript != null)
            {
                //bubbleScript.SetText("Argh!"); // Or any text you wish
            }
        }
    }

    public void reduceSpawn(int reduceSpawnAmount)
    {
        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().reduceSpawnEffect(reduceSpawnAmount);
    }

    public void increaseBreaks(float longerBreakAmount)
    {
        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().longerBreakEffect(longerBreakAmount);
    }

    public void generalPatrol()
    {
        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().reduceSpawnEffect(2);
        spawner.GetComponent<EnemySpawner>().longerBreakEffect(0.375f);
    }

    public void specialPatrol()
    {
        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().setLessSpecialEnemies();
    }
}
