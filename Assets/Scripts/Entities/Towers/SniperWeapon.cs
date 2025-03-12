using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperWeapon : Weapon
{

    protected override void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            target.Damage(damage * (1 + extraDamage), source, this);
            Destroy(this.gameObject);
        }
    }
}
