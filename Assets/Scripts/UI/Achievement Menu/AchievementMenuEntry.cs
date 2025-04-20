using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Achievement menu entry for UI
public class AchievementMenuEntry : MonoBehaviour
{
    Game Game;
    public AchievementEntry entry;
    public TMP_Text name, description, progress, completed, rewardText;
    public RectTransform progressBar;
    public RawImage icon, rewardIcon;

    // Set info for achievement
    void Start()
    {
        Game = Game.Get();
    }

    public void Refresh()
    {
        name.text = entry.name;
        description.text = entry.description;
        int amt = Game.AchievementManager.GetProgress(entry.requireType, entry.requireID);
        progress.text = $"{amt} / {entry.requireAmount}";
        if (entry.rewardType == null || entry.rewardType == "")
        {
            rewardText.gameObject.SetActive(false);
            rewardIcon.gameObject.SetActive(false);
        }
        else rewardText.text = entry.rewardName;

        //icon.texture = Resources.Load<Texture>($"Icons/achievements/{entry.id}");

        // No entry for reward type? Assume no reward and make it blank
        if (entry.rewardType != null && entry.rewardType != "")
        {
            rewardIcon.texture = Resources.Load<Texture>($"Icons/{entry.rewardType}/{entry.rewardID}");
            rewardText.text = entry.rewardName;
        }
        else {
            rewardIcon.gameObject.SetActive(false);
            rewardText.gameObject.SetActive(false);
        }

        progressBar.sizeDelta = new Vector2(355 * (amt / entry.requireAmount), 41);
    }
}
