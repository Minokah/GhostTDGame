using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonEnemy : Enemy
{
    void Start()
    {
        // balloon enemy will head towards random region of the map. At least 1/4 and at most 1/2
        currentWaypointIndex = Random.Range((int)Mathf.Ceil(waypoints.Length/4), (int)Mathf.Ceil(waypoints.Length/2));
    }

    public override void EnemySpawnDialogue()
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
                    bubbleScript.SetText("I LUV BLOOOOONS!!!");
                }
                else if (randomValue >= 0.33f)
                {
                    bubbleScript.SetText("ME FLY!");
                }
                else
                {
                    bubbleScript.SetText("WHEEEE! I FLY!!!");
                }
            }
        }
    }
}
