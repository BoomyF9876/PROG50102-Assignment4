using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float turnSpeed = 5f;
    public GameObject player;
    bool isWalking = false;
    bool isIdle = true;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Start()
    { 
    }

    IEnumerator WaitAndDoSomething()
    {
        yield return new WaitForSeconds(2.0f);
    }

    private void Animate()
    {
        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsIdle, isIdle);
    }

    public void Move()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude > 0.1f)
        {
            isWalking = true;
            isIdle = !isWalking;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, direction, turnSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        StartCoroutine(WaitAndDoSomething());
        Move();
        Animate();
    }
}
