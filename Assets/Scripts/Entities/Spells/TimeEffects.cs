using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEffects : MonoBehaviour
{
    public float slowRate = 0.95f;
    public float originalSpeed;

    public void Slow(Enemy enemy)
    {
        if (enemy != null)
        {
            originalSpeed = enemy.speed;
            enemy.speed = enemy.speed * slowRate;
            WaitUtility.Wait(5, () => {
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
