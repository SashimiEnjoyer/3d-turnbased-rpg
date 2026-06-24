using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    [SerializeField] private AttackPatternConfig[] attackConfigs;

    private UnityAction onDoneAttack;
    protected Hero targetedHero;

    public override void InitCharacter(SOCharacter data, UnityAction onDone)
    {
        base.InitCharacter(data, onDone);

        onDoneAttack += onDone;

        attackConfigs[0].animation.Events.AddCallback(0, Attack1Callback);
        attackConfigs[0].animation.Events.OnEnd = () =>
        {
            Debug.Log("Animation Ended");
            onDoneAttack?.Invoke();
            animComponent.Stop();
        };
    }

    public void DoAttack(Hero target)
    {
        targetedHero = target;
        animComponent.Play(attackConfigs[0].animation);
    }

    public void AttackedByPlayer(int recievedValue, AttackType type)
    {
        switch (type)
        {
            default:
                hitFx.Emit(1);
                CurrentHp -= recievedValue;

                OnUpdateUi?.Invoke();

                if (CurrentHp < 0)
                {
                    isAlive = false;
                    gameObject.SetActive(false);
                }
                break;
        }
        
    }

    private void Attack1Callback()
    {
        targetedHero.AttackedByEnemy(2, attackConfigs[0].attackType);
    }
}
