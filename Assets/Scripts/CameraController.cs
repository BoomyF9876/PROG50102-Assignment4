using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")] public Transform character;
    [Header("Camera")] public Transform mainCamera;
    [Header("Offset")] public Vector3 offset = new Vector3(0f, 3f, -5f);
    [Header("Smoothness")] public float followSpeed = 5f;
    [Header("Controller Type")]
    public ControllerType controllerType = ControllerType.BotV4;
    public enum ControllerType
    {
        BotV3,
        BotV4
    }

    private void LateUpdate()
    {
        if (character == null) return;
        Vector3 desiredPosition = Vector3.zero;
        switch (controllerType)
        {
            case ControllerType.BotV3: // to stay behind the bot char
                desiredPosition = character.position + character.rotation * offset;
                break;
            case ControllerType.BotV4: // character's position + fixed world-space offset
                desiredPosition = character.position + offset;
                break;
        }
        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(mainCamera.position,
                                                desiredPosition,
                                                followSpeed * Time.deltaTime);
        mainCamera.position = smoothedPosition;
        // Always look at the character 
        mainCamera.LookAt(character.position + Vector3.up * (offset.y * 0.5f));
    }
}