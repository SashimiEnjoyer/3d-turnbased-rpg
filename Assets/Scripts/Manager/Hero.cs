using Animancer;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AttackPatternConfig
{
    [System.Serializable]
    public enum AttackType
    {
        Damage, Heal, Buff, Debuff, Stun, DOT
    }    

    public AttackType attackType;
    public bool isMultiTarget;
    public ClipTransition animation;

    public bool IsBuffCamera()
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

    public override void InitCharacter(SOCharacter data)
    {
        base.InitCharacter(data);
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

    public virtual void AttackPattern1(Character target, UnityAction onDone) { }
    public virtual void AttackPattern2(Character target, UnityAction onDone) { }
    public virtual void AttackedByEnemy(float rawValue) { }

    protected virtual void Attack1Callback() { }
    protected virtual void Attack2Callback() { }


    public virtual void AttackPattern3()
    {
        Debug.Log("attack 3");
    }


    public virtual void UltimatePattern()
    {
        Debug.Log("Ultimate!");
    }
}
