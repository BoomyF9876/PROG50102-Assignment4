using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DynamicWall : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private int Width = 6;
    [SerializeField] private int Height = 6;

    private void Start()
    {
        for (int i = -Width/2; i < Width / 2; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector3 offset = new Vector3(i, j, 0);
                var b = Instantiate(brick, transform.position + offset, Quaternion.identity);
                b.name = $"Brick({i}, {j})";
                b.transform.SetParent(transform);
            }
        }
    }

    void Update()
    {
        
    }
}
