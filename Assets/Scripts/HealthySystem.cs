using UnityEngine;

public class HealthySystem : MonoBehaviour
{
    [SerializeField] GameObject bar;
    
    public void UpdateHealth(float health)
    {
        bar.transform.localScale = new Vector3(health, 1.0f, 1.0f);
    }
}
