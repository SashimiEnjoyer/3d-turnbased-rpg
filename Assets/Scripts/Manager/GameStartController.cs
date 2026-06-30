using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(ExecuteLoadScene());
    }

    private IEnumerator ExecuteLoadScene()
    {
        yield return new WaitUntil(()=> GameManager.instance != null);
        GameManager.instance.InitAllManagers();
        SceneManager.LoadSceneAsync("Roam");
    }
}
