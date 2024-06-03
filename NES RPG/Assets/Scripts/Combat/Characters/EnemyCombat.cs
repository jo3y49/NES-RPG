using System.Collections.Generic;

public class EnemyCombat : CharacterCombat {
    public EnemyData enemyData;

    public void SetEnemyData(EnemyData enemyData)
    {
        this.enemyData = enemyData;
        stats = baseStats = enemyData.stats;
        affectedElement = enemyData.element;
        characterName = enemyData.enemyName;
    }

    public (string, CharacterCombat) DecideAction(PlayerCombat target)
    {
        return (enemyData.DecideAction(), target);
    }
}