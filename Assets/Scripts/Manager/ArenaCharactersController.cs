using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class CharacterSequence
{
    [SerializeField] private Character character;
    [SerializeField] private ArenaCharacterContainer characterContainer;

    public CharacterSequence(Character chr, ArenaCharacterContainer cntr)
    {
        character = chr;
        characterContainer = cntr;
    }

    public Character GetCharacter() => character;
    public ArenaCharacterContainer GetCharacterContainer() => characterContainer;

}

public class ArenaCharactersController : MonoBehaviour
{
    [SerializeField] private TargetIndicatorUi targetIndicatorUi;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask heroLayer;
    [SerializeField] private ArenaCharacterContainer[] charactersPoints;
    [SerializeField] private ArenaCharacterContainer[] enemiesPoints;

    private int characterPlacementIdx = 0;
    private int enemyPlacementIdx = 0;

    private List<CharacterSequence> allCharaInArena = new();
    private List<CharacterSequence> sortedCharaSequences = new();

    private CharacterSequence currentHeroTurn;
    private CharacterSequence currentEnemyTurn;

    private bool isHeroTurn = false;
    private bool isTargetingEnemy = false;

    private Character selectedTarget;

    public UnityAction OnManuallySelectTarget;

    public void AssignOnSelectTarget(UnityAction action)
    {
        OnManuallySelectTarget += action;
    }

    public void AddAllCharaInArena(Character chara)
    {
        if (chara is Hero)
        {
            chara.transform.parent = charactersPoints[characterPlacementIdx].transform;
            chara.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            allCharaInArena.Add(new CharacterSequence(chara, charactersPoints[characterPlacementIdx]));
            characterPlacementIdx++;
        }
        else
        {
            chara.transform.parent = enemiesPoints[enemyPlacementIdx].transform;
            chara.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            allCharaInArena.Add(new CharacterSequence(chara, enemiesPoints[enemyPlacementIdx]));
            enemyPlacementIdx++;
        }
    }

    public List<CharacterSequence> GetSortedCharaSequence()
    {
        sortedCharaSequences.Clear();

        return sortedCharaSequences = allCharaInArena
            .OrderByDescending(u => u.GetCharacter().GetSpeed()).ToList();
    }

    public bool CheckAllHeroAlive()
    {
        int count = allCharaInArena.Count(x => x.GetCharacter() as Hero && x.GetCharacter().CheckIsAlive());
        return count > 0;
    }

    public bool CheckAllEnemyAlive()
    {
        int count = allCharaInArena.Count(x => x.GetCharacter() as Enemy && x.GetCharacter().CheckIsAlive());
        return count > 0;
    }

    public ArenaCharacterContainer GetSelectedEnemy()
    {
        return sortedCharaSequences.FirstOrDefault(u => u.GetCharacter() is Enemy)?.GetCharacterContainer();
    }

    public CharacterSequence GetCurrentTurnHero(Hero myHero)
    {
        if (currentHeroTurn == null || currentHeroTurn.GetCharacter() as Hero == myHero)
            return currentHeroTurn = allCharaInArena.Find(u => u.GetCharacter() as Hero == myHero);
        else
            return currentHeroTurn;
    }
    public CharacterSequence GetCurrentTurnEnemy(Enemy enemy)
    {
        if (currentEnemyTurn == null || currentEnemyTurn.GetCharacter() as Enemy == enemy)
            return currentEnemyTurn = allCharaInArena.Find(u => u.GetCharacter() as Enemy == enemy);
        else
            return currentEnemyTurn;
    }

    public ArenaCharacterContainer GetHeroForEnemy(Hero myHero)
    {
        return sortedCharaSequences.FirstOrDefault(u => u.GetCharacter() is Hero)?.GetCharacterContainer();
    }

    public Hero GetDefaultHero()
    {
        return sortedCharaSequences.Find(u => u.GetCharacter() is Hero).GetCharacter() as Hero;
    }

    public Character GetManualSelectedTarget()
    {
        if(selectedTarget == null)
        {
            if (isTargetingEnemy)
            {
                selectedTarget = sortedCharaSequences.Find(u => !u.GetCharacter().CheckIsFriend() && u.GetCharacter().CheckIsAlive()).GetCharacter();
            }else
                selectedTarget = sortedCharaSequences.Find(u => u.GetCharacter().CheckIsFriend() && u.GetCharacter().CheckIsAlive()).GetCharacter();
            
            targetIndicatorUi.MoveIndicatorToTarget(selectedTarget.GetTargetIndicator());
        }

        return selectedTarget;
    }

    public void CheckCurrentTurnStatus(bool isHero) => isHeroTurn = isHero;
    public void SetTargetTypeEnemy(bool val) 
    {
        selectedTarget = null;
        isTargetingEnemy = val;
        GetManualSelectedTarget();
    }

    private void Update()
    {
        if (!isHeroTurn)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (UnityEngine.EventSystems.EventSystem.current != null &&
                UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            DetectObjectWithRaycast();
        }
    }

    void DetectObjectWithRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        LayerMask currentLayer = isTargetingEnemy? enemyLayer : heroLayer;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, currentLayer))
        {
            selectedTarget = hit.transform.GetComponentInParent<Character>();
            targetIndicatorUi.MoveIndicatorToTarget(selectedTarget.GetTargetIndicator());
            OnManuallySelectTarget.Invoke();
        }
    }
}
