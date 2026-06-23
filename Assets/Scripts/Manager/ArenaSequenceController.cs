using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArenaSequenceController : MonoBehaviour
{
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
        }
        else
        {
            currentEnemy = currentCharacter as Enemy;
            currentHero = null;

            EnemyAtkSequence();
        }
    }

    public void ExecuteEnemyAttack(Hero target)
    {
        currentEnemy.DoAttack(target, onDoneSequence);
    }

    private void EnemyAtkSequence()
    {
        Hero h = arenaCharacterController.GetDefaultHero();
        ExecuteEnemyAttack(h);
        //onDoneSequence?.Invoke();
    }

    public void ExecuteHeroAttack(int idx)
    {
        if (atkPatternIdx != idx)   //Prepare Attack
        {
            AttackPatternConfig config = currentHero.GetAttackConfig(idx);

            if (config.IsBuffCamera())
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

            atkPatternIdx = idx;
        }
        else //Execute Attack
        {
            if(atkPatternIdx == 0)
                currentHero.AttackPattern1(arenaCharacterController.GetManualSelectedTarget(), onDoneSequence);
            else if(atkPatternIdx == 1)
                currentHero.AttackPattern2(arenaCharacterController.GetManualSelectedTarget(), onDoneSequence);
        }
    }
}
