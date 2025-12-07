using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "SOData/Attacks/Melee")]
public class MeleeAttackDataSO : AttackDataSO
{
    [Header("Melee Specific")]
    public float swingRange = 2f;
    public LayerMask hitLayers = -1;

    public override void Execute(Transform attacker, Transform target)
    {
        Debug.Log($"{attackName} makes {damage} damage.");
    }
}
