using UnityEngine;

public class Hero : Character
{

    public override void InitCharacter(SOCharacter data)
    {
        base.InitCharacter(data);
    }

    public void AttackPattern1()
    {
        Debug.Log("attack 1");
    }

    public void AttackPattern2()
    {
        Debug.Log("attack 2");
    }

    public void AttackPattern3()
    {
        Debug.Log("attack 3");
    }

    public void UltimatePattern()
    {
        Debug.Log("Ultimate!");
    }
}
