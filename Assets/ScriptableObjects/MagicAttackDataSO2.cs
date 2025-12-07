using UnityEngine;

[CreateAssetMenu(fileName = "MagicAttack", menuName = "SOData/Attacks/Magic")]
public class MagicAttackDataSO : AttackDataSO
{
    [Header("Magic Specific")]
    public float manaCost = 20f;
    public ParticleSystem castEffect;

    public override void Execute(Transform attacker, Transform target)
    {
        Debug.Log($"{attackName} makes {damage} magic damage.");
    }
}
