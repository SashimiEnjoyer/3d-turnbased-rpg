

public class CharacterOneManager : Hero
{
    private AttackPatternConfig config;
    private int additionalDmg;

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
        targetedEnemy.AttackedByPlayer(config.value + additionalDmg, config.attackType);
        OnUpdateUi?.Invoke();
    }

    protected override void Attack2Callback()
    {
        targetedHero.AttackedByEnemy(config.value, config.attackType);
        CurrentMana -= config.cost;

        OnUpdateUi?.Invoke();
    }

    protected override void Attack3Callback()
    {
        targetedHero.AttackedByEnemy(config.value, config.attackType);
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
                    currentEffect = Instantiate(config.effect, targetedHero.GetEffectTarget());
                    Destroy(currentEffect, 2f);
                }
                OnUpdateUi?.Invoke();
                break;

                case AttackType.Buff:
                    additionalDmg += receivedValue;

                if (config.effect != null)
                {
                    currentEffect = Instantiate(config.effect, targetedHero.GetEffectTarget());
                    Destroy(currentEffect, 2f);
                }

                break;

            default:
                hitFx.Emit(1);
                CurrentHp -= receivedValue;

                OnUpdateUi?.Invoke();

                if (CurrentHp <= 0)
                {
                    isAlive = false;
                    speed = 0;
                    gameObject.SetActive(false);
                }
                break;
        }
        
    }
}
