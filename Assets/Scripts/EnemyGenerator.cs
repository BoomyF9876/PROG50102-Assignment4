using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int count = 10;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(i, 0, 0);
            var b = Instantiate(enemy, transform.position + offset, Quaternion.identity);
            b.name = $"Enemy({i})";
            b.transform.SetParent(transform);
        }
    }

    void Update()
    {

    }
}
