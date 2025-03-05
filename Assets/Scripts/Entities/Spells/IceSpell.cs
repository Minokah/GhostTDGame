using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : BaseSpell
{
    public List<Enemy> enemyList = new List<Enemy>();
    public IceEffects iceEffect;
    private IceEffects currentIceEffect;
    public float slowRate = 0.75f;

    public void Start()
    {
        WaitUtility.Wait(0.1f, () => {
                CastEffect();
            }
        );
    }

    public override void CastEffect()
    {
        foreach (Enemy enemy in enemyList)
        {
            if (enemy != null)
            {
                currentIceEffect = Instantiate(iceEffect);
                currentIceEffect.Slow(enemy, slowRate);
            }
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            enemyList.Add(enemy.gameObject.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit(Collider enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            enemyList.Remove(enemy.gameObject.GetComponent<Enemy>());
        }
    }
}
