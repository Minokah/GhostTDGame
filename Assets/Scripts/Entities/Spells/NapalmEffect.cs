using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NapalmEffect : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();
    public float damage = 0.5f;
    private bool cooldown;

    public void Start()
    {
        StartCoroutine(stopNapalm());
        cooldown = false;
    }

    public void Update()
    {
        if (false == cooldown)
        {
            StartCoroutine(NapalmLoop());
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

    IEnumerator stopNapalm()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    IEnumerator NapalmLoop()
    {
        cooldown = true;

        foreach (Enemy enemy in enemyList)
        {
            if (enemy != null)
            {
                enemy.EnemyFireDialogue();
                enemy.Damage(damage);
            }
        }

        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }
}
