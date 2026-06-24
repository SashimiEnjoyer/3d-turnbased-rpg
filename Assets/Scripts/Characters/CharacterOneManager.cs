using Animancer;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CharacterOneManager : Hero
{
    AttackPatternConfig config;
    public override void AttackPattern(int id, Character target)
    {
        config = attackPatternConfig[id];

        if (config.IsBuffTeamate())
        {
            targetedHero = target as Hero;
            targetedEnemy = null;
        }
        else
        {
            targetedHero = null;
            targetedEnemy = target as Enemy;
        }
        animComponent.Play(config.animation);
    }

    protected override void Attack1Callback()
    {
        CurrentMana += 1;
        targetedEnemy.AttackedByPlayer(config.value, config.attackType);
        OnUpdateUi?.Invoke();
    }

    protected override void Attack2Callback()
    {
        targetedHero.AttackedByEnemy(1, config.attackType);
        CurrentMana -= config.cost;

        OnUpdateUi?.Invoke();
    }

    public override void AttackedByEnemy(int receivedValue, AttackType type)
    {
        switch (type)
        {
            case AttackType.Heal:
                CurrentHp += receivedValue;
                OnUpdateUi?.Invoke();
                break;

            default:
                hitFx.Emit(1);
                CurrentHp -= receivedValue;

                OnUpdateUi?.Invoke();

                if (CurrentHp <= 0)
                {
                    isAlive = false;
                    gameObject.SetActive(false);
                }
                break;
        }
        
    }
}
