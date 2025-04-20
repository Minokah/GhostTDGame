using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuEntry : MonoBehaviour
{
    Game Game;
    UI UI;
    Button button;
    public GameObject entry, previewEntry, previewCamera;
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
        else UI.ProgressionMenu.ShowInformation(entry, previewEntry, previewCamera);
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
        
        // Set model texture (cosmetic)
        TowerToGhostMatLink link = previewEntry.GetComponent<TowerToGhostMatLink>();
        int i = 1;
        foreach (ProgressionCosmetic cosm in entry.GetComponents<ProgressionCosmetic>())
        {
            if (cosm.equipped) link.SetMaterial(i);
            i++;
        }

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
            string mapText = "";
            string levelText = "";

            if (entry.requireLevel <= 2) {
                mapText = "Forest"; // 0-1
                levelText = entry.requireLevel.ToString();
            }
            else if (entry.requireLevel <= 5) {
                mapText = "Falls"; // 3-5
                levelText = (entry.requireLevel - 2).ToString();
            }
            else if (entry.requireLevel <= 8) {
                mapText = "Snowlands"; // 5-7
                levelText = (entry.requireLevel - 5).ToString();
            }
            else if (entry.requireLevel <= 10)
            {
                mapText = "Spirit Realm"; // 8-9
                levelText = (entry.requireLevel - 8).ToString();
            }

            progressText.text = $"Clear Stage {levelText} of the {mapText}";
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