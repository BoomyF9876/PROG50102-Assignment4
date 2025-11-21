using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material material;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    private void ChangeColor(AnimationEvent myEvent)
    {
        material.color = color;
    }
}
