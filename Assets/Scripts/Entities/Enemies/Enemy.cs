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
        // as tons of basic enemies can spawn we noramlly do not want to spawn too many text bubbles
        //float randomValue= UnityEngine.Random.Range(0f, 1f);
        //if (speechBubblePrefab != null & randomValue >= 0.7f)
        //{
        //    //WaitUtility.Wait(3, () => {
        //            // Calculate the spawn position with the offset
        //            Vector3 spawnPosition = transform.position + speechBubbleOffset;
        //            // Instantiate the speech bubble at that position
        //            GameObject bubble = Instantiate(speechBubblePrefab, spawnPosition, Quaternion.identity);
        //
        //            // Optionally, if your SpeechBubble prefab has a script that allows setting text,
        //            // get that component and set the desired text.
        //            SpeechBubble bubbleScript = bubble.GetComponent<SpeechBubble>();
        //            if (bubbleScript != null)
        //            {
        //
        //                float randomDialogue = UnityEngine.Random.Range(0f, 1f);
        //                if (randomDialogue >= 0.66f)
        //                {
        //                    bubbleScript.SetText("Hey! A Free House!");
        //                }
        //                else if (randomDialogue >= 0.33f)
        //                {
        //                    bubbleScript.SetText("Wow, so many places to litter!");
        //                }
        //                else
        //                {
        //                    bubbleScript.SetText("Oohhh! Lets cut down a tree!");
        //                }
        //            }
        //    //}
        //    //);
        //}
    }

    public void EnemySpawnDialogueFast()
    {
        if (speechBubblePrefab != null)
        {
            //WaitUtility.Wait(3, () => {
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
            //}
            //);
        }
    }

    public void EnemySpawnDialogueTough()
    {
        if (speechBubblePrefab != null)
        {
            // if they spawn too early the dialogue will be outside the map
            //WaitUtility.Wait(3, () => {
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
            //}
            //);
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
                bubbleScript.SetText("OOF! I feel tired!");
            }
        }
    }

    public void EnemyFireDialogue()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (speechBubblePrefab != null & randomValue >= 0.7f)
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
                bubbleScript.SetText("FIRE! HOT HOT HOT!");
            }
        }
    }

    public void EnemyIceDialogue()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (speechBubblePrefab != null & randomValue >= 0.7f)
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
                bubbleScript.SetText("So... Cold!");
            }
        }
    }

    public void EnemyWindDialogue()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (speechBubblePrefab != null & randomValue >= 0.7f)
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
                bubbleScript.SetText("Wha?! Im Flying?!");
            }
        }
    }
}
