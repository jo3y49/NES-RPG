using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatOptions
{
    public Dictionary<string, Button> spells = new();

    public PlayerCombatOptions(Button buttonPrefab)
    {
        spells.Add("Fire", GenerateButton(buttonPrefab, "Fire"));
        spells.Add("Ice", GenerateButton(buttonPrefab, "Ice"));
        spells.Add("Lightning", GenerateButton(buttonPrefab, "Lightning"));
        spells.Add("Water", GenerateButton(buttonPrefab, "Water"));
    }

    public Button GenerateButton(Button buttonPrefab, string text)
    {
        Button button = Object.Instantiate(buttonPrefab);
        button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        return button;
    }
}