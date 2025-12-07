using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttack", menuName = "SOData/Attacks/Ranged")]
public class RangedAttackDataSO : AttackDataSO
{
    [Header("Range Specific")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    public override void Execute(Transform attacker, Transform target)
    {
        Debug.Log($"{attackName} makes {damage} ranged damage.");
    }
}
