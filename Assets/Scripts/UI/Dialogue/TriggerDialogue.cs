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

    // this should be triggered by some other object that passes in what level the player is playing, to trigger the right message at the start. EnemySpawner?
    public void initalDialogue(int level)
    {
        if (level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Damm! I shouldn't have knocked over the concealment spell pedestal! Now a horde of humans are going to arrive.\n" +
                "I need to call up some spirits. I have some space I can invite them to on the open grass.";
            WaitUtility.Wait(7, () => {
                dialoguePanel.SetActive(false);
                ghostDialogue.text = "";
            }
            );
        }
    }

    public void tutorialInfo(int level, int dialogueId)
    {
        if (level == 1)
        {
            if (dialogueId == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Rude and nasty humans are coming to trash up your house! But don't fear, you can hire spirits who can help!\n " +
                    "Build appropriate towers for them to inhabit by clicking and placing towers onto the open grass.";
                WaitUtility.Wait(7, () => {
                    infoPanel.SetActive(false);
                    tutorialDialogue.text = "";
                }
                );
            }
            if (dialogueId == 2)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "These basic humans pose little threat, other than coming in so damm high numbers.\n" +
                    "The spirit bolts you just hired are good overall defenders, who shoot homing bolts, so they never miss!";
                WaitUtility.Wait(7, () => {
                    infoPanel.SetActive(false);
                    tutorialDialogue.text = "";
                }
                );
            }
            if (dialogueId == 3)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Spirits need soul coins to show up and do their job. You can gather more, primarily from reaping the souls of those nasty humans!\n " +
                    "You get souls for every human you kill.";
                WaitUtility.Wait(7, () => {
                    infoPanel.SetActive(false);
                    tutorialDialogue.text = "";
                }
                );
            }
            if (dialogueId == 4)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "What kind of ghost doesn't know a few magical tricks? Try out your fire spell on those pesky invaders.\n" +
                    "Just select from the spell menu, aim, and click on the group of unluck humans.";
                WaitUtility.Wait(7, () => {
                    infoPanel.SetActive(false);
                    tutorialDialogue.text = "";
                }
                );
            }
        }
    }

    // should trigger when number of enemies spawned is around half of all enemies that can spawn in the level. Probably trigger these from the enemy spawner.
    public void middleDialogue()
    {
        dialoguePanel.SetActive(true);
        ghostDialogue.text = "I'm halfway done gathering energy for my concealment spell charge. Taking care of the rest of these humans shouldn't be too bad...";
        WaitUtility.Wait(7, () => {
            dialoguePanel.SetActive(false);
            ghostDialogue.text = "";
        }
        );
    }

    // should trigger when number of enemies spawned is all enemies that can spawn in the level. Probably trigger these from the enemy spawner.
    public void endDialogue(int level)
    {
        dialoguePanel.SetActive(true);
        ghostDialogue.text = "I'm done gathering energy for my concealment spell charge! I just got to handle the rest of these humans before I can cast it";
        WaitUtility.Wait(7, () => {
            dialoguePanel.SetActive(false);
            ghostDialogue.text = "";
        }
        );
    }

    // all progress dialogues to inform player when enemy has reached a certain point; We will reuse these detectors for all levels;
    private void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Enemy") && this.name == "EarlyDetection1")
        {
            this.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Oh damm, those humans are already coming in. They're moving quicker then I expected too. I have to keep them away from my home, until I finish collecting a concealment spell charge.";
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
