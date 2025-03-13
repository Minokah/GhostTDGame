using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : BaseSpell
{
    public List<Enemy> enemyList = new List<Enemy>();
    public LightningEffects lightningEffect;
    private LightningEffects currentLightningEffect;
    private float higherDamage = 0f;

    Boolean stormEnabled = false;
    public List<GameObject> allEnemyList;

    public void Start()
    {
        GameObject enemySpawner = GameObject.FindWithTag("Enemy Spawner");
        allEnemyList = enemySpawner.GetComponent<EnemySpawner>().enemyList;
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
                currentLightningEffect = Instantiate(lightningEffect);
                currentLightningEffect.damageModifier = higherDamage;
                currentLightningEffect.Bolt(enemy, this);
            }
        }
        if (stormEnabled == true)
        {
            Enemy lastEnemy = enemyList[enemyList.Count-1];
            if (lastEnemy != null)
            {
                currentLightningEffect = Instantiate(lightningEffect);
                currentLightningEffect.damageModifier = higherDamage;
                currentLightningEffect.Bolt(lastEnemy, this);
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

    public void setHigherDamage(float modifier)
    {
        higherDamage = higherDamage + modifier;
    }

    public void setStorm()
    {
        stormEnabled = true;
    }
}
