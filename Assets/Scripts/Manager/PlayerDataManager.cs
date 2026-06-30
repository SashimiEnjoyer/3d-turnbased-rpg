
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public SOCharacter characterDetail;
    public int level;
    public int expAmount;
    public float finalHealth;
    public float finalDamage;
}

public class PlayerData 
{
    public string playerName;
    public int playerLevel;
    public List<SOCharacter> ownedCharacters = new ();

    public PlayerData(string name, int lvl, SOCharacter chara)
    {
        playerName = name;
        playerLevel = lvl;
        ownedCharacters.Add(chara);
    }
}

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;
    public int currentCharacterIndex = 0;

    public void PlayerDataInit()
    {
        playerData = new PlayerData("Player", 1, GameManager.instance.GetDefaultCharaDatabase());
    }
}
