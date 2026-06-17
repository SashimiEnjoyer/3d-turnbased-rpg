using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] private Transform[] charactersPoints;
    [SerializeField] private Transform[] enemiesPoints;

    private List<Enemy> enemies;

    private void Awake()
    {
        foreach (var item in GameManager.instance.GetEnemiesToFight())
        {
            GameObject enemy = Instantiate(GameManager.instance.GetEnemy(item), enemiesPoints[0].position, Quaternion.identity);
            enemies.Add(enemy.GetComponent<Enemy>());
        }
    }

    public void BackToRoam()
    {

    }
}
