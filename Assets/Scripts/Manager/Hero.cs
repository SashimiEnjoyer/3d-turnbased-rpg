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
    protected UnityAction onDoneAttack;

    public override void InitCharacter(SOCharacter data)
    {
        base.InitCharacter(data);
        OptionalWarning.EndEventInterrupt.Disable();
        attackPatternConfig[0].animation.Events.AddCallback(0, Attack1Callback);
        attackPatternConfig[0].animation.Events.OnEnd = ()=>
        {
            Debug.Log("Animation Ended");
            onDoneAttack?.Invoke();
            animComponent.Stop();
        };
    }

    public AttackPatternConfig GetAttackConfig(int idx)
    {
        return attackPatternConfig[idx];
    }

    public virtual void AttackPattern1(Enemy target, UnityAction onDone) { }
    public virtual void AttackedByEnemy() { }

    private void Attack1Callback()
    {
        Debug.Log("Call back attack " + targetedEnemy.name);
        targetedEnemy.AttackedByPlayer();
    }

    public virtual void AttackPattern3()
    {
        Debug.Log("attack 3");
    }


    public virtual void UltimatePattern()
    {
        Debug.Log("Ultimate!");
    }
}
