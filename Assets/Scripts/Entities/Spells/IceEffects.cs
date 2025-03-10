using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffects : MonoBehaviour
{
    public float originalSpeed;

    public void Slow(Enemy enemy, float slowRate)
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
