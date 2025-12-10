using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private int count = 50;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));
            GameObject b = Instantiate(brick, transform.position + offset, Quaternion.identity);
            b.name = $"Wall({i})";
            b.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
