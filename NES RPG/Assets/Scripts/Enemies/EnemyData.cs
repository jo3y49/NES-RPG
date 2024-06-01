using UnityEngine;
using static ElementEnum;
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Stats stats;
    public ElementType element;

    public virtual void OnEnable()
    {
        enemyName = this.name;
    }

    public virtual string DecideAction(PlayerCombat target = null)
    {
        return "Attack";
    }
}