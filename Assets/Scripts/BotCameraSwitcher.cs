using Unity.Cinemachine;
using UnityEngine;

public class BotCameraSwitcher : MonoBehaviour
{
    [Header("Virtual Cameras")]
    [SerializeField] CinemachineCamera BotV3Camera;
    [SerializeField] CinemachineCamera BotV4Camera;

    [Header("Priorities")]
    [SerializeField] int CamPriorityLow = 0;
    [SerializeField] int CamPriorityHigh = 10;

    private BotControllerV3 BotControllerV3;
    private PlayerController BotControllerV4;

    private void Awake()
    {
        BotControllerV3 = GetComponent<BotControllerV3>();
        BotControllerV4 = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BotControllerV3.enabled = false;
            BotControllerV4.enabled = true;

            BotV3Camera.Priority = CamPriorityLow;
            BotV4Camera.Priority = CamPriorityHigh;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BotControllerV3.enabled = true;
            BotControllerV4.enabled = false;

            BotV3Camera.Priority = CamPriorityHigh;
            BotV4Camera.Priority = CamPriorityLow;
        }
    }
}
