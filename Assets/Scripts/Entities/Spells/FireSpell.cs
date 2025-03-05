using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : BaseSpell
{
    public List<Enemy> enemyList = new List<Enemy>();
    public FireEffects fireEffect;
    private FireEffects currentFireEffect;


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
                currentFireEffect = Instantiate(fireEffect);
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
}
