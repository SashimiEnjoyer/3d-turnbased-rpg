using UnityEngine;

public class Hero : Character
{
    public override void InitCharacter(SOCharacter data)
    {
        maxHp = data.health;
        baseDmg = data.baseDamage;
    }
}
