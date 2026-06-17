using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected SOCharacter characterData;

    protected float maxHp;
    protected float baseDmg;

    private void Awake()
    {
        maxHp = characterData.health;
        baseDmg = characterData.baseDamage;
    }

    public virtual void InitCharacter() { }
}
