using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text ghostDialogue;
    public GameObject infoPanel;
    public TMP_Text tutorialDialogue;

    // all progress dialogues to inform player when enemy has reached a certain point; We will reuse these detectors for all levels;
    private void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Enemy") && this.name == "EarlyDetection1")
        {
            this.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Damn! Those humans are already coming in, and they're moving quicker than I expected too. Gotta keep them away from my home, until I finish collecting a concealment spell charge.";
            WaitUtility.Wait(7, () => { 
                dialoguePanel.SetActive(false);
                //this.gameObject.SetActive(true);
                ghostDialogue.text = ""; }
            );
        }
        else if (collisionObject.gameObject.CompareTag("Enemy") && this.name == "MidDetection1")
        {
            this.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "They're starting to come pretty close... I gotta stop them!";
            WaitUtility.Wait(7, () => {
                dialoguePanel.SetActive(false);
                //this.gameObject.SetActive(true);
                ghostDialogue.text = "";
            }
            );
        }
        else if (collisionObject.gameObject.CompareTag("Enemy") && this.name == "LateDetection1")
        {
            this.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "They're getting way too close for comfort now! I've got to stop them!";
            WaitUtility.Wait(7, () => {
                dialoguePanel.SetActive(false);
                //this.gameObject.SetActive(true);
                ghostDialogue.text = "";
            }
            );
        }
    }


}
