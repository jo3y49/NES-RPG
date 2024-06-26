using System;
using UnityEngine;
using static ElementEnum;
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Stats stats;
    public ElementType element;
    public int experience;

    public virtual string DecideAction(PlayerCombat target = null)
    {
        return "Attack";
    }
}