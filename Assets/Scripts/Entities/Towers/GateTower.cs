using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTower : Entity
{

    public GameObject spawner;

    void Start()
    {

        if (speechBubblePrefab != null)
        {
            // Calculate the spawn position with the offset
            Vector3 spawnPosition = transform.position + speechBubbleOffset;
            // Instantiate the speech bubble at that position
            GameObject bubble = Instantiate(speechBubblePrefab, spawnPosition, Quaternion.identity);
            // Optionally, if your SpeechBubble prefab has a script that allows setting text,
            // get that component and set the desired text.
            SpeechBubble bubbleScript = bubble.GetComponent<SpeechBubble>();
            if (bubbleScript != null)
            {
                float randomValue = UnityEngine.Random.Range(0f, 1f);
                if (randomValue >= 0.66f)
                {
                    bubbleScript.SetText("I'll try my best to cut those humans off");
                }
                else if (randomValue >= 0.33f)
                {
                    bubbleScript.SetText("So many humans, lets reduce that");
                }
                else
                {
                    bubbleScript.SetText("I'll delay them the best I can...");
                }
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
        spawner.GetComponent<EnemySpawner>().reduceSpawnEffect(5);
        spawner.GetComponent<EnemySpawner>().longerBreakEffect(0.35f);
    }

    public void specialPatrol()
    {
        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().setLessSpecialEnemies();
    }
}
