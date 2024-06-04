using System.Collections.Generic;
using UnityEngine;
using static CharacterAction;
using static ActionResult;
using static ElementEnum;

public static class CharacterActionList
{
    private static Dictionary<string, BattleAction> battleActions = new();

    static CharacterActionList()
    {
        FillActionList();
    }

    public static ActionResult BaseAttack(CharacterCombat user, CharacterCombat target)
    {
        // add slight randomness to the damage calculation
        int damage = user.stats.GetAttack() - target.stats.GetDefense();
        bool isHit = Random.Range(0, 100) < 90;
        return CreateActionResult(user, target, isHit ? ResultType.Damage : ResultType.Miss, damage, 10, "attacks");
    }

    public static ActionResult FireSpell(CharacterCombat user, CharacterCombat target)
    {
        int damage = (int)(user.stats.GetMagic() * 1.5f) - target.stats.GetResistance();
        bool isHit = Random.Range(0, 100) < 80;
        return CreateElementalActionResult(user, target, isHit, damage, ElementType.Fire);
    }

    public static ActionResult LightningSpell(CharacterCombat user, CharacterCombat target)
    {
        int damage = (int)(user.stats.GetMagic() * 1.5f) - target.stats.GetResistance();
        bool isHit = Random.Range(0, 100) < 80;
        return CreateElementalActionResult(user, target, isHit, damage, ElementType.Lightning);
    }

    public static ActionResult WaterSpell(CharacterCombat user, CharacterCombat target)
    {
        int damage = user.stats.GetMagic() - target.stats.GetResistance();
        bool isHit = true;
        return CreateElementalActionResult(user, target, isHit, damage, ElementType.Water);
    }

    public static ActionResult IceSpell(CharacterCombat user, CharacterCombat target)
    {
        int damage = user.stats.GetMagic() - target.stats.GetResistance();
        bool isHit = true;
        return CreateElementalActionResult(user, target, isHit, damage, ElementType.Ice);
    }

    private static ActionResult CreateActionResult(
        CharacterCombat user, 
        CharacterCombat target, 
        ResultType resultType, 
        int damage,
        int stunChance,
        string baseMessage)
    {
        damage += Random.Range((int)(damage*.9), (int)(damage*1.1));
        bool stun = resultType == ResultType.Damage && Random.Range(0, 100) < stunChance;
        string message = stun 
            ? $"{user.characterName} stuns {target.characterName}!"
            : (resultType == ResultType.Damage ? $"{user.characterName} {baseMessage} {target.characterName}!" : $"{user.characterName} missed!");

        return new ActionResult(user, resultType, damage)
        {
            stun = stun,
            message = message,
            target = target
        };
    }
    
    private static ActionResult CreateElementalActionResult(
        CharacterCombat user,
        CharacterCombat target,
        bool isHit,
        int damage,
        ElementType element)
    {
        damage = Random.Range((int)(damage*.9), (int)(damage*1.1));
        ResultType resultType = isHit ? ResultType.ElementAttack : ResultType.Miss;
        bool stun = false;
        string message;

        if (isHit)
        {
            if (IsStrongAgainst(element, target.affectedElement))
            {
                damage += (int)(damage * 1.2f);
                stun = true;
                message = $"{user.characterName} stuns {target.characterName} with {element}!";
                element = ElementType.None;
            }
            else if (IsWeakAgainst(element, target.affectedElement))
            {
                damage += (int)(damage * 0.5f);
                message = $"{user.characterName} weakly hit {target.characterName} with {element}!";
                element = ElementType.None;
            }
            else
            {
                message = $"{user.characterName} casts {element} on {target.characterName}!";
            }
        }
        else
        {
            message = $"{user.characterName} missed!";
        }

        return new ActionResult(user, resultType, damage)
        {
            stun = stun,
            message = message,
            target = target,
            element = element,
            manaUsed = 1
        };
    }

    // Additional helper methods to determine elemental strengths/weaknesses
    private static bool IsStrongAgainst(ElementType attackElement, ElementType defenseElement)
    {
        // Implement logic based on your game's elemental rules
        switch (attackElement)
        {
            case ElementType.Fire:
            case ElementType.Water:
                return defenseElement == ElementType.Lightning || defenseElement == ElementType.Ice;
            case ElementType.Lightning:
            case ElementType.Ice:
                return defenseElement == ElementType.Fire || defenseElement == ElementType.Water;
            default:
                return false;
        }
    }

    private static bool IsWeakAgainst(ElementType attackElement, ElementType defenseElement)
    {
        // Implement logic based on your game's elemental rules
        switch (attackElement)
        {
            case ElementType.Fire:
                return defenseElement == ElementType.Water;
            case ElementType.Water:
                return defenseElement == ElementType.Fire;
            case ElementType.Lightning:
                return defenseElement == ElementType.Ice;
            case ElementType.Ice:
                return defenseElement == ElementType.Lightning;
            default:
                return false;
        }
    }

    public static BattleAction GetAction(string actionName)
    {
        if (battleActions.ContainsKey(actionName))
        {
            return battleActions[actionName];
        }

        return null;
    }

    public static string GetRandomSpell()
    {
        List<string> spellList = new List<string> { "Fire", "Lightning", "Water", "Ice" };
        return spellList[Random.Range(0, spellList.Count)];
    }

    private static void FillActionList()
    {
        battleActions.Add("Attack", BaseAttack);
        battleActions.Add("Fire", FireSpell);
        battleActions.Add("Lightning", LightningSpell);
        battleActions.Add("Water", WaterSpell);
        battleActions.Add("Ice", IceSpell);
    }
}