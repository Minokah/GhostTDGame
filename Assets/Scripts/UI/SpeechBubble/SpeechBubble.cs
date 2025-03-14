using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetText(string message)
    {
        TMP_Text textMesh = GetComponentInChildren<TMP_Text>();
        textMesh.text = message;
    }

}