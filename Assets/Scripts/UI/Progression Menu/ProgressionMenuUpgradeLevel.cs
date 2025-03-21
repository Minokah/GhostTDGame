using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuUpgradeLevel : MonoBehaviour
{
    CanvasGroup canvas;
    Image image;
    public TMP_Text amount;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    public void SetEquipped(bool active)
    {
        if (active)
        {
            image.color = new Color(0 / 255, 187f / 255, 255 / 255, 1);
        }
        else image.color = new Color(50f / 255, 50f / 255, 50f / 255, 1);
    }

    public void SetLocked(bool locked = true)
    {
        canvas.alpha = locked ? 0.3f : 1f;
        canvas.interactable = !locked;
        canvas.blocksRaycasts = !locked;
    }
}
