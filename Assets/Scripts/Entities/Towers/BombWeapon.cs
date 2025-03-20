using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : Weapon
{
    Vector3 projectileTarget;
    private List<Entity> enemyList = new List<Entity>();
    public SphereCollider bombRadius;
    // for napalm upgrade
    public NapalmEffect napalm;

    public override void Init(Entity source_init, Entity target_init)
    {
        source = source_init;
        target = target_init;
        transform.position = source_init.gameObject.transform.position;
        transform.LookAt(target.gameObject.transform.position,Vector3.back);
        projectileTarget = target_init.gameObject.transform.position;
    }

    protected override void Update()
    {
        for (float i = 0; 2 * Mathf.PI > i; i += 2 * Mathf.PI / 16)
        {
            // line between point, next point, red, and last for a frame
            Debug.DrawLine(transform.position + new Vector3(Mathf.Sin(i) * bombRadius.radius, 0.0f, Mathf.Cos(i) * bombRadius.radius),
                transform.position + new Vector3(Mathf.Sin(i + 2 * Mathf.PI / 16) * bombRadius.radius, 0.0f, Mathf.Cos(i + 2 * Mathf.PI / 16) * bombRadius.radius),
                Color.red,
                Time.deltaTime);
        }

        transform.LookAt(projectileTarget,Vector3.back);

        if (special2 == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 10.0f);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 50.0f);
        }
            
        if (Vector3.Distance(transform.position, projectileTarget) < 0.1f)
        {
            if (special1 == true)
            {
                Instantiate(napalm, transform.position, Quaternion.identity);
            }

            foreach (Entity enemy in enemyList)
            {
                if (enemy != null)
                {
                    enemy.Damage(damage * (1 + extraDamage), source, this);
                }
            }
            Destroy(this.gameObject);
        }
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
        if (enemy.gameObject.tag == "Enemy") {
            enemyList.Remove(enemy.gameObject.GetComponent<Enemy>());
        }
    }
}
