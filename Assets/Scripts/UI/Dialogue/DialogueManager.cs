using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public List<GameObject> mapTriggerList = new List<GameObject>();
    // Start is called before the first frame update
    public void resetDialogue(){
		foreach (GameObject trigger in mapTriggerList){
			if (trigger != null){
				trigger.SetActive(true);
			}	
		}
	}	
}
