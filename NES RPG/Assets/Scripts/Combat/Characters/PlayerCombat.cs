using UnityEngine;

public class PlayerCombat : CharacterCombat {
    protected override void Start()
    {
        stats = baseStats = GameDataManager.Instance.GetStats();

        base.Start();
    }

    public void XPGain(int xp)
    {
        GameDataManager.Instance.AddXP(xp);

        if (GameDataManager.Instance.GetXP() >= GameDataManager.Instance.GetLevel() * 15)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        GameDataManager.Instance.AddLevel();
        baseStats.AddAttack(2);
        baseStats.AddMagic(2);
        baseStats.AddMaxMana(1);
        baseStats.AddDefense(2);
        baseStats.AddSpeed(1);
        baseStats.AddMaxHealth(5);
        stats = baseStats;
        GameDataManager.Instance.SetStats(stats);
    }
}