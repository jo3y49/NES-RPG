using System;
using System.Collections.Generic;
using UnityEngine;
using static ElementEnum;

public class CharacterCombat : MonoBehaviour {
    public Stats stats { get; protected set;}
    public string characterName = "Player";
    protected Stats baseStats;
    protected int turnCount = 0;
    public CharacterAction CharacterAction {get; protected set;} = new();
    public ElementType affectedElement = ElementType.None;
    public bool stunned = false;

    protected virtual void Start() {
        FillActionList();
    }

    protected virtual void FillActionList()
    {
        CharacterAction.AddToActionList("Attack");
        CharacterAction.AddToActionList("Fire");
        CharacterAction.AddToActionList("Ice");
        CharacterAction.AddToActionList("Lightning");
        CharacterAction.AddToActionList("Water");
    }

    public virtual void PrepareBattle()
    {

    }

    public virtual ActionResult DoRandomAction(CharacterCombat target)
    {
        return CharacterAction.PerformRandomBattleAction(this, target);
    }

    public virtual ActionResult DoAction(string actionName, CharacterCombat target)
    {
        if (CharacterAction.battleActions.ContainsKey(actionName))
        {
            return CharacterAction.battleActions[actionName](this, target);
        } else
        {
            return null;
        }
    }

    public virtual void StartTurn()
    {
        turnCount++;
    }

    public virtual void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    public virtual void Heal(int heal)
    {
        stats.Heal(heal);
    }

    public virtual void EndBattle()
    {
        stats = baseStats;
    }
}