using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] Rigidbody bullet;
    private Animator animator;
    private int ShootBullet = Animator.StringToHash("shootBullet");

    private void Awake()
    {
        if (animator == null)
        {
            animator = transform.parent.GetComponentInChildren<Animator>();
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(ShootBullet);
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
