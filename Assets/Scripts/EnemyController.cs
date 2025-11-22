using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator animator;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }
}
