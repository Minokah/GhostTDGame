using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    public float tower_range;
    public GameObject range_visual;
    public GameObject local_range;

    void Start()
    {
        local_range = Instantiate(range_visual, transform.position, Quaternion.identity);
        local_range.transform.localScale = local_range.transform.localScale + new Vector3(2* tower_range, 0.01f, 2 * tower_range);
    }

    void Update()
    {
        local_range.transform.position = transform.position;
    }

    public void setRange(float modifierValue)
    {
        tower_range = tower_range * (1 + modifierValue);
    }
}
