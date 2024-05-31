public class ActionResult {
    public enum ResultType
    {
        Damage,
        ElementAttack,
        Heal,
        Miss
    }

    public ResultType resultType;
    public int value = 0;
    public string message;
    public bool stun = false;
    public ElementEnum.ElementType element;
    public CharacterCombat user, target;

    public ActionResult(CharacterCombat user, ResultType resultType, int value)
    {
        this.user = user;
        this.resultType = resultType;
        this.value = value;
    }

    public void DoResult()
    {
        switch (resultType)
        {
            case ResultType.Damage:
                target.TakeDamage(value);
                break;
            case ResultType.ElementAttack:
                target.TakeDamage(value);
                break;
            case ResultType.Heal:
                target.Heal(value);
                break;
            case ResultType.Miss:
                
                break;
        }
    }
}