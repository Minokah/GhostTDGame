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
            ghostDialogue.text = "Damm! I shouldn't have knocked over the concealment spell pedestal! Now a horde of humans are coming to ruin my house.\n" +
                "I need to start collecting different energy crystals to get the spell back up!";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 0 && level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Okay, that's one energy crystal charged. Now I just need to charge 9 more. Hmm, for the energy I need, I might soon need to charge elsewhere for the other 8...";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 1 && level == 0)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Sadly, I won't be able to charge the rest of the crystals I need in that lush forest.\n" + 
			"Hopefully I can find some more energy here, in this Autumun enviroment.";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 1 && level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "This place has a lot of good energy for my spell. I can keep charging crystals here. The only issue is that the humans seem to get more and more numerous...";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 1 && level == 2)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Well look at that: I'm almost halfway done my spell. Once I finish here I can start looking for the last half of the energy crystals I need.";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 2 && level == 0)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Brr.... This place certainly isn't warm, but it might have the frigid energy I need for some of my crystals.";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 2 && level == 1)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "These humans really are stubborn huh? They keep coming even though they are liable to freeze to death! Well, if they keep coming, I'll keep beating them.";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 2 && level == 2)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "I'm starting to get tired of the cold here. Once I get this crystal charged, I can hopefully find someplace warmer to charge the last two!";
			yield return new WaitForSeconds(10);	
			dialoguePanel.SetActive(false);
			runDialogue = true;
        }
		
		if (map == 3 && level == 0)
        {
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "This is odd, I've never seen this part of the spirit world before. Which means it just might have the energy I need for the last two crystals I need!";
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
        ghostDialogue.text = "I'm halfway done gathering energy for a energy crystal. I just have to hold out for a bit longer...";
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
		
		if (map == 0 && level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Some humans are tougher than others because of something called 'steroids' or whatever. In any case, Tough enemies (in orange) have a lot more health then their basic fellows. So be careful when you see one.";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Spirit Cannon shoots a shell that can hurt multiple humans at once. They don't attack too quickly and can miss, so you might need something to pick-off any stragglers.";
            }
        }
		
		if (map == 1 && level == 0)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Ice Spell slows down all humans in the targeted radius. This can come in handy, such as keeping enemies in the range of towers for longer.";
            }
        }
		
		if (map == 1 && level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Some humans are faster than others because of something called 'energy drinks' or whatever. In any case, Fast enemies (In Green) are really fast. Slow them down, or handle them quickly.";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Spirit Sniper shoots a bolt that instantly strikes their target. They attack very slowly but powerfully, so use them to take out humans that have a lot of health.";
            }
        }
		
		if (map == 1 && level == 2)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Ok so some humans seem to be a bit nuts... These Macho enemies(in purple) seem to strengthen the heal of their nearby allies over time. They can make even the most basic enemy dangerous, so be careful.";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Lightning Spell has less area of effect compared to the fireball, but it's much more deadly. Use it to take out any priority targets.";
            }
        }
		
		if (map == 2 && level == 0)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The humans seem to be using gadgets called 'Balloons'... They have limited flight and can land deep behind your defenses, so take measures to catch these tresspassers (in blue).";
            }
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Spirit Bubble tower slows down all enemies in its range. As a support tower, you can use it to help build defensive chokepoints.";
            }
        }
		
		if (map == 2 && level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Spirit Flood Gateway confuses humans, reducing the number of overall humans that pass onto the map, and also how fast they can organize and appear on the map. You can only have one at a time.";
            }
        }
		
		if (map == 2 && level == 2)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Mix-Tape Enemy (in black) seems to play these really annoying sounds they call 'music'. Somehow, these awful noises motivate nearby humans to move even faster.";
            }			
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Wind Spell flings humans backwards. Very useful in delaying those invaders, for instance.";
            }
        }
		
		if (map == 3 && level == 0)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "Oh no! Here comes these annoying Camera or 'Influencer' enemies! Over time, these people (in yellow) can summon a lot of their fans to show up, adding to the number of invaders.";
            }			
            if (dialogueNumber == 1)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Time Spell will slow all humans wherever they are. It can help buy you critical time to react to the current defensive situation.";
            }
        }
		
		if (map == 3 && level == 1)
        {
            if (dialogueNumber == 0)
            {
                infoPanel.SetActive(true);
                tutorialDialogue.text = "The Arcane Spirit Tower contains the best spirit researchers, who will greatly help with your spell casting, letting you cast spells more often.";
            }			
        }
    }
}
