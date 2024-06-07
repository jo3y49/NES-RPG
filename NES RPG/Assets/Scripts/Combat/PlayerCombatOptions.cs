using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatOptions
{
    public List<string> spells = new();

    public PlayerCombatOptions()
    {
        spells.Add("Fire");
        spells.Add("Ice");
        spells.Add("Lightning");
        spells.Add("Water");
    }
}