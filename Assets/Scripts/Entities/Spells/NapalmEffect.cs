using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NapalmEffect : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();
    public float damage = 0.025f;

    public void Start()
    {
        StartCoroutine(stopNapalm());
    }

    public void Update()
    {
        foreach (Enemy enemy in enemyList)
        {
            if (enemy != null)
            {
                enemy.EnemyFireDialogue();
                enemy.Damage(damage);
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

    IEnumerator stopNapalm()
    {
        WaitUtility.Wait(2, () => {
            Destroy(this.gameObject);
        }
        );
        yield return new WaitForSeconds(0);
    }
}
