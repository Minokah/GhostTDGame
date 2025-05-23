using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Weapon weapon;
    public CapsuleCollider range_collider;
    public float attack_cooldown;
    public float health;

    // connection to the money mangment object
    public StatisticsManager moneyManager;

    // reference id
    public int entityId;

    // Reference to the SpeechBubble prefab. 
    // Assign this in the Inspector.
    public GameObject speechBubblePrefab;
    // An offset to position the bubble (e.g., above the entity)
    public Vector3 speechBubbleOffset = new Vector3(0, 2f, 0);

    public float higherDamage = 1f;
    public Boolean specialUpgrade1 = false;
    public Boolean specialUpgrade2 = false;

    public AudioClip deathSFX;


    public void Damage(float damage, Entity attacker, Weapon damager)
    {
        Debug.LogFormat(this + " hit by " + attacker);
        health -= damage;
        // take damage, provided context for attacker
        if (0 >= health) {
            Die();

        }
    }

    // for spells
    public void Damage(float damage, BaseSpell attacker)
    {
        Debug.LogFormat(this + " hit by " + attacker);
        health -= damage;
        // take damage, provided context for attacker
        if (0 >= health)
        {
            Die();
        }
    }

    // For misc effects
    public void Damage(float damage)
    {
        health -= damage;
        // take damage, provided context for attacker
        if (0 >= health)
        {
            Die();
        }
    }

    private void Die()
    {
        // get money from the kill
        MoneyOnKill();

        // Before destroying the entity, show the speech bubble.
        //Debug.Log(speechBubblePrefab);
        //&& Random.value < 0.1f

        if (deathSFX != null && UnityEngine.Random.value < 0.5f)
        {
            AudioManager.Instance.PlaySFX(deathSFX, 0.01f);
        }

        if (speechBubblePrefab != null )
        {
            float deathmessagechance = UnityEngine.Random.Range(0f, 1f);
            if (deathmessagechance >= 0.7f)
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
                        bubbleScript.SetText("Argh!");
                    }
                    else if (randomValue >= 0.33f)
                    {
                        bubbleScript.SetText("Ouch!");
                    }
                    else
                    {
                        bubbleScript.SetText("Aie!");
                    }
                }
            }
        }
        
        // Finally, destroy the entity
        Destroy(this.gameObject);
    
    }

    protected virtual void AttackAnimation()
    {
        // do nothing for base Entity class
        // override for subclasses with animations
    }

    protected void Attack(Entity target)
    {
        Weapon weapon_instance;

        AttackAnimation();

        weapon_instance = Instantiate(weapon);
        weapon_instance.Init(this, target);
        weapon_instance.extraDamage = higherDamage;
        weapon_instance.special1 = specialUpgrade1;
        weapon_instance.special2 = specialUpgrade2;
    }

    protected void MoneyOnKill()
    {
        moneyManager.IncrementStatistics("totalKills");
        
        if (entityId == 1) // Basic enemy
        {
            moneyManager.IncrementStatistics("basicKills");
            moneyManager.addMoney(1);
        }
        else if (entityId == 2) // Tough
        {
            moneyManager.IncrementStatistics("toughKills");
            moneyManager.addMoney(2);
        }
        else if (entityId == 3) // Fast
        {
            moneyManager.IncrementStatistics("fastKills");
            moneyManager.addMoney(2);
        }
        else if (entityId == 4) // Macho
        {
            moneyManager.IncrementStatistics("machoKills");
            moneyManager.addMoney(2);
        }
        else if (entityId == 5) // Balloon
        {
            moneyManager.IncrementStatistics("balloonKills");
            moneyManager.addMoney(2);
        }
        else if (entityId == 6) // Mixtape
        {
            moneyManager.IncrementStatistics("mixtapeKills");
            moneyManager.addMoney(2);
        }
        else if (entityId == 7) // Camera
        {
            moneyManager.IncrementStatistics("cameraKills");
            moneyManager.addMoney(2);
        }
    }

    public void setSpecialOne()
    {
        specialUpgrade1 = true;
    }
    public void setSpecialTwo()
    {
        specialUpgrade2 = true;
    }
}
