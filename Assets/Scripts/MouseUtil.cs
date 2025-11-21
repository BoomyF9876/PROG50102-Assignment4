using UnityEngine;
using UnityEngine.Events;

public class MouseUtil : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = -1;
    [SerializeField] private GameObject mousePointer;
    private bool isEnabled = true;
    private bool IsEnabled { get { return isEnabled; } }
    private Vector3 lastMousePosition;
    public static MouseUtil Instance;

    public UnityEvent<Vector3> OnMouseChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (mousePointer == null)
            {
                if (transform.childCount == 0)
                {
                    Debug.LogError("The MouseUtil has no Mouse Pointer");
                }
                else
                {
                    mousePointer = transform.GetChild(0).gameObject;
                }
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        TogllerEnabled();

        if (!isEnabled) return;
        transform.position = GetMouseWorldPosition();
        OnMouseChanged?.Invoke(transform.position);
    }

    private void TogllerEnabled()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isEnabled = !isEnabled;
            mousePointer.SetActive(isEnabled);
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, layerMask))
        {
            lastMousePosition = transform.position;
            return hitInfo.point;
        }
        return lastMousePosition;
    }

    public bool TryGetMouseWorldPosition(out Vector3 pos)
    {
        pos = GetMouseWorldPosition();
        return IsEnabled;
    }
}
