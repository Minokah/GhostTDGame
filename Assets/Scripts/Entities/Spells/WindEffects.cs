using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffects : MonoBehaviour
{
    public int pushAmount = 1;
    public int extraAmount = 0;

    public void Push(Enemy enemy, Boolean kickback)
    {
        if (enemy != null & kickback == true)
        {
            if (enemy.currentWaypointIndex >= (int)(enemy.waypoints.Length * 3 / 4))
            {
                enemy.Damage(8f);
            }
        }

        if (enemy != null)
        {
            if (enemy.currentWaypointIndex - (pushAmount + extraAmount) >= 0)
            {
                enemy.currentWaypointIndex = enemy.currentWaypointIndex - (pushAmount + extraAmount);
            }
            else
            {
                enemy.currentWaypointIndex = 0;
            }
        }
        Destroy(this.gameObject);
    }
}
