using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnemy : Enemy
{
    private GameObject spawner;
    void Start()
    {
        StartCoroutine(CameraLoop());
    }
    
    private IEnumerator CameraLoop()
    {
        Camera();
        Debug.Log("Camera running");
        yield return new WaitForSeconds(30);
    }

    private void Camera()
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
                bubbleScript.SetText("Hey Fans! Come out and Join Me!");
            }
        }

        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().cameraManEffect();
    }

    public override void EnemySpawnDialogue()
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
                    bubbleScript.SetText("Hey Youtube Whats Going On! Today Im checking out this haunted house!");
                }
            //}
            //);
        }
    }
}
