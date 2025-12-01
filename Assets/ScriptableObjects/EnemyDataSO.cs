using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SOData/Enemy", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public Sprite miniMapIcon;
    public float moveSpeed = 2.5f;
    public float turnSpeed = 25f;
    public int attack = 25;
    public int maxHealth = 100;

    public void Attack(string target)
    {
        Debug.Log($"{enemyName} attacks {target} for {attack} damage");
    }
}
