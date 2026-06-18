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
    public float health;
    public float baseDamage;
}
