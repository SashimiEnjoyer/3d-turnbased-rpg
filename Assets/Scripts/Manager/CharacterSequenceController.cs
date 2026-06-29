using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSequenceController : MonoBehaviour
{
    [SerializeField] private ArenaUiManage uiManager;
    [SerializeField] private ArenaCharactersController arenaCharacterController;

    public ArenaCharactersController ArenaCharacterController => arenaCharacterController;

    private CharacterSequence currentCharaSequence;
    private Character currentCharacter;
    private Hero currentHero;
    private Enemy currentEnemy;

    private int atkPatternIdx = -1;

    private UnityAction onDoneSequence;

    private void Awake()
    {
        arenaCharacterController.AssignOnSelectTarget(() => currentCharaSequence.GetCharacterContainer().SetBaseCamTarget(arenaCharacterController.GetManualSelectedTarget().GetTargetIndicator()));
    }

    public void InitCurrentTurnCharacter(CharacterSequence chr, UnityAction onDone = null)
    {
        atkPatternIdx = -1;
        currentCharaSequence?.GetCharacterContainer().ResetAllCam();
        currentCharaSequence = chr;
        currentCharacter = currentCharaSequence.GetCharacter();
        currentCharaSequence.GetCharacterContainer().SetupCurrentContainer();
        currentCharaSequence.GetCharacterContainer().SetActiveBaseCam();
        onDoneSequence = onDone;

        if (currentCharacter.GetCharaDetail().isFriend)
        {
            currentHero = currentCharacter as Hero;
            currentEnemy = null;
            ExecuteHeroAttackSequence(0);
        }
        else
        {
            currentEnemy = currentCharacter as Enemy;
            currentHero = null;

            EnemyAtkSequence();
        }
    }

    public void ExecuteEnemyAttackSequence(Hero target)
    {
        currentEnemy.DoAttack(target);
    }

    private void EnemyAtkSequence()
    {
        Hero h = arenaCharacterController.GetDefaultHero();
        ExecuteEnemyAttackSequence(h);
    }

    public void ExecuteHeroAttackSequence(int idx)
    {
        AttackPatternConfig config = currentHero.GetAttackConfig(idx);

        if (atkPatternIdx != idx)   //Prepare Attack
        {

            if (config.IsBuffTeamate())
            {
                currentCharaSequence.GetCharacterContainer().SetActiveBuffCam();
                arenaCharacterController.SetTargetTypeEnemy(false);
            }
            else
            {
                currentCharaSequence.GetCharacterContainer().SetActiveBaseCam();
                arenaCharacterController.SetTargetTypeEnemy(true);
                currentCharaSequence.GetCharacterContainer().SetBaseCamTarget(arenaCharacterController.GetManualSelectedTarget().GetTargetIndicator());
            }
            uiManager.SetActiveButtonIndicator(idx);
            atkPatternIdx = idx;
        }
        else //Execute Attack
        {
            if (currentHero.CurrentMana >= config.cost)
            {
                if (config.isMultiTarget)
                {
                    List<Character> temp = new();
                    foreach (CharacterSequence c in arenaCharacterController.GetSortedCharaSequence()) { temp.Add(c.GetCharacter()); }
                    currentHero.AttackPattern(atkPatternIdx, temp);
                }
                else
                {
                    List<Character> temp = new();
                    temp.Add(arenaCharacterController.GetManualSelectedTarget());
                    currentHero.AttackPattern(atkPatternIdx, temp);
                }
            }
        }
    }
}
