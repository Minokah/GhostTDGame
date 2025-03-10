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
        Debug.Log("Healing running");
        yield return new WaitForSeconds(10);
        cooldown = false;
    }

    private void Heal()
    {
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
}
