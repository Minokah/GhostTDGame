using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text ghostDialogue;

    // this should be triggered by some other object that passes in what level the player is playing, to trigger the right message at the start. EnemySpawner?
    public void initalDialogue(int level)
    {
        if (level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Damm! I shouldn't have knocked over the concealment spell pedestal! Now a horde of humans are going to arrive.\n" +
                "I need to get some towers up. I can put some down on the open grass.";
            WaitUtility.Wait(7, () => {
                dialoguePanel.SetActive(false);
                ghostDialogue.text = "";
            }
            );
        }
    }

    // should trigger when number of enemies spawned is around half of all enemies that can spawn in the level. Probably trigger these from the enemy spawner.
    public void middleDialogue(int level)
    {
        if (level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "I'm halfway done my concealment spell. Taking out the rest of the humans shouldn't be too bad...";
            WaitUtility.Wait(7, () => {
                dialoguePanel.SetActive(false);
                ghostDialogue.text = "";
            }
            );
        }
    }

    // should trigger when number of enemies spawned is all enemies that can spawn in the level. Probably trigger these from the enemy spawner.
    public void endDialogue(int level)
    {
        if (level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "I'm done my concealment spell! I just got to kill the rest of these humans before I can cast it";
            WaitUtility.Wait(7, () => {
                dialoguePanel.SetActive(false);
                ghostDialogue.text = "";
            }
            );
        }
    }

    // all progress dialogues to inform player when enemy has reached a certain point; We will reuse these detectors for all levels; They retrigger after a while to prevent spamming player with notifications
    private void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Enemy") && this.name == "EarlyDetection1")
        {
            this.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Oh damm, those humans are already coming in. They're moving quicker then I expected too. I have to keep them away from my home, until I finish my concealment spell.\n " +
                "Speaking of spells, maybe I could use some against them right now?";
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
            ghostDialogue.text = "Hey they're starting to come pretty close... I should probably stop them before they get too close.";
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
            ghostDialogue.text = "Now they are way too close for comfort... I've got to stop them before they mess up my house!";
            WaitUtility.Wait(7, () => {
                dialoguePanel.SetActive(false);
                //this.gameObject.SetActive(true);
                ghostDialogue.text = "";
            }
            );
        }
    }


}
