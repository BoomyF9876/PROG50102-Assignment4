using UnityEngine;

public class Bullet : MonoBehaviour, IDoDamage
{
    [SerializeField] float lifeSpan = 2.5f;
    [SerializeField] float speed = 50f;
    Rigidbody rb;

    [SerializeField] int damage = 100;
    [SerializeField] GameObject decal;

    public GameObject Decal { get { return decal; } }

    public int Damage { get { return damage; } }

    void Start()
    {
        Destroy(gameObject, lifeSpan);
        rb = GetComponent<Rigidbody>();
        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void Init()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    public void ApplyDecal(GameObject target, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!decal || !target) return;
        GameObject d = Instantiate(decal, hitPoint + hitNormal * -0.001f,
            Quaternion.LookRotation(hitNormal));
        d.transform.parent = target.transform;
        Destroy(d, 10f);
    }
}
