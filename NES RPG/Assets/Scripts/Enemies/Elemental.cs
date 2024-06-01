using UnityEngine;

[CreateAssetMenu(fileName = "Elemental", menuName = "EnemyData/Elemental", order = 0)]
public class Elemental : EnemyData {

    public override string DecideAction(PlayerCombat target = null)
    {
        return element.ToString();
    }
}