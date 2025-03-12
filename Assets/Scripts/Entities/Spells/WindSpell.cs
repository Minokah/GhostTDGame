using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpell : BaseSpell
{
    public List<Enemy> enemyList = new List<Enemy>();
    public WindEffects windEffect;
    private WindEffects currentWindEffect;
    private int moreKnockback = 0;

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
                currentWindEffect = Instantiate(windEffect);
                currentWindEffect.extraAmount = moreKnockback;
                currentWindEffect.Push(enemy);
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

    public void setHigherKnockback(int modifier)
    {
        moreKnockback = moreKnockback + modifier;
    }
}
