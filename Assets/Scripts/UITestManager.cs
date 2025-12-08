using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITestManager : MonoBehaviour
{
    [SerializeField] Button resetButton;
    [SerializeField] TextMeshProUGUI enemyCounter;
    [SerializeField] TextMeshProUGUI textCounter;
    private float counter = 0;
    private int enemyCount = 0;
    private string countTxt = "Enemy Count: ";

    private void Start()
    {
        resetButton.onClick.AddListener(ResetCounter);
    }

    public void ResetCounter()
    {
        counter = 0;
    }

    public void SetEnemyCounter(int _count)
    {
        enemyCount = _count;
    }

    public void DecrementEnemy()
    {
        enemyCount--;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        enemyCounter.text = countTxt + enemyCount.ToString();
        textCounter.text = counter.ToString("F2") + "s";
    }
}
