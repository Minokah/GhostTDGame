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
    private float slowModifer = 0f;
    private int timeModifier = 0;

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
                currentTimeEffect.moreSlow = slowModifer;
                currentTimeEffect.moreDuration = timeModifier;
                currentTimeEffect.Slow(enemy.gameObject.GetComponent<Enemy>());
            }
        }
        Destroy(this.gameObject);
    }

    public void setMoreSlow(float modifier)
    {
        slowModifer = slowModifer + modifier;
    }

    public void setHigherDuration(int modifier)
    {
        timeModifier = timeModifier + modifier;
    }
}
