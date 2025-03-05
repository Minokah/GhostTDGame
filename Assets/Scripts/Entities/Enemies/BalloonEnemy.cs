using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonEnemy : Enemy
{
    void Start()
    {
        // balloon enemy will head towards random region of the map. At least 1/4 and at most 1/2
        currentWaypointIndex = Random.Range((int)Mathf.Ceil(waypoints.Length/4), (int)Mathf.Ceil(waypoints.Length/2));
    }
}
