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
    public SpellbarUI Spellbar;
    public CastbarUI Castbar;
    public StatsHoverUI StatsHover;

    public bool windowActive = true;

    public static UI Get() {
        return GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }
}
