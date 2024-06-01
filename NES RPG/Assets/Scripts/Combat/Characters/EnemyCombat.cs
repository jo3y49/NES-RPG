using System.Collections.Generic;

public class EnemyCombat : CharacterCombat {
    public EnemyData enemyData;

    public void SetEnemyData(EnemyData enemyData)
    {
        this.enemyData = enemyData;
        stats = baseStats = enemyData.stats;
        affectedElement = enemyData.element;
    }

    public (string, CharacterCombat) DecideAction(List<PlayerCombat> target)
    {
        return (enemyData.DecideAction(), target[0]);
    }
}