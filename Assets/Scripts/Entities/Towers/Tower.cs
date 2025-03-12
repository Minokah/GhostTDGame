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
        }

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
        attack_cooldown = attack_cooldown * (1 + modifierValue);
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
