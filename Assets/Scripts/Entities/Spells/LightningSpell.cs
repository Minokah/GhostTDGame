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

    public AudioClip fireSFX;

    public void Start()
    {
        WaitUtility.Wait(0.1f, () => {
                CastEffect();
            }
        );
    }

    public override void CastEffect()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (fireSFX != null)
            AudioManager.Instance.PlaySFX(fireSFX, 0.01f);
        if (stormEnabled == false || randomValue >= 0.20f)
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
