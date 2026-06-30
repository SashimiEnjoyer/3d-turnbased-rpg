using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private SOCharacter[] enemies;
    [SerializeField] private Transform teleportPos;

    [SerializeField] private UnityEvent OnObjectTriggering;

    public Transform TeleportPos => teleportPos;

    public void GoToArena()
    {
        GameManager.instance.playManager.SetEnemyToFight(enemies);

        SceneManager.LoadSceneAsync("Fight Arena");
    }
}
