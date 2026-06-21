using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterOneManager : Hero
{
    public override void AttackPattern1(UnityAction onDoneAttack)
    {
        StartCoroutine(DoAttack(onDoneAttack));
    }

    private IEnumerator DoAttack(UnityAction onDoneAttack)
    {
        Debug.Log("Do Attack 1");
        yield return new WaitForSeconds(2f);
        Debug.Log("Finish Attack 1");
        onDoneAttack();
    }
}
