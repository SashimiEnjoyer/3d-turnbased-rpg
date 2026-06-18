
public class Enemy : Character
{
    public override void InitCharacter(SOCharacter data)
    {
        maxHp = data.health;
        baseDmg = data.baseDamage;
    }


}
