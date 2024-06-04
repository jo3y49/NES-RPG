using UnityEngine;
using static ElementEnum;

[CreateAssetMenu(fileName = "FinalBoss", menuName = "EnemyData/FinalBoss", order = 0)]
public class FinalBoss : EnemyData {
    public override string DecideAction(PlayerCombat target = null)
    {
        if (target.stunned)
        {
            return "Attack";
        } 
        else if (target.affectedElement != ElementType.None)
        {
            int randomNumber = Random.Range(0,2);
            string action = target.affectedElement switch
            {
                ElementType.Fire or ElementType.Water => (randomNumber == 0) ? "Lightning" : "Ice",// Randomly choose Lightning or Ice
                ElementType.Ice or ElementType.Lightning => (randomNumber == 0) ? "Fire" : "Water",// Randomly choose Fire or Water
                _ => "Attack", // Default to "Attack
            };
            return action;
        }
        else {
            return CharacterActionList.GetRandomSpell();
        }
    }
}