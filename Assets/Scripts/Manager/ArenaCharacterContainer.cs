using Unity.Cinemachine;
using UnityEngine;

public class ArenaCharacterContainer : MonoBehaviour
{
    [SerializeField] private CinemachineCamera baseCharacterCam;
    public CinemachineCamera BaseCam => baseCharacterCam;
}
