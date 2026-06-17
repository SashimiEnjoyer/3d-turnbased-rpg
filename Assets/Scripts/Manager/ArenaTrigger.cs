using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private SOCharacter[] enemies;

    public void GoToArena()
    {
        GameManager.instance.playManager.SetEnemyToFight(enemies);

        SceneManager.LoadSceneAsync("Fight Arena");
    }
}
