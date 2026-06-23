using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private List<SOCharacter> enemiesToFight = new();
    private int enemyIdxInsideRoam = -1;
    private bool wasWinningFromBattle = false;

    public void SetEnemyToFight(SOCharacter[] enemies)
    {
        enemiesToFight.Clear();

        foreach (var item in enemies)
        {
            enemiesToFight.Add(item);
        }
    }
    public int GetLastEnemyIdx() => enemyIdxInsideRoam;
    public bool CheckWasWinningBefore() => wasWinningFromBattle;
    public void UpdateEnemyDefeatIndex(int newIdx) => enemyIdxInsideRoam = newIdx;
    public void FlagWinningBattle(bool flag) => wasWinningFromBattle = flag;

    public List<SOCharacter> GetEnemiesToFight()
    {
        return enemiesToFight;
    }
}
