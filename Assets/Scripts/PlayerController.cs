using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Transform weapon;
    [SerializeField] Rigidbody bullet;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool isIdle = true;
    private float speed = 0;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");
    private int IsRunning = Animator.StringToHash("isRunning");
    private int ShootBullet = Animator.StringToHash("shootBullet");
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
        animator.SetBool(IsRunning, isRunning);
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

        return !isIdle;
    }

    private void Move()
    {
        Vector2 input = GetInputNormalized();
        Vector3 direction = Vector3.zero;
        if (CanMove(input, transform.position, ref direction))
        {
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, direction, turnSpeed * Time.deltaTime);
        }
        else
        {
            speed = startSpeed;
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

    private void Update()
    {
        Move();
        Animate();
        Shoot();
    }
}
