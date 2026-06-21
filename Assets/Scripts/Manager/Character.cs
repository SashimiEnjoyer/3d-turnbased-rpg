using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected SOCharacter characterData;

    protected float maxHp;
    protected float currentHp;

    protected float finalDmg;
    protected int speed;
    protected bool isAlive = true;

    public float GetMaxHp() { return maxHp; }
    public float GetCurrentHp() { return currentHp; }
    public float GetFinalDmg() { return finalDmg; }
    public int GetSpeed() { return speed; }
    public bool CheckIsAlive() { return isAlive; }
    public SOCharacter GetCharaDetail() { return characterData; }

    private void Awake()
    {
        maxHp = characterData.health;
        finalDmg = characterData.baseDamage;
    }

    public virtual void InitCharacter(SOCharacter data) 
    {
        maxHp = data.health;
        finalDmg = data.baseDamage;
        speed = data.baseSpeed;
    }
}
