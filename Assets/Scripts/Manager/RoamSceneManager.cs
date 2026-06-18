using StarterAssets;
using UnityEngine;

public class RoamSceneManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController roamController;
    private GameObject mainSpawnedCharacter;

    private void Start()
    {
        mainSpawnedCharacter = Instantiate(GameManager.instance.GetPlayerData().ownedCharacters[0].characterPrefab, Vector3.zero, Quaternion.identity);
        mainSpawnedCharacter.transform.parent = roamController.transform;

        roamController.InitLocomotion();
    }
}
