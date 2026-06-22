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

    private int atkPatternIdx = 0;

    private UnityAction onDoneSequence;

    public void InitCurrentTurnCharacter(CharacterSequence chr, UnityAction onDone = null)
    {
        currentCharaSequence?.GetCharacterContainer().ResetAllCam();
        currentCharaSequence = chr;
        currentCharaSequence.GetCharacterContainer().SetupCurrentContainer();
        currentCharaSequence.GetCharacterContainer().SetActiveBaseCam();
        currentCharacter = currentCharaSequence.GetCharacter();
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
        if (atkPatternIdx != idx)
        {
            AttackPatternConfig config = currentHero.GetAttackConfig(idx);

            if (config.IsBuffCamera())
            {
                currentCharaSequence.GetCharacterContainer().SetActiveBuffCam();
            }
            else
            {
                currentCharaSequence.GetCharacterContainer().SetActiveBaseCam();
            }

            atkPatternIdx = idx;
        }
        else
        {
            currentHero.AttackPattern1(arenaCharacterController.GetManualSelectedEnemy(), onDoneSequence);
        }
    }
}
