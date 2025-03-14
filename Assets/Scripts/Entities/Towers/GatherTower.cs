using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTower : Entity
{
    Boolean cooldown = false;
    public float pauseTime;
    public int money;
    Boolean riskyTrades = false;

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
                    bubbleScript.SetText("I'll start adding to your portfolio");
                }
                else if (randomValue >= 0.33f)
                {
                    bubbleScript.SetText("I can gurantee some good investments");
                }
                else
                {
                    bubbleScript.SetText("Lets get rich together!");
                }
            }
        }
    }

    void Update()
    {
        if (cooldown == false)
        {
            StartCoroutine(GenerateMoney());
        }
    }

    IEnumerator GenerateMoney()
    {
        cooldown = true;
        if (riskyTrades == false)
        {
            moneyManager.addMoney(money);
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
                        bubbleScript.SetText("Got some stock returns for you");
                    }
                }
            }
        }
        else
        {
            float randomValue = UnityEngine.Random.Range(0f, 1f);
            if (randomValue >= 0.5f)
            {
                moneyManager.addMoney(money + 5);
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
                        float randomDialogueChance = UnityEngine.Random.Range(0f, 1f);
                        if (randomDialogueChance >= 0.7f)
                        {
                            bubbleScript.SetText("Got some Great stock returns for you!");
                        }
                    }
                }
            }
            else
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
                        float randomDialogueChance = UnityEngine.Random.Range(0f, 1f);
                        if (randomDialogueChance >= 0.7f)
                        {
                            bubbleScript.SetText("Sorry, investments didn't pan out.");
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(pauseTime);
        cooldown = false;
    }

    public void moreMoneyGeneration(int moneyAmount)
    {
        money = money + moneyAmount;
    }

    public void fasterMoneyGeneration(float generationTime)
    {
        pauseTime = pauseTime - generationTime;
    }

    public void setRiskyTrades()
    {
        riskyTrades = true;
    }
}
