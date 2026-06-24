using Animancer;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterOneManager : Hero
{
    Character tar;
    public override void AttackPattern1(Character target, UnityAction done)
    {
        targetedEnemy = target as Enemy;
        onDoneAttack = done;
        animComponent.Play(attackPatternConfig[0].animation);
    }

    public override void AttackPattern2(Character target, UnityAction onDone)
    {
        tar = target;
        onDoneAttack = onDone;
        animComponent.Play(attackPatternConfig[1].animation);
    }

    protected override void Attack1Callback()
    {
        targetedEnemy.AttackedByPlayer(2);
    }

    protected override void Attack2Callback()
    {
        CurrentHp += 1;
        CurrentMana -= 1;
    }

    public override void AttackedByEnemy(int rawValue)
    {
        CurrentHp -= rawValue;
        if (CurrentHp < 0)
        {
            isAlive = false;
            gameObject.SetActive(false);
        }
    }
}
