using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public MenuUI Menu;
    public EndScreenUI EndScreen;
    public NotificationUI NotificationUI;
    public AchievementMenuUI AchievementMenu;
    public ProgressionMenuUI ProgressionMenu;
    public BuildMenuUI BuildMenu;
	public CameraModeUI CameraPanel;
    public SpellbarUI Spellbar;
    public CastbarUI Castbar;
    public StatsHoverUI StatsHover;
	public MenuMapSelect MapSelection;

    public bool windowActive = true;

    public static UI Get() {
        return GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }
	
	public void resetLevelSelection(){
		// Fixes a visual bug When player finishes a game. This ensure the unlocked levels or map is shown correctly after player returns to map selection menu from main menu
		MapSelection.UpdatePreview();	
	}
}
