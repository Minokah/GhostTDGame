using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffects : MonoBehaviour
{
    public float damage = 5f;
    public float damageModifier;

    public void Burn(Enemy enemy, FireSpell spell)
    {
        if (enemy != null)
        {
            enemy.Damage(damage * (1f + damageModifier), spell);
        }
        Destroy(this.gameObject);
    }
}
