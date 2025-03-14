using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffects : MonoBehaviour
{
    public float originalSpeed;

    public void Slow(Enemy enemy, float slowRate, Boolean freezeEnabled)
    {
        if (enemy != null & freezeEnabled == true)
        {
            enemy.EnemyIceDialogue();
            originalSpeed = enemy.speed;
            enemy.speed = 0;
            WaitUtility.Wait(5, () => {
                if (enemy != null)
                {
                    enemy.speed = enemy.speed + originalSpeed;
                }
            }
            );
        }
        else if (enemy != null)
        {
            enemy.EnemyIceDialogue();
            originalSpeed = enemy.speed;
            enemy.speed = enemy.speed * slowRate;
            WaitUtility.Wait(5, () => {
                    if (enemy != null)
                    {
                        enemy.speed = enemy.speed / slowRate;
                    }
                }
            );
        }
        Destroy(this.gameObject);
    }
}
