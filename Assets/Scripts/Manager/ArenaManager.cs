using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] private Transform[] charactersPoints;
    [SerializeField] private Transform[] enemiesPoints;

    [SerializeField] private ArenaUiManage arenaUiManage;

    private List<Enemy> enemies = new();
    private List<Hero> heroes = new();

    [SerializeField] private List<Character> charaSequences = new();
    private int currentSequenceIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < GameManager.instance.GetEnemiesToFight().Count; i++)
        {
            int temp = i;
            SOCharacter item = GameManager.instance.GetEnemiesToFight()[temp];
            Enemy enemy = Instantiate(item.characterPrefab, enemiesPoints[temp]).GetComponent<Enemy>();
            enemy.InitCharacter(item);
            enemies.Add(enemy);
            arenaUiManage.InitCharaSeqUi(enemy);
            charaSequences.Add(enemy);
        }

        for (int i = 0; i < GameManager.instance.playerDataManager.playerData.ownedCharacters.Count; i++)
        {
            int temp = i;
            SOCharacter item = GameManager.instance.playerDataManager.playerData.ownedCharacters[temp];
            Hero hero = Instantiate(item.characterPrefab, charactersPoints[temp]).GetComponent<Hero>();
            hero.InitCharacter(item);
            heroes.Add(hero);
            arenaUiManage.InitCharaSeqUi(hero);
            charaSequences.Add(hero);
        }

        charaSequences.Sort((x, y) => y.GetSpeed().CompareTo(x.GetSpeed()));
    }

    public void BackToRoam()
    {

    }
}
