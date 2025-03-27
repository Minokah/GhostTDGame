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
            transform.position = target.gameObject.transform.position;
            if (special2 == false)
            {
                if (special1 == true)
                {
                    if (target != null & target.GetComponent<Enemy>().currentWaypointIndex > 0)
                    {
                        target.GetComponent<Enemy>().currentWaypointIndex = target.GetComponent<Enemy>().currentWaypointIndex - 1;
                    }
                }
                target.Damage(damage * (1 + extraDamage), source, this);
            }
            else
            {
                target.Damage(damage * (2 + extraDamage), source, this);
            }
            Destroy(this.gameObject);
        }
    }
}
