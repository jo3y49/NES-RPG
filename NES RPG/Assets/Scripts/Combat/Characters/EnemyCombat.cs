using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ElementEnum;

public class EnemyCombat : CharacterCombat {
    public EnemyData enemyData;

    protected override void Start()
    {
        stats = baseStats = enemyData.stats;

        FillActionList();
    }

    public (string, CharacterCombat) DecideAction(List<PlayerCombat> target)
    {
        

        if (stats.GetMana() <= 0 || target[0].stunned)
        {
            string action = CharacterAction.battleActions.Keys.ToList()[0];
            CharacterCombat targetCharacter = target[0];

            return (action, targetCharacter);
        } else if (target[0].affectedElement != ElementType.None)
        {
            int randomNumber = Random.Range(0,2);
            CharacterCombat targetCharacter = target[0];
            string action = targetCharacter.affectedElement switch
            {
                ElementType.Fire or ElementType.Water => (randomNumber == 0) ? "Lightning" : "Ice",// Randomly choose Lightning or Ice
                ElementType.Ice or ElementType.Lightning => (randomNumber == 0) ? "Fire" : "Water",// Randomly choose Fire or Water
                _ => "Attack", // Default to "Attack
            };
            return (action, targetCharacter);
        }
        else {
            int randomNumber = Random.Range(1, CharacterAction.battleActions.Count);
            string action = CharacterAction.battleActions.Keys.ToList()[randomNumber];
            CharacterCombat targetCharacter = target[0];

            return (action, targetCharacter);
        } 
    }
}