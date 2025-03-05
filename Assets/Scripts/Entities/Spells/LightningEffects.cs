using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffects : MonoBehaviour
{
    public float damage = 10f;

    public void Bolt(Enemy enemy, LightningSpell spell)
    {
        if (enemy != null)
        {
            enemy.Damage(damage, spell);
        }
        Destroy(this.gameObject);
    }
}
