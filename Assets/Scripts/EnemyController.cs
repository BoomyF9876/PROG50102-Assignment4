using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float turnSpeed = 5f;
    private GameObject player;
    private float spawnTime = 0;
    private bool isWalking = false;
    private bool isIdle = true;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsIdle = Animator.StringToHash("isIdle");

    public void SetPlayer(ref GameObject _player)
    {
        player = _player;
    }

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Start()
    {
        spawnTime = Random.Range(1.0f, 5.0f);
        Animate();
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

    private void Move()
    {
        Vector3 direction = Vector3.zero;
        if (player != null) direction = player.transform.position - transform.position;
        if (direction.magnitude > 0.1f)
        {
            isWalking = true;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, direction, turnSpeed * Time.deltaTime);
        }
        else
        {
            isWalking = false;
        }
        isIdle = !isWalking;
    }

    private void Update()
    {
        if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
        }
        else
        {
            Move();
            Animate();
        }
    }
}
