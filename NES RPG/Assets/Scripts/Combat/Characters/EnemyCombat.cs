using UnityEngine;

public class EnemyCombat : CharacterCombat {
    public EnemyData enemyData;

    public void SetEnemyData(EnemyData enemyData)
    {
        this.enemyData = Instantiate(enemyData);
        stats = baseStats = this.enemyData.stats;
        affectedElement = this.enemyData.element;
        characterName = this.enemyData.enemyName;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GameDataManager.Instance.AddDamageDealt(damage);
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
            PlayerMovement.Instance.GetComponent<PlayerCombat>().UpgradeSword();
        }

        GameDataManager.Instance.AddKills();

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            HostileWorldManager.Instance.StartCombat(enemyData);
        }
    }
}