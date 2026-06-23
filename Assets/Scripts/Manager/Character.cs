using Animancer;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] protected SOCharacter characterData;
    [SerializeField] protected AnimancerComponent animComponent;
    [SerializeField] protected Transform targetIndicator;

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
    public Transform GetTargetIndicator() { return targetIndicator; }


    public virtual void InitCharacter(SOCharacter data) 
    {
        maxHp = data.health;
        currentHp = maxHp;
        finalDmg = data.baseDamage;
        speed = data.baseSpeed;
    }

    public bool CheckIsFriend() => characterData.isFriend;
}
