using System.Collections.Generic;
using UnityEngine;
using static ActionResult;
using static ElementEnum;

public static class CharacterActionList
{
    private static Dictionary<string, CharacterAction.BattleAction> battleActions = new();

    static CharacterActionList()
    {
        FillActionList();
    }

    public static ActionResult BaseAttack(CharacterCombat user, CharacterCombat target)
    {
        string message;
        int damage = 0;
        bool stun = false;
        ResultType resultType = Random.Range(0, 100) < 90 ? ResultType.Damage : ResultType.Miss;

        if (resultType == ResultType.Damage)
        {
            damage = user.stats.GetAttack() - target.stats.GetDefense();

            if (Random.Range(0, 100) < 10)
            {
                stun = true;
                message = $"{user.name} stuns {target.name}!";
            }
            else
                message = $"{user.name} attacks {target.name}!";
            
        } else
        {
            message = $"{user.name} missed!";
        }

        ActionResult result = new(user, resultType, damage)
        {
            stun = stun,
            message = message,
            target = target
        };

        return result;
    }

    public static ActionResult FireSpell(CharacterCombat user, CharacterCombat target)
    {
        string message;
        int damage = 0;
        bool stun = false;

        ResultType resultType = Random.Range(0, 100) < 80 ? ResultType.ElementAttack : ResultType.Miss;

        if (resultType == ResultType.ElementAttack)
        {
            damage = (int)(user.stats.GetMagic() * 1.5f) - target.stats.GetResistance();

            if (target.affectedElement == ElementType.Lightning || target.affectedElement == ElementType.Ice)
            {
                damage += (int)(damage * 1.5f);
                stun = true;
                message = $"{user.name} stuns {target.name}!";
            }
            else if (target.affectedElement == ElementType.Water)
            {
                damage += (int)(damage * 0.5f);
                message = $"{user.name} weakly hit {target.name}!";
            }
            else
            {
                message = $"{user.name} casts Fire on {target.name}!";
            }
        }
        else
        {
            message = $"{user.name} missed!";
        }

        ActionResult result = new(user, resultType, damage)
        {
            stun = stun,
            message = message,
            target = target,
            element = ElementType.Fire
        };

        return result;
    }

    public static ActionResult LightningSpell(CharacterCombat user, CharacterCombat target)
    {
        string message;
        int damage = 0;
        bool stun = false;

        ResultType resultType = Random.Range(0, 100) < 80 ? ResultType.ElementAttack : ResultType.Miss;

        if (resultType == ResultType.ElementAttack)
        {
            damage = (int)(user.stats.GetMagic() * 1.5f) - target.stats.GetResistance();

            if (target.affectedElement == ElementType.Water || target.affectedElement == ElementType.Fire)
            {
                damage += (int)(damage * 1.5f);
                stun = true;
                message = $"{user.name} stuns {target.name}!";
            }
            else if (target.affectedElement == ElementType.Ice)
            {
                damage += (int)(damage * 0.5f);
                message = $"{user.name} weakly hit {target.name}!";
            }
            else
            {
                message = $"{user.name} casts Lightning on {target.name}!";
            }
        }
        else
        {
            message = $"{user.name} missed!";
        }

        ActionResult result = new(user, resultType, damage)
        {
            stun = stun,
            message = message,
            target = target,
            element = ElementType.Lightning
        };

        return result;
    }

    public static ActionResult WaterSpell(CharacterCombat user, CharacterCombat target)
    {
        string message;
        int damage = 0;
        bool stun = false;
        ResultType resultType = ResultType.ElementAttack;
    
        damage = user.stats.GetMagic() - target.stats.GetResistance();

        if (target.affectedElement == ElementType.Ice || target.affectedElement == ElementType.Lightning)
        {
            damage += (int)(damage * 1.5f);
            stun = true;
            message = $"{user.name} stuns {target.name}!";
        }
        else if (target.affectedElement == ElementType.Fire)
        {
            damage += (int)(damage * 0.5f);
            message = $"{user.name} weakly hit {target.name}!";
        }
        else
        {
            message = $"{user.name} casts Water on {target.name}!";
        }

        ActionResult result = new(user, resultType, damage)
        {
            stun = stun,
            message = message,
            target = target,
            element = ElementType.Water
        };

        return result;
    }

    public static ActionResult IceSpell(CharacterCombat user, CharacterCombat target)
    {
        string message;
        int damage = 0;
        bool stun = false;
        ResultType resultType = ResultType.ElementAttack;
    
        damage = user.stats.GetMagic() - target.stats.GetResistance();

        if (target.affectedElement == ElementType.Fire || target.affectedElement == ElementType.Water)
        {
            damage += (int)(damage * 1.5f);
            stun = true;
            message = $"{user.name} stuns {target.name}!";
        }
        else if (target.affectedElement == ElementType.Lightning)
        {
            damage += (int)(damage * 0.5f);
            message = $"{user.name} weakly hit {target.name}!";
        }
        else
        {
            message = $"{user.name} casts Ice on {target.name}!";
        }

        ActionResult result = new(user, resultType, damage)
        {
            stun = stun,
            message = message,
            target = target,
            element = ElementType.Ice
        };

        return result;
    }
    

    public static CharacterAction.BattleAction GetAction(string actionName)
    {
        if (battleActions.ContainsKey(actionName))
        {
            return battleActions[actionName];
        }

        return null;
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