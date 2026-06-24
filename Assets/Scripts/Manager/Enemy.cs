using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    [SerializeField] private AttackPatternConfig[] attackConfigs;

    private UnityAction onDoneAttack;
    protected Hero targetedHero;

    public override void InitCharacter(SOCharacter data)
    {
        base.InitCharacter(data);

        attackConfigs[0].animation.Events.AddCallback(0, Attack1Callback);
        attackConfigs[0].animation.Events.OnEnd = () =>
        {
            Debug.Log("Animation Ended");
            onDoneAttack?.Invoke();
            animComponent.Stop();
        };
    }

    public void DoAttack(Hero target, UnityAction onDone)
    {
        targetedHero = target;
        onDoneAttack = onDone;
        animComponent.Play(attackConfigs[0].animation);
    }

    public void AttackedByPlayer(int rawValue)
    {
        hitFx.Emit(1);
        CurrentHp -= rawValue;

        OnUpdateUi?.Invoke();

        if (CurrentHp < 0)
        {
            isAlive = false;
            gameObject.SetActive(false);
        }
    }

    private void Attack1Callback()
    {
        targetedHero.AttackedByEnemy(2);
    }
}
