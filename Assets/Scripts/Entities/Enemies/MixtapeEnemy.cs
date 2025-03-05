using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixtapeEnemy : Enemy
{
    private List<Entity> enemyList = new List<Entity>();
    public CapsuleCollider healRadius;
    public FastEffects fastEffect;
    private FastEffects currentFastEffect;
    public float fastRate = 1.5f;

    void Start()
    {
        healRadius = GetComponent<CapsuleCollider>();
        StartCoroutine(SpeedLoop());
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
    }

    private IEnumerator SpeedLoop()
    {
        Slow();
        Debug.Log("Mixtape running");
        yield return new WaitForSeconds(5);
    }

    private void Slow()
    {
        foreach (Entity enemy in enemyList)
        {
            if (enemy != null)
            {
                currentFastEffect = Instantiate(fastEffect);
                currentFastEffect.Fast(enemy.gameObject.GetComponent<Enemy>(), fastRate);
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
