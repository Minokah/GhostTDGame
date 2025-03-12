using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTower : Entity
{
    Boolean cooldown = false;
    public float pauseTime;
    public int money;

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
        moneyManager.addMoney(money);
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
}
