using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVisible : MonoBehaviour
{
    CanvasGroup canvas;
    public bool visible = false;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        if (!visible) Hide();
    }

    void Update()
    {
        if (visible) {
            if (canvas.alpha < 1f) canvas.alpha += 5 * Time.deltaTime;
        }
    }

    public void Show(float amount = 0f)
    {
        if (amount > 0f) canvas.alpha = amount;
        visible = true;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void Hide(float amount = 0f)
    {
        visible = false;
        canvas.alpha = amount;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
