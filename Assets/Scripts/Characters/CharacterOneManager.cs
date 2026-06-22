using Animancer;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterOneManager : Hero
{
    public override void AttackPattern1(Enemy target, UnityAction done)
    {
        targetedEnemy = target;
        onDoneAttack = done;
        animComponent.Play(attackPatternConfig[0].animation);
    }

    public override void AttackedByEnemy()
    {
        Debug.Log("Attacked by Enemy!");
    }

    private async void PlayAtk1Animation() 
    {
        await animComponent.Play(attackPatternConfig[0].animation);
        animComponent.Stop();
    }

}
