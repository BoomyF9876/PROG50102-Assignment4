using UnityEngine;

[DefaultExecutionOrder(3)]
public class EnemyAttack : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("2. Enemy Awake");
    }

    void Start()
    {
        Debug.Log("2. Enemy Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("2. Enemy Update");
    }
}
