using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTower : Entity
{

    public float tower_range;
    public float slow_rate = 0.5f;
    private List<Enemy> targets;
    public GameObject range_visual;
    private GameObject local_range;

    void Start()
    {
        targets = new List<Enemy>();

        if (null != range_collider)
        {
            range_collider.radius = tower_range;
            local_range = Instantiate(range_visual, transform.position, Quaternion.identity);
            local_range.transform.localScale = local_range.transform.localScale + new Vector3(2 * tower_range, 0.01f, 2 * tower_range);
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
                    bubbleScript.SetText("I'll slow them down");
                }
                else if (randomValue >= 0.33f)
                {
                    bubbleScript.SetText("I'll sap their strength");
                }
                else
                {
                    bubbleScript.SetText("They won't get past me so fast");
                }
            }
        }

    }

    void Update()
    {
        // debug show tower range
        for (float i = 0; 2 * Mathf.PI > i; i += 2 * Mathf.PI / 16)
        {
            // line between point, next point, red, and last for a frame
            Debug.DrawLine(transform.position + new Vector3(Mathf.Sin(i) * tower_range, 0.0f, Mathf.Cos(i) * tower_range),
                            transform.position + new Vector3(Mathf.Sin(i + 2 * Mathf.PI / 16) * tower_range, 0.0f, Mathf.Cos(i + 2 * Mathf.PI / 16) * tower_range),
                            Color.red,
                            Time.deltaTime);
        }
        if (specialUpgrade2 == true)
        {
            foreach (Enemy enemy in targets)
            {
                if (enemy != null)
                {
                    enemy.Damage(0.025f);
                }
            }
        }

        if (Input.GetKey(KeyCode.C))
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
        if (null != (attacker = other.GetComponent<Enemy>()))
        {
            targets.Add(attacker);
            float originalSpeed = attacker.speed;
            attacker.speed = attacker.speed * slow_rate;

            if (specialUpgrade1 == true)
            {
                attacker.speed = 0;
                StartCoroutine(stopFreeze(attacker, originalSpeed));
            }
        }
        return;
    }

    void OnTriggerExit(Collider other)
    {
        Enemy attacker;
        if (null != (attacker = other.GetComponent<Enemy>()))
        {
            // byebye, come again!
            targets.Remove(attacker);
            attacker.speed = attacker.speed / slow_rate;
        }
    }

    public void setEffectRange(float modifierValue)
    {
        tower_range = tower_range * (1 + modifierValue);
    }

    public void setSlowRate(float modifierValue)
    {
        slow_rate = slow_rate * (1 - modifierValue);
    }

    IEnumerator stopFreeze(Enemy enemy, float originalSpeed)
    {
        WaitUtility.Wait(1, () => {
            if (enemy != null)
            {
                enemy.speed = enemy.speed + originalSpeed;
            }
        }
        );
        yield return new WaitForSeconds(0);
    }

    private void OnDestroy()
    {
        Destroy(local_range);
    }
}
