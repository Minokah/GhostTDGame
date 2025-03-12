using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Entity source;
    protected Entity target;
    public float damage;
    public float extraDamage;
    public Boolean special1 = false;
    public Boolean special2 = false;
    public virtual void Init(Entity source_init, Entity target_init)
    {
        source = source_init;
        target = target_init;
        transform.position = source_init.gameObject.transform.position;
        transform.LookAt(target.gameObject.transform.position,
                         Vector3.back);

    }

    protected virtual void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.LookAt(target.gameObject.transform.position,
             Vector3.back);
            if (special1 == false)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 30.0f);
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 90.0f);
            }

            if (Vector3.Distance(transform.position,
                                 target.gameObject.transform.position)
                < 0.1f)
            {
                float stunChance = UnityEngine.Random.Range(0f, 1f);
                if (stunChance >= 0.95f)
                {
                    StartCoroutine(Stun(target.GetComponent<Enemy>()));
                }
                target.Damage(damage * (1 + extraDamage), source, this);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Stun(Enemy enemy)
    {
        if (enemy != null)
        {
            //float originalSpeed = enemy.speed;
            enemy.speed = enemy.speed * 0.5f;
            //WaitUtility.Wait(5, () => {
            //    if (enemy != null)
            //    {
            //        enemy.speed = originalSpeed;
            //    }
            //}
            //);
        }
        yield return new WaitForSeconds(0);
    }
}