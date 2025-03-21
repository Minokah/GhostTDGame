using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencySectionUI : MonoBehaviour
{
    Game Game;
    public TMP_Text amount;

    void Start()
    {
        Game = Game.Get();
    }

    public void Refresh()
    {
        amount.text = Game.StatisticsManager.statistics["tokens"].ToString();
    }
}
