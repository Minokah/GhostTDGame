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
	private int map = 0;
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
	
	public void triggerLevelDialogue(int mapId, int levelId){
		map = mapId;
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
		map = 0;
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
        if (map == 0 && level == 0)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Damn! I shouldn't have knocked over the concealment spell pedestal!\nI'll need to start collecting those energy crystals before the humans arrive to ruin my house!";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 0 && level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Okay, that's one energy crystal charged. Now I just need to charge 9 more.\nI'll need to look elsewhere to charge the other 8 though...";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 1 && level == 0)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "As I suspected... I won't be able to charge the rest of the crystals I need in that lush forest.\n" + 
                                 "Hopefully I can find some more energy here from this autumn environment.";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 1 && level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "This place has a lot of good energy for my spell.\nI can keep charging crystals here, but I'll need to watch out for the humans! They're getting more and more numerous...";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 1 && level == 2)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Well look at that: I'm almost halfway done my spell.\nOnce I finish here I can start looking for the last half of the energy crystals I need.";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 2 && level == 0)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Brrrr! Cold!\nThis place certainly isn't warm, but it might have the frigid energy I need for some of my crystals.";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 2 && level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Stubborn humans! Don't they care about not freezing to death!?\nWell, if they keep coming, I'll just keep beating them.";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 2 && level == 2)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "I'm getting tired of the cold here.\nOnce I get this crystal charged, I can hopefully find someplace warmer to charge the last two!";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 3 && level == 0)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "This is odd, I've never seen this part of the spirit world before.\nThis place might have the energy I need for the last two crystals I need!";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 3 && level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "This is it! I just need one more crystal and I can finally stop these pesky humans from trashing the place!";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
    }
	
	// should trigger when number of enemies spawned is around half of all enemies that can spawn in the level.
    IEnumerator middleDialogue()
    {
        dialoguePanel.SetActive(true);
        ghostDialogue.text = "I'm halfway done gathering energy for a energy crystal.\nI just have to hold out for a bit longer...";
		yield return new WaitForSeconds(7);
		dialoguePanel.SetActive(false);
    }

    // should trigger when number of enemies spawned is all enemies that can spawn in the level.
    IEnumerator endDialogue()
    {
        dialoguePanel.SetActive(true);
        ghostDialogue.text = "I'm done gathering energy for my crystal! Now to take care of the rest of these humans.";
		yield return new WaitForSeconds(7);
		dialoguePanel.SetActive(false);
    }

    public void tutorialInfo()
    {
        if (map == 0 && level == 0)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Rude and nasty humans are coming to trash up your house!\nBut don't fear, you can hire spirits who can help!\n\n" + 
                                        "Build appropriate towers for them to inhabit by clicking and placing towers onto the open grass.";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Basic humans pose little threat, but they come in high numbers.\n" + 
                                        "The Spirit Bolt you just hired are good overall defenders, who shoot homing bolts, so they never miss!";
            }
            if (dialogueNumber == 2)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Spirits need Soul Coins to show up and do their job. Reap human souls through combat to gather more!";
            }
            if (dialogueNumber == 3)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text =
	                "What kind of ghost doesn't know a few magical tricks?\n\nAgi (fire) lights enemies up within a radius.\nSelect spells from the Spell Sigil, point, and click!";
            }
        }
		
		if (map == 0 && level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Some humans use human made compounds called steriods to boost their strength.\n" + 
                                        "Tough enemies (in orange) have a lot more Health then their basic fellows. Be careful!";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Spirit Cannon shoots a shell that can hurt multiple humans at once.\n" + 
                                        "They hit slow but hard, and can miss. Consider hiring a spirit to pick-off any stragglers.";
            }
        }
		
		if (map == 1 && level == 0)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Bufu (ice) is a spell that slows down all humans in its radius. This can come in handy, such as keeping enemies in the range of towers for longer!";
            }
        }
		
		if (map == 1 && level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "In addition to steroids, humans may use Energy Drinks!\nFast enemies (in green) are really fast. Be sure to slow them down, or handle them quickly!";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Spirit Sniper shoots a bolt that instantly strikes their target.\nSlow shooting but packs a punch, so use them to take out humans that have a lot of health.";
            }
        }
		
		if (map == 1 && level == 2)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Some humans just can't get enough...\n\nMacho enemies (in purple) strengthen the healing of their nearby allies over time. They can make even the most basic enemy dangerous, so be careful.";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Zio (lightning) is a spell which has less area of effect compared to Agi, but it's much more deadly.\nUse it to take out any priority targets.";
            }
        }
		
		if (map == 2 && level == 0)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Through human advancements, they are using Balloons.\nThey have limited flight and can land deep behind your defenses, so take measures to catch these Trespassers (in blue)!";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Spirit Bubble tower slows down all enemies in its range. As a support tower, you can use it to help build defensive choke points.";
            }
        }
		
		if (map == 2 && level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Spirit Flood Gateway confuses humans, reducing the number of overall humans that pass onto the map\nand how fast they can organize and appear on the map.\n\nYou can only have one at a time.";
            }
        }
		
		if (map == 2 && level == 2)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Mix-Tape enemy (in black) seems to play these really annoying sounds they call Music.\nThese awful noises seem to motivate nearby humans to move faster!";
            }			
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Feng (wind) is a spell that flings humans backwards. Very useful in delaying those invaders, for instance.";
            }
        }
		
		if (map == 3 && level == 0)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Oh no! Here comes these annoying Camera or 'Influencer' enemies (in yellow)!\nOver time, these people can summon a lot of their fans to show up, adding to the number of invaders.";
            }			
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Jikan (time) is a spell that slows all humans wherever they are. It can help buy you critical time to react to the current defensive situation.";
            }
        }
		
		if (map == 3 && level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Arcane Spirit Tower contains the best spirit researchers.\nThrough research, they will greatly help with your spell casting, letting you cast spells more often!";
            }			
        }
    }
}
