using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffects : MonoBehaviour
{
    public int pushAmount = 1;

    public void Push(Enemy enemy)
    {
        if (enemy != null)
        {
            if (enemy.currentWaypointIndex - pushAmount >= 0)
            {
                enemy.currentWaypointIndex = enemy.currentWaypointIndex - pushAmount;
            }
            else
            {
                enemy.currentWaypointIndex = 0;
            }
        }
        Destroy(this.gameObject);
    }
}
