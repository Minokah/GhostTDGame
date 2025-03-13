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

    Boolean rewindEnabled = false;
    Boolean freezeEnabled = false;

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
                if (rewindEnabled == true)
                {
                    if ((enemy.gameObject.GetComponent<Enemy>().currentWaypointIndex - 1) >= 0)
                    {
                        enemy.gameObject.GetComponent<Enemy>().currentWaypointIndex = enemy.gameObject.GetComponent<Enemy>().currentWaypointIndex - 1;
                    }
                    else
                    {
                        enemy.gameObject.GetComponent<Enemy>().currentWaypointIndex = 0;
                    }
                }
                currentTimeEffect = Instantiate(timeEffect);
                currentTimeEffect.moreSlow = slowModifer;
                currentTimeEffect.moreDuration = timeModifier;
                currentTimeEffect.Slow(enemy.gameObject.GetComponent<Enemy>(), freezeEnabled);
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

    public void setRewind()
    {
        rewindEnabled = true;
    }

    public void setFreeze()
    {
        freezeEnabled = true;
    }
}
