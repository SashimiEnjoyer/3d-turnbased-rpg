using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArenaSequenceController : MonoBehaviour
{
    private CharacterSequence currentCharaSequence;
    private Character currentCharacter;
    private Hero currentHero;
    private Enemy currentEnemy;

    private int atkPatternIdx = 0;

    private UnityAction onDoneSequence;
    
    public void AssignCharacterForSequence(CharacterSequence chr, UnityAction onDone = null)
    {
        currentCharaSequence = chr;

        currentCharacter = currentCharaSequence.GetCharacter();

        if (currentCharacter.GetCharaDetail().isFriend)
            currentHero = currentCharacter as Hero;
        else
            currentEnemy = currentCharacter as Enemy;

        onDoneSequence = onDone;
    }

    public void ExecuteHeroAttack(int idx, Enemy target)
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
            currentHero.AttackPattern1(target, onDoneSequence);
        }
    }

    public void ExecuteEnemyAttack(Hero target)
    {
        currentEnemy.DoAttack(target, onDoneSequence);
    }

}
