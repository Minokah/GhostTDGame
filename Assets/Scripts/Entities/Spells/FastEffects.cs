using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEffects : MonoBehaviour
{
    public float originalSpeed;
    public void Fast(Enemy enemy, float fastRate)
    {
        if (enemy != null)
        {
            originalSpeed = enemy.speed;
            enemy.speed = enemy.speed * fastRate;
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
