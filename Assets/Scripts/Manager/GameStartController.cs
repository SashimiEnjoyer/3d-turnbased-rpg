using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager.InitAllManagers();
        SceneManager.LoadSceneAsync("Roam");
    }
}
