using Unity.Cinemachine;
using UnityEngine;

public class ArenaCharacterContainer : MonoBehaviour
{
    [SerializeField] private CinemachineCamera baseCharacterCam;
    [SerializeField] private CinemachineCamera buffTeamCam;
    public CinemachineCamera BaseCam => baseCharacterCam;
    public CinemachineCamera BuffTeamCam => buffTeamCam;

    private int baseCamPriority = 10;

    public void SetupCurrentContainer()
    {
        baseCharacterCam.Priority = baseCamPriority;
        buffTeamCam.Priority = baseCamPriority;
    }

    public void SetActiveBaseCam()
    {
        baseCharacterCam.Priority = baseCamPriority + 10;
        buffTeamCam.Priority = baseCamPriority;
    }

    public void SetActiveBuffCam()
    {
        baseCharacterCam.Priority = baseCamPriority;
        buffTeamCam.Priority = baseCamPriority + 10;
    }

    public void ResetAllCam()
    {
        baseCharacterCam.Priority = 0;
        buffTeamCam.Priority = 0;
    }
}
