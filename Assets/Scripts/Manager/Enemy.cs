using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    public override void InitCharacter(SOCharacter data)
    {
        base.InitCharacter(data);
    }

    public void DoAttack(UnityAction onDoneAttack)
    {
        StartCoroutine(AttackSequence(onDoneAttack));
    }

    private IEnumerator AttackSequence(UnityAction onDoneAttack)
    {
        Debug.Log("Enemy Attack 1");
        yield return new WaitForSeconds(2f);
        Debug.Log("Finish Enemy Attack 1");
        onDoneAttack();
    }
}
