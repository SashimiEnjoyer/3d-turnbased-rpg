using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InGameCharaDatabase charaDatabase;
    public PlayManager playManager;
    public PlayerDataManager playerDataManager;

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

    public GameObject GetCharacter(SOCharacter chara)
    {
        return charaDatabase.GetCharacter(chara);
    }

    public GameObject GetEnemy(SOCharacter chara)
    {
        return charaDatabase.GetEnemy(chara);
    }

    public SOCharacter GetDefaultChara()
    {
        return defaultChara;
    }

    public PlayerData GetPlayerData()
    {
        return playerDataManager.playerData;
    }
}
