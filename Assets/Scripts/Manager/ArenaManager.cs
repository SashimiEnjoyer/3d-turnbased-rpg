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
    [SerializeField] private int maxTurn;
    [SerializeField] private ArenaUiManage arenaUiManage;
    [SerializeField] private ArenaSequenceController arenaSequenceController;

    private List<Enemy> enemies = new();
    private List<Hero> heroes = new();

    //private List<CharacterSequence> sortedCharacterSequence;

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
            enemy.InitCharacter(item, NextSequence);
            enemies.Add(enemy);
            arenaUiManage.AssignEnemyDetailUi(temp, enemy);
            arenaUiManage.InitCharaSeqUi();
            arenaSequenceController.ArenaCharacterController.AddAllCharaInArena(enemy);
        }

        for (int i = 0; i < heroCount; i++)
        {
            int temp = i;
            SOCharacter item = GameManager.instance.playerDataManager.playerData.ownedCharacters[temp];
            Hero hero = Instantiate(item.characterPrefab).GetComponent<Hero>();
            hero.InitCharacter(item, NextSequence);
            heroes.Add(hero);
            arenaUiManage.AssignHeroDetailUi(temp, hero);
            arenaUiManage.InitCharaSeqUi();
            arenaSequenceController.ArenaCharacterController.AddAllCharaInArena(hero);

        }

        BuildSequence();
        CheckThisTurn();

        arenaUiManage.InitActionButtons(
            ()=> arenaSequenceController.ExecuteHeroAttackSequence(0), 
            () => arenaSequenceController.ExecuteHeroAttackSequence(1), 
            () => arenaSequenceController.ExecuteHeroAttackSequence(2), 
            () => arenaSequenceController.ExecuteHeroAttackSequence(3)
            ); 

        arenaUiManage.InitSkipButtons(arenaUiManage.ActiveLosePanel, NextSequence);   
    }

    private void BuildSequence()
    {
        var getSortedCharacter = arenaSequenceController.ArenaCharacterController.GetSortedCharaSequence().Select(u => u.GetCharacter()).ToList();
        arenaUiManage.ArrangeSequenceUi(getSortedCharacter);
    }

    public void CheckThisTurn()
    {
        Character ch = arenaSequenceController.ArenaCharacterController.GetSortedCharaSequence()[currentSequenceIndex].GetCharacter();
        
        if(!ch.CheckIsAlive())
        {
            NextSequence();
            return;
        }

        currentFightTurn = ch.GetCharaDetail().isFriend ? FightTurn.HeroTurn : FightTurn.EnemyTurn;
        arenaSequenceController.ArenaCharacterController.CheckCurrentTurnStatus(currentFightTurn == FightTurn.HeroTurn);

        switch (currentFightTurn)
        {
            case FightTurn.EnemyTurn:
                arenaUiManage.SetHeroTurnPanelState(false);
                currentTurnEnemy = ch as Enemy;
                currentTurnHero = null;
                arenaSequenceController.InitCurrentTurnCharacter(arenaSequenceController.ArenaCharacterController.GetSortedCharaSequence()[currentSequenceIndex], NextSequence);
                break;
            case FightTurn.HeroTurn:

                currentTurnHero = ch as Hero;
                currentTurnEnemy = null;

                arenaUiManage.SetHeroTurnPanelState(true);
                arenaUiManage.SetButtonSkillDetail(currentTurnHero.GetAllAttackConfig());
                
                arenaSequenceController.InitCurrentTurnCharacter(arenaSequenceController.ArenaCharacterController.GetSortedCharaSequence()[currentSequenceIndex], NextSequence);
                break;
            default:
                arenaUiManage.SetHeroTurnPanelState(false);
                break;
        }

        arenaUiManage.SetCurrentTurnText(currentRound);
    }

    public void NextSequence()
    {
        if (currentTurnHero != null && !arenaSequenceController.ArenaCharacterController.CheckAllEnemyAlive())
        {
            arenaUiManage.ActiveWinPanel();
            return;
        }else if (currentTurnEnemy != null && !arenaSequenceController.ArenaCharacterController.CheckAllHeroAlive())
        {
            arenaUiManage.ActiveLosePanel();
            return;
        }

        currentSequenceIndex++;

        if (currentSequenceIndex >= arenaSequenceController.ArenaCharacterController.GetSortedCharaSequence().Count)
        {
            BuildSequence();
            currentSequenceIndex = 0;
            currentRound++;

            if (currentRound >= maxTurn)
            {
                arenaUiManage.ActiveLosePanel();
                return;
            }
        }

        CheckThisTurn();
    }

    public void BackToRoamAfterWinning()
    {
        GameManager.instance.playManager.FlagWinningBattle(true);
        GameManager.instance.playManager.UpdateEnemyDefeatIndex(0);
        SceneManager.LoadSceneAsync("Roam");

    }

    public void BackToRoamAfterLosing()
    {
        GameManager.instance.playManager.UpdateEnemyDefeatIndex(-1);
        SceneManager.LoadSceneAsync("Roam");
    }

    public void RestartFight()
    {
        SceneManager.LoadSceneAsync("Fight Arena");
    }
}
