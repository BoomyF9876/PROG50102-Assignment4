using UnityEngine;

public interface IAttack
{
    string Name { get; }
    float Damage { get; }
    float CoolDown { get; }

    void Execute(Transform attacker, Transform target);
}
