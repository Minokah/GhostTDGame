using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : BaseSpell
{
    public List<Enemy> enemyList = new List<Enemy>();
    public FireEffects fireEffect;
    private FireEffects currentFireEffect;
    private float higherDamage = 0f;

    // for napalm upgrade
    Boolean napalmEnabled = false;
    public NapalmEffect napalm;
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
        if (napalmEnabled == true)
        {
            Instantiate(napalm, transform.position, Quaternion.identity);
        }
        if (fireSFX != null)
            AudioManager.Instance.PlaySFX(fireSFX, 0.01f);
        foreach (Enemy enemy in enemyList)
        {
            if (enemy != null)
            {
                currentFireEffect = Instantiate(fireEffect);
                currentFireEffect.damageModifier = higherDamage;
                currentFireEffect.Burn(enemy, this);
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

    public void setNapalm()
    {
        napalmEnabled = true;
    }
}
