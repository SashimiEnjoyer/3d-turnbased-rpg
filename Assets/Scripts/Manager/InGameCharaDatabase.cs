using System.Linq;
using UnityEngine;

[System.Serializable]
public struct CharaDatabase 
{
    public SOCharacter charaDetail;
    public GameObject prefab;
}

public class InGameCharaDatabase : MonoBehaviour
{
    [SerializeField] private CharaDatabase[] characterDatabase;
    [SerializeField] private CharaDatabase[] enemyDatabase;

    public CharaDatabase[] CharacterDatabase => characterDatabase;
    public CharaDatabase[] EnemyDatabase => enemyDatabase;

    public GameObject GetCharacter(SOCharacter chara)
    {
        return characterDatabase.First(x => x.charaDetail == chara).prefab;
    }

    public GameObject GetEnemy(SOCharacter enemy)
    {
        return enemyDatabase.First(x => x.charaDetail == enemy).prefab;
    }

}
