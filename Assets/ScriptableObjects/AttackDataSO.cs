using UnityEngine;

public abstract class AttackDataSO : ScriptableObject, IAttack
{
    [Header("Common")]
    public string attackName;
    public float damage;
    public float cooldown;

    public string Name => attackName;
    public float Damage => damage;
    public float CoolDown => cooldown;

    public abstract void Execute(Transform attacker, Transform target);

}
