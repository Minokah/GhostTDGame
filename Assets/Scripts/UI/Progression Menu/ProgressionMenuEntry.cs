using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuEntry : MonoBehaviour
{
    Game Game;
    UI UI;
    Button button;
    public GameObject entry, previewCamera;
    public TMP_Text name, description, progressText;
    public RectTransform progressBar;
    public RawImage icon;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    // Tell the progression menu to display selected info, unless we are changing spells. Then do that instead.
    private void Clicked()
    {
        if (UI.ProgressionMenu.listing.IsSpellSelecting())
        {
            UI.ProgressionMenu.listing.ChangeSpell(entry);
        }
        else
        {
            if (previewCamera != null) previewCamera.SetActive(true);
            UI.ProgressionMenu.ShowInformation(entry);
        }
    }

    // Refresh this entry's info
    public void Refresh()
    {
        // Not valid? Don't do anything
        ProgressionEntry entry = this.entry.GetComponent<ProgressionEntry>();
        name.text = entry.name;
        description.text = entry.description;
        icon.texture = Resources.Load<Texture>($"Icons/{entry.type}/{entry.id}");

        int level = Game.ProgressionManager.GetPlayerLevel();

        // Level met? Show "View Entry" instead
        if (level >= entry.requireLevel)
        {
            progressText.text = "View Entry";
            button.interactable = true;

            progressBar.sizeDelta = new Vector2(418f, progressBar.sizeDelta.y);
        }
        // Disable button too
        else
        {
            string levelText = "";

            if (entry.requireLevel <= 3) levelText = "Map1"; // 0-2
            else if (entry.requireLevel <= 5) levelText = "Map2"; // 3-4
            else if (entry.requireLevel <= 7) levelText = "Map3"; // 5-6
            else if (entry.requireLevel <= 10) levelText = "Map4"; // 7-9

            progressText.text = $"Clear All Stages of {levelText}";
            button.interactable = false;

            // Set progress bar
            progressBar.sizeDelta = new Vector2(418f * (level / entry.requireLevel), progressBar.sizeDelta.y);
        }
    }

    // Change the text for spells.
    public void SetChanging(bool changing)
    {
        if (!changing)
        {
            Refresh();
            return;
        }

        ProgressionEntry entry = this.entry.GetComponent<ProgressionEntry>();
        // Level met?
        if (Game.ProgressionManager.GetPlayerLevel() >= entry.requireLevel)
        {
            progressText.text = "Select To Change";
        }
    }
}