

using System.Collections.Generic;

public class CharacterOneManager : Hero
{
    private AttackPatternConfig config;
    private int additionalDmg;

    public override void AttackPattern(int id, List<Character> targets)
    {
        config = attackPatternConfig[id];

        if (config.IsBuffTeamate())
        {
            targetedHeroes.Clear();
            targetedEnemies.Clear();
            foreach (var item in targets)
            {
                if(item is Hero)
                    targetedHeroes.Add(item as Hero);
            }
        }
        else
        {
            targetedHeroes.Clear();
            targetedEnemies.Clear();
            foreach (var item in targets)
            {
                if (item is Enemy)
                    targetedEnemies.Add(item as Enemy);
            }
        }
        animComponent.Play(config.animation);
    }

    protected override void Attack1Callback()
    {
        CurrentMana += 1;

        foreach (var item in targetedEnemies)
        {
            item.AttackedByPlayer(config.value + additionalDmg, config.attackType);
        }
        OnUpdateUi?.Invoke();
    }

    protected override void Attack2Callback()
    {
        foreach (var item in targetedHeroes)
        {
            item.AttackedByEnemy(config.value, config.attackType);
        }

        CurrentMana -= config.cost;

        OnUpdateUi?.Invoke();
    }

    protected override void Attack3Callback()
    {
        foreach (var item in targetedHeroes)
        {
            item.AttackedByEnemy(config.value, config.attackType);
        }

        CurrentMana -= config.cost;

        OnUpdateUi?.Invoke();
    }

    protected override void UltCallback()
    {
        foreach (var item in targetedEnemies)
        {
            item.AttackedByPlayer(config.value + additionalDmg, config.attackType);

            if (config.effect != null)
            {
                currentEffect = Instantiate(config.effect);
                currentEffect.transform.position = item.GetEffectTarget().position;
                Destroy(currentEffect, 2f);
            }
        }
        CurrentMana -= config.cost;

        OnUpdateUi?.Invoke();
    }

    public override void AttackedByEnemy(int receivedValue, AttackType type)
    {
        switch (type)
        {
            case AttackType.Heal:
                CurrentHp += receivedValue;

                if (config.effect != null)
                {
                    foreach (var item in targetedHeroes)
                    {
                        currentEffect = Instantiate(config.effect);
                        currentEffect.transform.position = item.GetEffectTarget().position;
                        Destroy(currentEffect, 2f);
                    }
                }

                OnUpdateUi?.Invoke();

                break;

                case AttackType.Buff:
                    additionalDmg += receivedValue;

                if (config.effect != null)
                {
                    foreach (var item in targetedHeroes)
                    {
                        currentEffect = Instantiate(config.effect);
                        currentEffect.transform.position = item.GetEffectTarget().position;
                        Destroy(currentEffect, 2f);
                    }
                }

                break;

            default:
                hitFx.Emit(1);
                CurrentHp -= receivedValue;
                animComponent.Play(hurtAnim);

                OnUpdateUi?.Invoke();

                if (CurrentHp <= 0)
                {
                    isAlive = false;
                    speed = 0;
                    animComponent.Play(dieAnim);
                }
                break;
        }
        
    }
}
