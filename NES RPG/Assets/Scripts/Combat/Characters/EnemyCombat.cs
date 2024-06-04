using UnityEngine;

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
        return (enemyData.DecideAction(target), target);
    }

    public override void EndBattle()
    {
        if (enemyData is MidBoss)
        {
            GameDataManager.Instance.AddDefeatedBosses();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            HostileWorldManager.Instance.StartCombat(enemyData);
        }
    }
}