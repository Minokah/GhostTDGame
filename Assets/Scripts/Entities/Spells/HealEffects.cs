using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffects : MonoBehaviour
{
    public void Heal(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.health = enemy.health + 5;
        }
        Destroy(this.gameObject);
    }
}
