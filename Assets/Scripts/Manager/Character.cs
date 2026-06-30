using Animancer;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] protected SOCharacter characterData;
    [SerializeField] protected AnimancerComponent animComponent;
    [SerializeField] protected Transform targetIndicator;
    [SerializeField] protected ParticleSystem hitFx;
    [SerializeField] protected Transform effectTarget;

    [SerializeField] protected ClipTransition idleAnim;
    [SerializeField] protected ClipTransition hurtAnim;
    [SerializeField] protected ClipTransition dieAnim;

    protected int maxHp;
    private int currentHp;
    public int CurrentHp
    {
        set
        {
            currentHp = value;

            if (value < 0)
                currentHp = 0;
            else if(value > maxHp)
                currentHp = maxHp;
        }
        get 
        {
            return currentHp; 
        }
    }

    protected int maxMana;
    protected int currentMana;
    public int CurrentMana
    {
        set
        {
            currentMana = value;

            if (value < 0)
                currentMana = 0;
            else if (value > maxHp)
                currentMana = maxHp;
        }
        get
        {
            return currentMana;
        }
    }

    protected int finalDmg;
    protected int speed;
    protected bool isAlive = true;

    public int GetMaxHp() { return maxHp; }
    public int GetMaxMana() { return maxMana; }
    public int GetFinalDmg() { return finalDmg; }
    public int GetSpeed() { return speed; }
    public bool CheckIsAlive() { return isAlive; }
    public SOCharacter GetCharaDetail() { return characterData; }
    public Transform GetTargetIndicator() { return targetIndicator; }
    public Transform GetEffectTarget() {  return effectTarget; }

    public UnityAction OnUpdateUi;

    public virtual void InitCharacter(SOCharacter data, UnityAction onDone) 
    {
        maxHp = data.health;
        CurrentHp = maxHp;

        maxMana = data.mana;
        currentMana = data.mana;

        finalDmg = data.baseDamage;
        speed = data.baseSpeed;
    }

    public void PlayIdleAnim() => animComponent.Play(idleAnim);

    public bool CheckIsFriend() => characterData.isFriend;
}
