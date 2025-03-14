using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Tooltip("Waypoints in the order the enemy should follow")]
    public Transform[] waypoints;

    public float speed = 2f;
    public int currentWaypointIndex = 0;

    // to stop enemies from being completly frozen by repedeted freezing effects
    public Boolean chilledFlag = false;

    protected virtual void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If close enough to waypoint, move to next
        if (direction.magnitude <= distanceThisFrame)
        {
            // Snap to waypoint
            transform.position = targetWaypoint.position;
            currentWaypointIndex++;

            // If we've reached the last waypoint, do something (e.g., destroy or deal damage)
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Reached end - destroy for now
                Destroy(gameObject);
            }
        }
        else
        {
            // Move forward
            //Debug.Log("direction.normalized" + direction.normalized);
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }
    }

    public virtual void EnemySpawnDialogue()
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
                // as tons of basic enemies can spawn we noramlly do not want to spawn too many text bubbles
                float randomValue = UnityEngine.Random.Range(0f, 1f);
                if (randomValue >= 0.7f)
                {
                    bubbleScript.SetText("Lookie There! A Free House!");
                }
                else if (randomValue >= 0.8f)
                {
                    bubbleScript.SetText("Wow, so much green space to litter!");
                }
                else if (randomValue >= 0.9f)
                {
                    bubbleScript.SetText("Oohhh! I want to cut down a tree!");
                }
            }
        }
    }

    public void EnemySpawnDialogueFast()
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
                bubbleScript.SetText("SPEED SPEED SPEED!!!!");
            }
        }
    }

    public void EnemySpawnDialogueTough()
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
                bubbleScript.SetText("Do you even lift BROOO!?");
            }
        }
    }

    public void EnemyStunRoundDialogue()
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
                bubbleScript.SetText("OOF! That knocked the wind out of me!");
            }
        }
    }

    public void EnemyFireDialogue()
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
                if (randomValue >= 0.7f)
                {
                    bubbleScript.SetText("FIRE! HOT HOT HOT!");
                }
            }
        }
    }

    public void EnemyIceDialogue()
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
                if (randomValue >= 0.7f)
                {
                    bubbleScript.SetText("So... Cold!");
                }
            }
        }
    }

    public void EnemyWindDialogue()
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
                if (randomValue >= 0.7f)
                {
                    bubbleScript.SetText("Wha?! Im Flying?!");
                }
            }
        }
    }
}
