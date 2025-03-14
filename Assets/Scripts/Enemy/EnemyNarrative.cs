using UnityEngine;
using TMPro;

public class EnemyNarrative : MonoBehaviour
{
    [Header("Speech Bubble Settings")]
    public GameObject speechBubblePrefab; // Assign your SpeechBubble prefab here
    public Vector3 bubbleOffset = new Vector3(0, 2f, 0); // Offset above the enemy

    //[Header("Audio Settings")]
    //public AudioClip gruntSound; // Assign a grunt sound clip
    //private AudioSource audioSource;

    //void Awake()
    //{
    //    // Ensure there is an AudioSource on the enemy
    //    audioSource = GetComponent<AudioSource>();
    //    if (audioSource == null)
    //    {
    //        audioSource = gameObject.AddComponent<AudioSource>();
    //    }
    //}

    /// <summary>
    /// Instantiates a speech bubble with the given message.
    /// </summary>
    public void ShowSpeechBubble(string message)
    {
        if (speechBubblePrefab != null)
        {
            // Instantiate the bubble at an offset above the enemy
            GameObject bubble = Instantiate(speechBubblePrefab, transform.position + bubbleOffset, Quaternion.identity, transform);

            // Assume the bubble has a TextMesh or TMP component in its children
            TMP_Text textMesh = bubble.GetComponentInChildren<TMP_Text>();
            if (textMesh != null)
            {
                textMesh.text = message;
            }
            // Destroy the bubble after 2 seconds
            Destroy(bubble, 2f);
        }
    }

    /// <summary>
    /// Plays a grunt sound.
    /// </summary>
    //public void PlayGrunt()
    //{
    //    if (gruntSound != null && audioSource != null)
    //    {
    //        audioSource.PlayOneShot(gruntSound);
    //    }
    //}
}
