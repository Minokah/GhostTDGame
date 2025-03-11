using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    public RawImage icon;

    public void Set(string icon)
    {
        this.icon.texture = Resources.Load<Texture>($"Icons/Spells/{icon}");
    }
}
