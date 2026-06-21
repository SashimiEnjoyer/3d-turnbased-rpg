using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
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

        arenaUiManage.InitActionButtons(()=> DoAttackPattern(0), () => DoAttackPattern(1), () => DoAttackPattern(2), () => DoAttackPattern(3)); 
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

        switch (currentFightTurn)
        {
            case FightTurn.EnemyTurn:
                arenaUiManage.SetHeroTurnPanelState(false);
                currentTurnEnemy = ch.GetComponent<Enemy>();

                currentTurnEnemy.DoAttack(NextSequence);

                break;
            case FightTurn.HeroTurn:

                currentTurnHero = ch.GetComponent<Hero>();
                arenaUiManage.SetHeroTurnPanelState(true);
                
                arenaUiManage.SetCurrentCharaUi(currentTurnHero);
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
        }
        CheckThisTurn();
    }

    public void BackToRoam()
    {
        SceneManager.LoadSceneAsync("Roam");
    }

    private int atkPatternIdx = 0;
    private bool isAttackPattern1;

    public void DoAttackPattern(int idx)
    {
        if(atkPatternIdx != idx)
        {
            AttackPatternConfig config = currentTurnHero.GetAttackConfig(idx);

            if(config.IsBuffCamera())
            {
                arenaCharacterController.GetCurrentTurnHero(currentTurnHero).GetCharacterContainer().SetActiveBuffCam();
            }
            else
            {
                arenaCharacterController.GetCurrentTurnHero(currentTurnHero).GetCharacterContainer().SetActiveBaseCam();
            }

            atkPatternIdx = idx;
        }
        else
        {
            currentTurnHero.AttackPattern1(NextSequence);
        }
    }
}
