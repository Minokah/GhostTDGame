using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{

    public float tower_range;
    public enum TargPref {
                            First,
                            Last
                         }
    public TargPref targ_affinity;
    private List<Enemy> targets;
    private float target_strength;
    private bool attack_cooling;

    public GameObject range_visual;
    private GameObject local_range;

    IEnumerator WaitAndAttack(float time)
    {
        attack_cooling = true;
        //Debug.LogFormat("tower attacked!");

        // check if target has died
        // and remove from list if necessary
        if (null != targets[0]) {
            Attack(targets[0]);
        } else {
            targets.RemoveAt(0);
        }

        // wait and then allow next attack to start
        yield return new WaitForSeconds(time);
        attack_cooling = false;
    }

    void Start()
    {
        attack_cooling = false;
        targets = new List<Enemy>();

        if (null != range_collider) {
            range_collider.radius = tower_range;
            local_range = Instantiate(range_visual, transform.position, Quaternion.identity);
            local_range.transform.localScale = local_range.transform.localScale + new Vector3(2* tower_range, 0.01f, 2 * tower_range);
            local_range.SetActive(false);
        }

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
                    bubbleScript.SetText("Lets fight these humans!");
                }
                else if (randomValue >= 0.33f)
                {
                    bubbleScript.SetText("Ready to help out!");
                }
                else
                {
                    bubbleScript.SetText("We'll handle them!");
                }
            }
        }

    }

    void Update()
    {
        // debug show tower range
        for (float i = 0; 2*Mathf.PI > i; i+=2*Mathf.PI/16) {
            // line between point, next point, red, and last for a frame
            Debug.DrawLine( transform.position + new Vector3(Mathf.Sin(i) * tower_range, 0.0f, Mathf.Cos(i) * tower_range),
                            transform.position + new Vector3(Mathf.Sin(i+2*Mathf.PI/16) * tower_range, 0.0f, Mathf.Cos(i+2*Mathf.PI/16) * tower_range),
                            Color.red,
                            Time.deltaTime);
        }

        if (0 != targets.Count && false == attack_cooling) {
            StartCoroutine(WaitAndAttack(attack_cooldown));
        }

        if(Input.GetKey(KeyCode.C))
        {
            local_range.SetActive(true);
        }
        else
        {
            local_range.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy attacker;
        if (null != (attacker = other.GetComponent<Enemy>())) {

            // add to target queue as targeting policy dictates
            switch(targ_affinity) {
                case (TargPref.First):
                    targets.Add(attacker);
                    break;

                case (TargPref.Last):
                    targets.Insert(0, attacker);
                    break;
            }
        }
        return;
    }

    void OnTriggerExit(Collider other)
    {
        Enemy attacker;
        if (null != (attacker = other.GetComponent<Enemy>())) {
            // byebye, come again!
            targets.Remove(attacker);
        }
    }

    public void setAttackSpeed(float modifierValue)
    {
        attack_cooldown = attack_cooldown * (1 - modifierValue);
    }

    public void setAttackRange(float modifierValue)
    {
        tower_range = tower_range * (1 + modifierValue);
    }

    public void setHigherDamage(float modifierValue)
    {
        // this will be used in instantiated weapon
        higherDamage = higherDamage + modifierValue;
    }
}
