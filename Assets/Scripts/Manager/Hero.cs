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

    public bool IsBuffCamera()
    {
        return attackType == AttackType.Buff || attackType == AttackType.Heal;
    }
}

public class Hero : Character
{
    [SerializeField] private AttackPatternConfig[] attackPatternConfig;

    public override void InitCharacter(SOCharacter data)
    {
        base.InitCharacter(data);
    }

    public AttackPatternConfig GetAttackConfig(int idx)
    {
        return attackPatternConfig[idx];
    }


    public virtual void AttackPattern1(UnityAction onDoneAttack)
    {
        Debug.Log("attack 1 parent");
    }

    public virtual void AttackPattern2()
    {
        Debug.Log("attack 2");
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
