using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachoEnemy : Enemy
{
    private List<Entity> enemyList = new List<Entity>();
    public CapsuleCollider healRadius;
    public HealEffects healEffect;
    private HealEffects currentHealEffect;
    private bool cooldown;

    void Start()
    {
        cooldown = false;
        healRadius = GetComponent<CapsuleCollider>();
    }
    
    protected override void Update()
    {
        base.Update();
        for (float i = 0; 2 * Mathf.PI > i; i += 2 * Mathf.PI / 16)
        {
            // line between point, next point, red, and last for a frame
            Debug.DrawLine(transform.position + new Vector3(Mathf.Sin(i) * healRadius.radius, 0.0f, Mathf.Cos(i) * healRadius.radius),
                transform.position + new Vector3(Mathf.Sin(i + 2 * Mathf.PI / 16) * healRadius.radius, 0.0f, Mathf.Cos(i + 2 * Mathf.PI / 16) * healRadius.radius),
                Color.red,
                Time.deltaTime);
        }
        if (false == cooldown)
        {
            StartCoroutine(HealingLoop());
        }
    }

    private IEnumerator HealingLoop()
    {
        cooldown = true;
        Heal();
        //Debug.Log("Healing running");
        yield return new WaitForSeconds(10);
        cooldown = false;
    }

    private void Heal()
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
                bubbleScript.SetText("Just Tough It Out People!");
            }
        }
        foreach (Entity enemy in enemyList)
        {
            if (enemy != null)
            {
                currentHealEffect = Instantiate(healEffect);
                currentHealEffect.Heal(enemy.gameObject.GetComponent<Enemy>());
            }
        }
    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            enemyList.Add(enemy.gameObject.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit(Collider enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            enemyList.Remove(enemy.gameObject.GetComponent<Enemy>());
        }
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
                    bubbleScript.SetText("No Pain No Gain!");
                }
            //}
            //);
        }
    }
}
