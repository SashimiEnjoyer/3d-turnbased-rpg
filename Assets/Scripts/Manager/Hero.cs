using Animancer;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public enum AttackType
{
    Damage, Heal, Buff, Debuff, Stun, DOT
}

[System.Serializable]
public class AttackPatternConfig
{
    public string name;
    public Sprite attackImage;
    public AttackType attackType;
    public int value;
    public int cost;
    public bool isMultiTarget;
    public ClipTransition animation;

    public bool IsBuffTeamate()
    {
        return attackType == AttackType.Buff || attackType == AttackType.Heal;
    }
}

public class Hero : Character
{
    [SerializeField] protected AttackPatternConfig[] attackPatternConfig;

    protected Enemy targetedEnemy;
    protected Hero targetedHero;
    protected UnityAction onDoneAttack;

    public override void InitCharacter(SOCharacter data, UnityAction onDone)
    {
        base.InitCharacter(data, onDone);
        onDoneAttack += onDone;
        OptionalWarning.EndEventInterrupt.Disable();
        
        attackPatternConfig[0].animation.Events.AddCallback(0, Attack1Callback);
        attackPatternConfig[0].animation.Events.OnEnd = ()=>
        {
            Debug.Log("Animation Attack 1 Ended");
            onDoneAttack?.Invoke();
            animComponent.Stop();
        };

        attackPatternConfig[1].animation.Events.AddCallback(0, Attack2Callback);
        attackPatternConfig[1].animation.Events.OnEnd = () =>
        {
            Debug.Log("Animation Attack 2 Ended");
            onDoneAttack?.Invoke();
            animComponent.Stop();
        };
    }

    public AttackPatternConfig GetAttackConfig(int idx)
    {
        return attackPatternConfig[idx];
    }

    public AttackPatternConfig[] GetAllAttackConfig() => attackPatternConfig;

    public virtual void AttackPattern(int id, Character target) { }
    public virtual void AttackedByEnemy(int recievedValue, AttackType type) { }

    protected virtual void Attack1Callback() { }
    protected virtual void Attack2Callback() { }

}
