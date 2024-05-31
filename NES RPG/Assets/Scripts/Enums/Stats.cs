using UnityEngine;
using System;

[Serializable]
public class Stats
{
    private int MaxHealth;
    [SerializeField]
    private int Health;
    private int MaxMana;
    [SerializeField]
    private int Mana;
    [SerializeField]
    private int Attack;
    [SerializeField]
    private int Defense;
    [SerializeField]
    private int Magic;
    [SerializeField]
    private int Resistance;
    [SerializeField]
    private int Speed;
    
    public Stats(int health, int mana, int attack, int defense, int magic, int resistance, int speed)
    {
        Health = health;
        MaxHealth = health;
        Mana = mana;
        MaxMana = mana;
        Attack = attack;
        Defense = defense;
        Magic = magic;
        Resistance = resistance;
        Speed = speed;
    }

    public void SetMaxHealth(int value) => MaxHealth = value;
    public void SetHealth(int value) => Health = value;
    public void SetMaxMana(int value) => MaxMana = value;
    public void SetMana(int value) => Mana = value;
    public void SetAttack(int value) => Attack = value;
    public void SetDefense(int value) => Defense = value;
    public void SetMagic(int value) => Magic = value;
    public void SetResistance(int value) => Resistance = value;
    public void SetSpeed(int value) => Speed = value;

    public void AddMaxHealth(int value, bool reset = true)
    {
        MaxHealth += value;
        if (reset) ResetHealth();
    }
    public void AddMaxMana(int value, bool reset = true)
    {
        MaxMana += value;
        if (reset) ResetMana();
    }
    public void AddAttack(int value) => Attack += value;
    public void AddDefense(int value) => Defense += value;
    public void AddMagic(int value) => Magic += value;
    public void AddResistance(int value) => Resistance += value;
    public void AddSpeed(int value) => Speed += value;

    public int GetMaxHealth() => MaxHealth;
    public int GetHealth() => Health;
    public int GetMaxMana() => MaxMana;
    public int GetMana() => Mana;
    public int GetAttack() => Attack;
    public int GetDefense() => Defense;
    public int GetMagic() => Magic;
    public int GetResistance() => Resistance;
    public int GetSpeed() => Speed;

    public void Heal(int health)
    {
        Health += health;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void RestoreMana(int mana)
    {
        Mana += mana;

        if (Mana > MaxMana)
        {
            Mana = MaxMana;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
        }
    }

    public void UseMana(int mana = 1)
    {
        Mana -= mana;

        if (Mana < 0)
        {
            Mana = 0;
        }
    }

    public void ResetHealth() => Health = MaxHealth;
    public void ResetMana() => Mana = MaxMana;
}