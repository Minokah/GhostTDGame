using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpell : MonoBehaviour
{
    public int spellId;
    private float higherRadius = 1f;
     

    public virtual void CastEffect()
    {
    
    }

    public void setHigherRadius(float modifier)
    {
        higherRadius = higherRadius + modifier;

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        float radius = sphereCollider.radius;
        float newScale = radius * higherRadius * transform.localScale.x * 2;
        transform.localScale = new Vector3(newScale, 0.01f, newScale);
    }
}
