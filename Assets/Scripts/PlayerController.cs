using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform weapon;

    private bool isWalking = false;
    private bool isRunning = false;
    private bool isIdle = true;
    private float speed = 0;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");
    private int IsRunning = Animator.StringToHash("isRunning");
    private int VictoryPose = Animator.StringToHash("victory");
    [SerializeField] float startSpeed = 2.5f;
    [SerializeField] float turnSpeed = 100f;
    private BotInputAction inputActions;

    public void Victory()
    {
        animator.SetTrigger(VictoryPose);
    }

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        inputActions = new BotInputAction();
        inputActions.Enable();
        speed = startSpeed;
    }

    private Vector2 GetInputNormalized()
    {
        Vector2 input = inputActions.BotCharActionMap.Move.ReadValue<Vector2>();

        isIdle = input == Vector2.zero;
        isWalking = !isIdle;
        isRunning = !isIdle;

        return input.normalized;
    }

    private void Animate()
    {
        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsIdle, isIdle);
        animator.SetBool(IsRunning, isRunning);
    }

    private void Move()
    {
        Vector2 input = GetInputNormalized();
        Vector3 direction = new Vector3(input.x, 0, input.y);

        CapsuleCastCollision collision = GetComponent<CapsuleCastCollision>();

        if (collision.CanMove(input, transform.position, transform.forward, ref direction))
        {
            isIdle = false;
            if (speed - startSpeed < 1.5f)
            {
                speed += Time.deltaTime;
                isWalking = !isIdle;
                isRunning = false;
            }
            else
            {
                isRunning = !isIdle;
                isWalking = false;
            }
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, direction, turnSpeed * Time.deltaTime);
        }
        else
        {  
            speed = startSpeed;
        }
    }

    private void Update()
    {
        Move();
        Animate();
    }
}
