using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Cinemachine Virtual Cameras")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera managerCam;
    public CinemachineVirtualCamera managerInCam;

    /// <summary>
    /// 플레이어 시점 카메라로 전환
    /// </summary>
    public void SetMainCamActive()
    {
        mainCam.Priority = 2;
        managerCam.Priority = 1;
        managerInCam.Priority = 0;
    }

    /// <summary>
    /// 관리자 연출용 카메라로 전환
    /// </summary>
    public void SetManagerCamActive()
    {
        managerCam.Priority = 2;
        mainCam.Priority = 1;
        managerInCam.Priority = 0;
    }

    /// <summary>
    /// 관리자 클로즈업 연출용 카메라로 전환
    /// </summary>
    public void SetManagerInCamActive()
    {
        managerInCam.Priority = 2;
        managerCam.Priority = 1;
        mainCam.Priority = 0;
    }

    /// <summary>
    /// 모든 카메라 우선순위 초기화
    /// </summary>
    public void ResetPriorities()
    {
        mainCam.Priority = 0;
        managerCam.Priority = 0;
        managerInCam.Priority = 0;
    }
}
