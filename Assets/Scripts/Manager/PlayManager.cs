using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private List<SOCharacter> enemiesToFight = new();

    public void SetEnemyToFight(SOCharacter[] enemies)
    {
        enemiesToFight.Clear();

        foreach (var item in enemies)
        {
            enemiesToFight.Add(item);
        }
    }

    public List<SOCharacter> GetEnemiesToFight()
    {
        return enemiesToFight;
    }
}
