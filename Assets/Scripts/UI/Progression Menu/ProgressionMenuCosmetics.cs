using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuCosmetics : MonoBehaviour
{
    public RawImage defaultIcon, cosmetic1Icon, cosmetic2Icon;
    public Button defaultButton, cosmetic1Button, cosmetic2Button;
    CanvasVisible canvas;

    private ProgressionCosmetic cosmetic1, cosmetic2;

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
        defaultButton.onClick.AddListener(EquipDefault);
        cosmetic1Button.onClick.AddListener(EquipFirst);
        cosmetic2Button.onClick.AddListener(EquipSecond);
    }

    public void Show(GameObject entry)
    {
        cosmetic1Button.interactable = false;
        cosmetic2Button.interactable = false;
        
        // Reset button states
        defaultButton.GetComponent<CanvasGroup>().alpha = 0.6f;
        cosmetic1Button.GetComponent<CanvasGroup>().alpha = 0.6f;
        cosmetic2Button.GetComponent<CanvasGroup>().alpha = 0.6f;

        // Map
        ProgressionCosmetic[] cosmetics = entry.GetComponents<ProgressionCosmetic>();
        cosmetic1 = cosmetics[0];
        cosmetic2 = cosmetics[1];
        
        // Set interactable/equipped
        if (cosmetic1.unlocked)
        {
            cosmetic1Button.interactable = true;
            cosmetic1Icon.texture = Resources.Load<Texture>($"Icons/cosmetics/{cosmetic1.id}");
        }
        else cosmetic1Icon.texture = Resources.Load<Texture>($"Icons/generic/lock");

        if (cosmetic2.unlocked)
        {
            cosmetic2Button.interactable = true;
            cosmetic2Icon.texture = Resources.Load<Texture>($"Icons/cosmetics/{cosmetic2.id}");
        }
        
        
        if (cosmetic1.equipped) cosmetic1Button.GetComponent<CanvasGroup>().alpha = 1f;
        else if (cosmetic2.equipped) cosmetic2Button.GetComponent<CanvasGroup>().alpha = 1f;
        else defaultButton.GetComponent<CanvasGroup>().alpha = 1f;
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    public void EquipDefault()
    {
        Equip(0);
    }

    public void EquipFirst()
    {
        Equip(1);
    }

    public void EquipSecond()
    {
        Equip(2);
    }

    public void Equip(int which)
    {
        defaultButton.GetComponent<CanvasGroup>().alpha = 0.6f;
        cosmetic1Button.GetComponent<CanvasGroup>().alpha = 0.6f;
        cosmetic2Button.GetComponent<CanvasGroup>().alpha = 0.6f;
        cosmetic1.equipped = false;
        cosmetic2.equipped = false;
        
        if (which == 0) defaultButton.GetComponent<CanvasGroup>().alpha = 1f;
        else if (which == 1)
        {
            cosmetic1Button.GetComponent<CanvasGroup>().alpha = 1f;
            cosmetic1.equipped = true;
        }
        else if (which == 2)
        {
            cosmetic2Button.GetComponent<CanvasGroup>().alpha = 1f;
            cosmetic2.equipped = true;
        }

        Game.Get().ProfileManager.Save();
    }
}
