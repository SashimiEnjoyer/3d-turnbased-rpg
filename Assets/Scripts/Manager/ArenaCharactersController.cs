using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
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
    [SerializeField] private LayerMask characterMask;
    [SerializeField] private ArenaCharacterContainer[] charactersPoints;
    [SerializeField] private ArenaCharacterContainer[] enemiesPoints;

    private int characterPlacementIdx = 0;
    private int enemyPlacementIdx = 0;

    private List<CharacterSequence> allCharaInArena = new();
    private List<CharacterSequence> sortedCharaSequences = new();

    private CharacterSequence currentHeroTurn;
    private bool isHeroTurn = false;
    private Enemy selectedEnemy;

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

        return sortedCharaSequences = allCharaInArena.Where(u => u.GetCharacter().CheckIsAlive())
            .OrderByDescending(u => u.GetCharacter().GetSpeed()).ToList();
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

    public ArenaCharacterContainer GetHeroForEnemy(Hero myHero)
    {
        return sortedCharaSequences.FirstOrDefault(u => u.GetCharacter() is Hero)?.GetCharacterContainer();
    }

    public Hero GetDefaultHero()
    {
        return sortedCharaSequences.Find(u => u.GetCharacter() is Hero).GetCharacter() as Hero;
    }

    public Enemy GetManualSelectedEnemy()
    {
        if (selectedEnemy == null)
            return selectedEnemy = sortedCharaSequences.FirstOrDefault(u => u.GetCharacter() is Enemy).GetCharacter() as Enemy;
        else
            return selectedEnemy;
    }

    public void CheckCurrentTurnStatus(bool isHero) => isHeroTurn = isHero;

    private void Update()
    {
        if (!isHeroTurn)
            return;

        if(Mouse.current.leftButton.wasPressedThisFrame)
            DetectObjectWithRaycast();
    }

    void DetectObjectWithRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, characterMask))
        {
            selectedEnemy = hit.transform.GetComponentInParent<Character>() as Enemy;
        }
    }
}
