using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private SOCharacter[] enemies;
    [SerializeField] private Transform teleportPos;
    public Transform TeleportPos => teleportPos;

    public void GoToArena()
    {
        GameManager.instance.playManager.SetEnemyToFight(enemies);

        SceneManager.LoadSceneAsync("Fight Arena");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GoToArena();
        }
    }
}
