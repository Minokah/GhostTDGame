using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneTower : Entity
{
    // To Handle the Arcane Tower Bonuses
    private GameObject spellManager;

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
                    bubbleScript.SetText("Want me to give you some pointers on spells?");
                }
                else if (randomValue >= 0.33f)
                {
                    bubbleScript.SetText("I'll help you out with your spells");
                }
                else
                {
                    bubbleScript.SetText("Need a Mana Boost?");
                }
            }
        }
    }

    public void cooldownEffect(float fasterCooldown){
        spellManager = GameObject.FindWithTag("Spell Manager");
        spellManager.GetComponent<SpellManager>().arcaneTowerCooldownEffect(fasterCooldown);
    }
    public void freeEffect(int moreFree)
    {
        spellManager = GameObject.FindWithTag("Spell Manager");
        spellManager.GetComponent<SpellManager>().arcaneTowerFreeEffect(moreFree);
    }
    public void eurekaEffect(Boolean eurekaOn)
    {
        spellManager = GameObject.FindWithTag("Spell Manager");
        spellManager.GetComponent<SpellManager>().arcaneTowerEurekaEffect(eurekaOn);
    }
}
