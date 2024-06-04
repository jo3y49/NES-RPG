using UnityEngine;

[CreateAssetMenu(fileName = "Magic Tank", menuName = "EnemyData/MagicTank", order = 0)]
public class MagicTank : EnemyData {
    public override string DecideAction(PlayerCombat target = null)
    {
        return CharacterActionList.GetRandomSpell();;
    }
}