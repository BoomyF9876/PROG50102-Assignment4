using UnityEngine;

public class Destroyable : MonoBehaviour, ITakeDamage
{
    private int maxHealth = 100;
    private int health = 0;

    public int Health { get { return health; } }

    public float NormalizedHealth { get { return (float)health/maxHealth; } }

    [SerializeField] HealthySystem healthySystem;

    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < maxHealth) healthySystem.gameObject.SetActive(true);
        healthySystem.UpdateHealth(NormalizedHealth);
        if (health <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IDoDamage>(out IDoDamage damageSource))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 hitPoint = contact.point;
            Vector3 hitNormal = contact.normal;
            damageSource.ApplyDecal(gameObject, hitPoint, hitNormal);
            TakeDamage(damageSource.Damage);
        }
    }
}
