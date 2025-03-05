using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpell : BaseSpell
{
    public TimeEffects timeEffect;
    private TimeEffects currentTimeEffect;
    public GameObject enemySpawner;
    public List<GameObject> enemyList;

    public void Start()
    {
        enemySpawner = GameObject.FindWithTag("Enemy Spawner");
        enemyList = enemySpawner.GetComponent<EnemySpawner>().enemyList;
        WaitUtility.Wait(0.1f, () => {
                CastEffect();
            }
        );
    }

    public override void CastEffect()
    {
        foreach (GameObject enemy in enemyList)
        {
            if (enemy != null)
            {
                currentTimeEffect = Instantiate(timeEffect);
                currentTimeEffect.Slow(enemy.gameObject.GetComponent<Enemy>());
            }
        }
        Destroy(this.gameObject);
    }
}
