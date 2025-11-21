using UnityEngine;

public class BotControllerV3 : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool isWalking = false;
    private bool isIdle = true;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");
    private int TurnLeft = Animator.StringToHash("turnLeft");
    private int TurnRight = Animator.StringToHash("turnRight");
    [SerializeField] float moveSpeed = 2.5f;
    [SerializeField] float rotateSpeed = 100f;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void HandleInput()
    {
        isWalking = Input.GetKey(KeyCode.W);
        isTurningLeft = Input.GetKey(KeyCode.A);
        isTurningRight = Input.GetKey(KeyCode.D);
        isIdle = !isWalking;
    }

    public void Animate()
    {
        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsIdle, isIdle);

        if (isIdle)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.A)) animator.SetTrigger(TurnLeft);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.D)) animator.SetTrigger(TurnRight);
        }
    }

    public void Move()
    {
        //float distance = Vector3.Distance(transform.position, target);
        if (isWalking)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (isTurningLeft)
        {
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        }
        if (isTurningRight)
        {
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
    }

    public void Update()
    {
        HandleInput();
        Move();
        Animate();
    }

}
