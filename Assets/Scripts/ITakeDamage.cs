using UnityEngine;

public interface ITakeDamage
{
    int Health { get; }
    float NormalizedHealth { get; }
    void TakeDamage(int damage);
}
