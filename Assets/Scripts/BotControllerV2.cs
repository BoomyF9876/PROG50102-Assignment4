using UnityEngine;

public class BotControllerV2 : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float rotateSpeed = 20.0f;
    [SerializeField] float stopDistance = 0.1f;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");
    [SerializeField] private Vector3 target;
    bool isIdle, isWalking;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        SetTarget(transform.position);
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SetTarget(MouseUtil.Instance.GetMouseWorldPosition());
        //}
        HandleMovement();
        Animate();
    }

    public void SetTarget(Vector3 _target)
    {
        target = _target;
    }

    public void HandleMovement()
    {
        float distance = Vector3.Distance(transform.position, target);
        if (distance >= stopDistance)
        {
            Vector3 direction = (target - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, direction, rotateSpeed * Time.deltaTime);
            isWalking = true;
            isIdle = false;
        }
        else
        {
            isWalking = false;
            isIdle = true;
        }
    }

    public void Animate()
    {
        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsIdle, isIdle);
    }
}
