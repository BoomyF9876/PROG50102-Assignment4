using UnityEngine;

public class CastleController : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool isWalking = false;
    private bool isIdle = true;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");
    private int IsJumping = Animator.StringToHash("isJumping");

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void HandleInput()
    {
        isIdle = Input.GetKey(KeyCode.Alpha1);
        isWalking = Input.GetKey(KeyCode.Alpha2);
    }

    private void Animate()
    {
        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsIdle, isIdle);
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetTrigger(IsJumping);
            isIdle = true;
            isWalking = false;
        }
    }

    public void Update()
    {
        HandleInput();
        Animate();
    }
}
