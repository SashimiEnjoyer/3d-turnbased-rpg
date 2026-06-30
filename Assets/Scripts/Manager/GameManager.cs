using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InGameCharaDatabase charaDatabase;
    public PlayManager playManager;
    public PlayerDataManager playerDataManager;
    public PlayerInputHandler playerInputHandler;

    [SerializeField] private SOCharacter defaultChara;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void InitAllManagers()
    {
        playerDataManager.PlayerDataInit();
    }

    public List<SOCharacter> GetEnemiesToFight()
    {
        return playManager.GetEnemiesToFight();
    }

    public GameObject GetCharacterFromDatabase(SOCharacter chara)
    {
        return charaDatabase.GetCharacter(chara);
    }

    public GameObject GetEnemyFromDatabase(SOCharacter chara)
    {
        return charaDatabase.GetEnemy(chara);
    }

    public SOCharacter GetDefaultCharaDatabase()
    {
        return defaultChara;
    }

    public PlayerData GetPlayerData()
    {
        return playerDataManager.playerData;
    }

    public void SwitchActionMaps(string name) 
    {
        Debug.Log("Switch Action Maps -> " + name);
        playerInputHandler.SwitchActionMaps(name);
    }
}
