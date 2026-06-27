using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private SOCharacter[] enemies;
    [SerializeField] private Transform teleportPos;

    [SerializeField] private UnityEvent OnObjectTriggering;

    public Transform TeleportPos => teleportPos;
    private bool isInteracted = false;

    public void GoToArena()
    {
        GameManager.instance.playManager.SetEnemyToFight(enemies);

        SceneManager.LoadSceneAsync("Fight Arena");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInteracted)
            return;

        if(other.CompareTag("Player"))
        {
            OnObjectTriggering?.Invoke();
            isInteracted = true;
        }
    }
}
