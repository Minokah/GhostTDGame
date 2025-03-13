using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEffects : MonoBehaviour
{
    public float slowRate = 0.95f;
    public float originalSpeed;
    public int duration = 5;
    public float moreSlow = 0f;
    public int moreDuration = 0;

    public void Slow(Enemy enemy, Boolean freeze)
    {

        if (enemy != null & freeze == true)
        {
            originalSpeed = enemy.speed;
            enemy.speed = 0;
            WaitUtility.Wait(1, () => {
                if (enemy != null)
                {
                    enemy.speed = originalSpeed;
                }
            }
            );
        }
        if (enemy != null)
        {
            originalSpeed = enemy.speed;
            enemy.speed = enemy.speed * (slowRate * (1f - moreSlow));
            WaitUtility.Wait((duration + moreDuration), () => {
                    if (enemy != null)
                    {
                        enemy.speed = originalSpeed;
                    }
                }
            );
        }
        Destroy(this.gameObject);
    }
}
