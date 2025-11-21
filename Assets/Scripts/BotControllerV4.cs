using UnityEngine;
using UnityEngine.Rendering;

public class BotControllerV4 : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Transform weapon;
    [SerializeField] Rigidbody bullet;
    private bool isWalking = false;
    private bool isIdle = true;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");
    private int ShootBullet = Animator.StringToHash("shootBullet");
    [SerializeField] float moveSpeed = 2.5f;
    [SerializeField] float turnSpeed = 100f;
    private BotInputAction inputActions;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        inputActions = new BotInputAction();
        inputActions.Enable();
    }

    private Vector2 GetInput()
    {
        Vector2 input = inputActions.BotCharActionMap.Move.ReadValue<Vector2>();

        isIdle = input == Vector2.zero;
        isWalking = !isIdle;

        return input;
    }

    private Vector2 GetInputNormalized()
    {
        return GetInput().normalized;
    }

    private void Animate()
    {
        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsIdle, isIdle);
    }

    private bool CanMove(Vector2 input, Vector3 position, ref Vector3 direction)
    {
        float maxDistance = 0.5f;
        direction = new Vector3(input.x, 0, input.y);
        isIdle = Physics.Raycast(position, direction, maxDistance) || direction.magnitude < 0.001f;

        if (isIdle)
        {
            direction.z = 0;
            isIdle = Physics.Raycast(position, direction, maxDistance) || direction.magnitude < 0.001f;
        }
        if (isIdle)
        {
            direction = new Vector3(0, 0, input.y);
            isIdle = Physics.Raycast(position, direction, maxDistance) || direction.magnitude < 0.001f;
        }

        isWalking = !isIdle;
        return isWalking;
    }

    public void Move()
    {
        Vector2 input = GetInputNormalized();
        Vector3 direction = Vector3.zero;
        if (CanMove(input, transform.position, ref direction))
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, direction, turnSpeed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(ShootBullet);
            Instantiate(bullet, weapon.position, weapon.rotation);
        }
    }

    public void Update()
    {
        Move();
        Animate();
        Shoot();
    }
}
