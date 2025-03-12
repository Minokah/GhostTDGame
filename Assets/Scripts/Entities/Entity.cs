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

    private void Die()
    {
        // get money from the kill
        MoneyOnKill();

        // Before destroying the entity, show the speech bubble.
        Debug.Log(speechBubblePrefab);
        //&& Random.value < 0.1f
        if (speechBubblePrefab != null )
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
        if (entityId == 1)
        {
            Debug.Log("Enemy Killed");
            moneyManager.addMoney(1);
        }
        else if (entityId == 2)
        {
            moneyManager.addMoney(2);
        }
        else if (entityId == 3)
        {
            moneyManager.addMoney(4);
        }
        else if (entityId == 4)
        {
            moneyManager.addMoney(8);
        }
        else if (entityId == 5)
        {
            moneyManager.addMoney(16);
        }
        else if (entityId == 6)
        {
            moneyManager.addMoney(32);
        }
        else if (entityId == 7)
        {
            moneyManager.addMoney(64);
        }
    }
}
