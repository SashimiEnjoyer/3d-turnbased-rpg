using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterOneManager : Hero
{
    public override void AttackPattern1(Enemy target, UnityAction onDoneAttack)
    {
        StartCoroutine(DoAttack(target, onDoneAttack));
    }

    private IEnumerator DoAttack(Enemy target, UnityAction onDoneAttack)
    {
        Debug.Log("Do Attack 1 to " + target.name);
        yield return new WaitForSeconds(2f);
        Debug.Log("Finish Attack 1");
        onDoneAttack?.Invoke();
    }
}
