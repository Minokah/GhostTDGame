using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatsHoverEntity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject entity;
    public StatsHoverUI.Alignment alignment = StatsHoverUI.Alignment.LeftTop;
    UI UI;

    // Start is called before the first frame update
    void Start()
    {
        UI = UI.Get();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Don't do anything if there is no valid entity
        if (entity == null) return;
        UI.StatsHover.Set(entity, alignment);
        UI.StatsHover.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.StatsHover.Hide();
    }
}
