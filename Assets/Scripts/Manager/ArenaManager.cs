using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FightTurn
{
    EnemyTurn,
    HeroTurn,
    Bussy
}

public class ArenaManager : MonoBehaviour
{
    [SerializeField] private ArenaUiManage arenaUiManage;
    [SerializeField] private ArenaCharactersController arenaCharacterController;
    [SerializeField] private ArenaSequenceController arenaSequenceController;

    private List<Enemy> enemies = new();
    private List<Hero> heroes = new();

    private Hero currentTurnHero;
    private Enemy currentTurnEnemy;

    private int currentSequenceIndex = 0;
    private int currentRound = 0;

    [SerializeField] private FightTurn currentFightTurn;
    public FightTurn CurrentFightTurn => currentFightTurn;

    private void Start()
    {
        int enemyCount = GameManager.instance.GetEnemiesToFight().Count;
        int heroCount = GameManager.instance.playerDataManager.playerData.ownedCharacters.Count;

        for (int i = 0; i < enemyCount; i++)
        {
            int temp = i;
            SOCharacter item = GameManager.instance.GetEnemiesToFight()[temp];
            Enemy enemy = Instantiate(item.characterPrefab).GetComponent<Enemy>();
            enemy.gameObject.name = $"Enemy {temp}";
            enemy.InitCharacter(item);
            enemies.Add(enemy);
            arenaUiManage.InitCharaSeqUi();
            arenaCharacterController.AddAllCharaInArena(enemy);
        }

        for (int i = 0; i < heroCount; i++)
        {
            int temp = i;
            SOCharacter item = GameManager.instance.playerDataManager.playerData.ownedCharacters[temp];
            Hero hero = Instantiate(item.characterPrefab).GetComponent<Hero>();
            hero.InitCharacter(item);
            heroes.Add(hero);
            arenaUiManage.InitCharaSeqUi();
            arenaCharacterController.AddAllCharaInArena(hero);

        }

        BuildSequence();
        CheckThisTurn();

        arenaUiManage.InitActionButtons(
            ()=> arenaSequenceController.ExecuteHeroAttack(0, arenaCharacterController.GetManualSelectedEnemy()), 
            () => arenaSequenceController.ExecuteHeroAttack(1, arenaCharacterController.GetManualSelectedEnemy()), 
            () => arenaSequenceController.ExecuteHeroAttack(2, arenaCharacterController.GetManualSelectedEnemy()), 
            () => arenaSequenceController.ExecuteHeroAttack(3, arenaCharacterController.GetManualSelectedEnemy())
            ); 

        arenaUiManage.InitSkipButtons(BackToRoam, NextSequence);   
    }

    private void BuildSequence()
    {
        List<Character> sortedChara = arenaCharacterController.GetSortedCharaSequence().Select(u => u.GetCharacter()).ToList();
        arenaUiManage.ArrangeSequenceUi(sortedChara);
    }

    public void CheckThisTurn()
    {
        Character ch = arenaCharacterController.GetSortedCharaSequence()[currentSequenceIndex].GetCharacter();
        currentFightTurn = ch.GetCharaDetail().isFriend ? FightTurn.HeroTurn : FightTurn.EnemyTurn;
        arenaCharacterController.CheckCurrentTurnStatus(currentFightTurn == FightTurn.HeroTurn);

        switch (currentFightTurn)
        {
            case FightTurn.EnemyTurn:
                arenaUiManage.SetHeroTurnPanelState(false);
                currentTurnEnemy = ch as Enemy;

                currentTurnEnemy.DoAttack(arenaCharacterController.GetDefaultHero() ,NextSequence);
                //arenaSequenceController.AssignCharacterForSequence(arenaCharacterController.GetCurrentTurnHero(currentTurnHero));
                break;
            case FightTurn.HeroTurn:

                currentTurnHero = ch as Hero;
                arenaUiManage.SetHeroTurnPanelState(true);
                
                arenaUiManage.SetCurrentCharaUi(currentTurnHero);
                arenaSequenceController.AssignCharacterForSequence(arenaCharacterController.GetCurrentTurnHero(currentTurnHero), NextSequence);
                break;
            default:
                arenaUiManage.SetHeroTurnPanelState(false);
                break;
        }
    }

    public void NextSequence()
    {
        currentSequenceIndex++;

        if (currentSequenceIndex >= arenaCharacterController.GetSortedCharaSequence().Count)
        {
            BuildSequence();
            currentSequenceIndex = 0;
            currentRound++;

            if (currentRound >= 3)
                BackToRoam();
        }

        CheckThisTurn();
    }

    public void BackToRoam()
    {
        SceneManager.LoadSceneAsync("Roam");
    }
}
