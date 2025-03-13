using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggableRange : MonoBehaviour
{
    private Boolean rangeEnabled = false;
    void OnMouseDown()
    {
        if (rangeEnabled == false)
        {
            rangeEnabled = true;
            gameObject.SetActive(true);
        }
        else
        {
            rangeEnabled = false;
            gameObject.SetActive(false);
        }
    }
}
