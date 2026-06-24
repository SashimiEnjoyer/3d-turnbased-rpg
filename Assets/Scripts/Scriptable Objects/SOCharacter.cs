using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Objects/Character")]
public class SOCharacter : ScriptableObject
{
    [Header("Character Info")]
    public string charaName;
    public Sprite charaSprite;
    public bool isFriend;

    [Header("Assets")]
    public GameObject characterPrefab;

    [Header("Stats")]
    public int health;
    public int mana;
    public int baseDamage;
    public int baseSpeed;
}
