using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillEffect : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();
    public float slow = 0.75f;

    public void Start()
    {
        StartCoroutine(stopChill());
    }

    public void Update()
    {
        foreach (Enemy enemy in enemyList)
        {
            if (enemy != null & enemy.chilledFlag == false)
            {
                StartCoroutine(applyChill(enemy));
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

    IEnumerator stopChill()
    {
        WaitUtility.Wait(2, () => {
            Destroy(this.gameObject);
        }
        );
        yield return new WaitForSeconds(0);
    }

    IEnumerator applyChill(Enemy target)
    {
        if (target != null)
        {
            target.chilledFlag = true;
            float originalSpeed = target.speed;
            target.speed = target.speed * 0.75f;
            WaitUtility.Wait(5, () => {
                if (target != null)
                {
                    target.speed = target.speed / 0.75f;
                    target.chilledFlag = false;
                }
            }
            );
        }
        yield return new WaitForSeconds(0);
    }
}
