using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StarterAssetsInputs gameInputs;

    public PlayerInput GetPlayerInput() => playerInput;
    public StarterAssetsInputs GetGameInputs() => gameInputs;

    public void SwitchActionMaps(string mapsName)
    {
        playerInput.SwitchCurrentActionMap(mapsName);
    }
}
