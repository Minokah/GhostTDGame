using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : BaseSpell
{
    public List<Enemy> enemyList = new List<Enemy>();
    public LightningEffects lightningEffect;
    private LightningEffects currentLightningEffect;


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
                currentLightningEffect = Instantiate(lightningEffect);
                currentLightningEffect.Bolt(enemy, this);
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
