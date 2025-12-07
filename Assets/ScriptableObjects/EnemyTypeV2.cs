using UnityEngine;

public class EnemyTypeV2 : MonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;
    public int currentHealth = 0;
    public float Health => (float)currentHealth / enemyData.maxHealth;

    private void Start()
    {
        currentHealth = enemyData.maxHealth;
    }

    public void Attack(Transform attacker, Transform target)
    {
        enemyData.Attack(attacker, target);
    }
}
