using UnityEngine;
using UnityEngine.UIElements;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private HealthySystem healthySystem;
    [SerializeField] private int count = 10;
    private bool isVictory = false;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
            GameObject b = Instantiate(enemy, transform.position + offset, Quaternion.identity);
            b.name = $"Enemy({i})";
            b.transform.SetParent(transform);

            EnemyController enemyController = b.GetComponent<EnemyController>();
            enemyController.SetPlayer(ref player);

            Destroyable destroyable = b.AddComponent(typeof(Destroyable)) as Destroyable;
            destroyable.healthySystem = healthySystem;
        }
    }

    private void Update()
    {
        if (transform.childCount == 0 && !isVictory)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.Victory();
            isVictory = true;
        }
    }
}
