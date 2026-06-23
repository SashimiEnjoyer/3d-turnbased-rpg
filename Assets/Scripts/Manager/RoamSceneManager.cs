using StarterAssets;
using System.Collections;
using UnityEngine;

public class RoamSceneManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController roamController;
    [SerializeField] private ArenaTrigger[] GoToArenaTriggers;
     
    private GameObject mainSpawnedCharacter;

    private IEnumerator Start()
    {
        mainSpawnedCharacter = Instantiate(GameManager.instance.GetPlayerData().ownedCharacters[0].characterPrefab, Vector3.zero, Quaternion.identity);
        mainSpawnedCharacter.transform.parent = roamController.transform;
        roamController.InitLocomotion();

        yield return new WaitUntil(()=> roamController.Controller != null);

        if (GameManager.instance.playManager.CheckWasWinningBefore())
        {
            roamController.Controller.enabled = false;
            Debug.Log(GameManager.instance.playManager.GetLastEnemyIdx());
            roamController.transform.position = GoToArenaTriggers[GameManager.instance.playManager.GetLastEnemyIdx()].TeleportPos.position;
            GoToArenaTriggers[0].gameObject.SetActive(false);
            GameManager.instance.playManager.FlagWinningBattle(false);
            roamController.Controller.enabled = true;
        }
    }
}
