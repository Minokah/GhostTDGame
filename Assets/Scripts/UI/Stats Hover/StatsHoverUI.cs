using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatsHoverUI : MonoBehaviour
{
    public TMP_Text name, description;
    public RawImage icon;

    public Canvas parentCanvas;
    public bool track = false;
    public enum Alignment { LeftTop, LeftBottom, RightTop, RightBottom };
    Alignment align = Alignment.LeftBottom;
    CanvasVisible canvas;

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
    }

    void Update()
    {
        if (!track) return;

        Vector3 offset = new Vector3(0, 0);

        switch (align) {
            case Alignment.LeftTop:
                offset = new Vector2(0, 0);
                break;
            case Alignment.LeftBottom:
                offset = new Vector2(0, 180);
                break;
            case Alignment.RightTop:
                offset = new Vector2(-505, 0);
                break;
            case Alignment.RightBottom:
                offset = new Vector2(-505, 180);
                break;
        }

        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out movePos);
        transform.position = parentCanvas.transform.TransformPoint(movePos);
        transform.GetComponent<RectTransform>().localPosition += offset;
    }

    public void Set(GameObject entry, Alignment align = Alignment.LeftBottom) {
        this.align = align;

        ProgressionEntry prog = entry.GetComponent<ProgressionEntry>();

        name.text = prog.name;
        description.text = prog.description;
        icon.texture = Resources.Load<Texture>($"Icons/{prog.type}/{prog.id}");
    }

    public void Show()
    {
        track = true;
        canvas.Show();
    }

    public void Hide()
    {
        track = false;
        canvas.Hide();
    }
}
