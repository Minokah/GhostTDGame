using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffects : MonoBehaviour
{
    public float damage = 10f;
    public float damageModifier;

    public void Bolt(Enemy enemy, LightningSpell spell)
    {
        if (enemy != null)
        {
            enemy.Damage(damage * (1f + damageModifier), spell);
        }
        Destroy(this.gameObject);
    }
}
