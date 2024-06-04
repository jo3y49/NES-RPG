using UnityEngine;

public class PlayerCombat : CharacterCombat {
    protected override void Start()
    {
        stats = baseStats = GameDataManager.Instance.GetStats();

        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GameDataManager.Instance.AddDamageTaken(damage);
    }

    public void XPGain(int xp)
    {
        GameDataManager.Instance.AddXP(xp);

        if (GameDataManager.Instance.GetXP() >= GameDataManager.Instance.GetLevel() * 15)
        {
            LevelUp();
            XPGain(0);
        }
    }

    private void LevelUp()
    {
        GameDataManager.Instance.AddLevel();

        baseStats.AddMaxHealth(5);
        baseStats.AddMaxMana(1);
        baseStats.AddDefense(2);
        baseStats.AddSpeed(1);
        stats = baseStats;

        GameDataManager.Instance.SetStats(stats);
    }

    public void UpgradeSword()
    {
        if (GameDataManager.Instance.GetSwordLevel() > 3) return;

        GameDataManager.Instance.AddSwordLevel();

        baseStats.AddAttack(5);
        stats = baseStats;

        GameDataManager.Instance.SetStats(stats);
    }
}