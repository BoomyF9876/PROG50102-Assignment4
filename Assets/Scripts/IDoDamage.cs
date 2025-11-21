using UnityEngine;

public interface IDoDamage
{
    GameObject Decal { get; }
    int Damage { get; }
    public void ApplyDecal(GameObject target, Vector3 hitPoint, Vector3 hitNormal);
}
