using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuSpecialty : MonoBehaviour
{
    public Button button;
    public TMP_Text name;
    public RawImage icon;
    RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
    }

    public void SetEquipped(bool equipped)
    {
        if (equipped) image.texture = Resources.Load<Texture>("Buttons/Button Blue");
        else image.texture = Resources.Load<Texture>("Buttons/Button Grey");
    }
}
