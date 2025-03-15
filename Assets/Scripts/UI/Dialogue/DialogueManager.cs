using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public List<GameObject> mapTriggerList = new List<GameObject>();
	private int dialogueNumber = 0;
	private int level = 0;
	private Boolean runDialogue = false;
	private Boolean midPoint = false;
	private Boolean finalPoint = false;
	
	public GameObject dialoguePanel;
    public TMP_Text ghostDialogue;
    public GameObject infoPanel;
    public TMP_Text tutorialDialogue;
	
	Game Game;
	
	void Start()
	{
		Game = Game.Get();
	}
	
	void Update() 
	{
		if (runDialogue == true){
			if (Game.EnemySpawner.spawnCount >= (int)(Game.EnemySpawner.maxSpawnCount/2) && midPoint == false){
				midPoint = true;
				StartCoroutine(middleDialogue());
			}
			else if (Game.EnemySpawner.spawnCount >= Game.EnemySpawner.maxSpawnCount && finalPoint == false){
				finalPoint = true;
				StartCoroutine(endDialogue());
			}
			
			StartCoroutine(RunDialogue());
		}	
	}
	
	public void triggerLevelDialogue(int levelId){
		level = levelId;
		StartCoroutine(initalDialogue());
	}

    public void resetDialogue(){
		StopAllCoroutines();
		foreach (GameObject trigger in mapTriggerList){
			if (trigger != null){
				trigger.SetActive(true);
			}	
		}
		
		dialogueNumber = 0;
		level = 0;
		runDialogue = false;
		midPoint = false;
		finalPoint = false;
		
		dialoguePanel.SetActive(false);
		infoPanel.SetActive(false);
	}	
	
	IEnumerator RunDialogue()
    {
		runDialogue = false;
		tutorialInfo();
        yield return new WaitForSeconds(10);
		infoPanel.SetActive(false);
		runDialogue = true;
		dialogueNumber = dialogueNumber + 1;
    }
	
	// this should be triggered by some other object that passes in what level the player is playing, to trigger the right message at the start.
    IEnumerator initalDialogue()
    {
        if (level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Damm! I shouldn't have knocked over the concealment spell pedestal! Now a horde of humans are going to arrive.\n" +
                "I need to start collecting different energies to get the spell back up!";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (level == 2)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Okay, that's on energy crystal charged. Now I just need to collect 9 more. I might need to look elsewhere for the other 8...";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
    }
	
	// should trigger when number of enemies spawned is around half of all enemies that can spawn in the level.
    IEnumerator middleDialogue()
    {
        dialoguePanel.SetActive(true);
        ghostDialogue.text = "I'm halfway done gathering energy for my concealment spell charge. Taking care of the rest of these humans shouldn't be too bad...";
		yield return new WaitForSeconds(7);
		dialoguePanel.SetActive(false);
    }

    // should trigger when number of enemies spawned is all enemies that can spawn in the level.
    IEnumerator endDialogue()
    {
        dialoguePanel.SetActive(true);
        ghostDialogue.text = "I'm done gathering energy for my concealment spell charge! I just got to handle the rest of these humans before I can cast it";
		yield return new WaitForSeconds(7);
		dialoguePanel.SetActive(false);
    }

    public void tutorialInfo()
    {
        if (level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Rude and nasty humans are coming to trash up your house! But don't fear, you can hire spirits who can help!\n " +
                    "Build appropriate towers for them to inhabit by clicking and placing towers onto the open grass.";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "These basic humans pose little threat, other than coming in so damm high numbers.\n" +
                    "The spirit bolts you just hired are good overall defenders, who shoot homing bolts, so they never miss!";
            }
            if (dialogueNumber == 2)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Spirits need soul coins to show up and do their job. You can gather more, primarily from reaping the souls of those nasty humans!\n " +
                    "You get souls for every human you kill.";
            }
            if (dialogueNumber == 3)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "What kind of ghost doesn't know a few magical tricks? Try out your fire spell on those pesky invaders.\n" +
                    "Just select from the spell menu, aim, and click on the group of unlucky humans.";
            }
        }
    }
}
