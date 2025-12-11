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
    private bool timer = true;
    private string countTxt = "Enemy Count: ";
    private string timeTxt = "Time: ";

    public void SetEnemyCounter(int _count)
    {
        enemyCount = _count;
    }

    public void DecrementEnemy()
    {
        enemyCount--;
    }

    public void StopTimer()
    {
        timeTxt = "Clear Time: ";
        timer = false;
    }

    private void Update()
    {
        if (timer) counter += Time.deltaTime;
        enemyCounter.text = countTxt + enemyCount.ToString();
        textCounter.text = timeTxt + counter.ToString("F2") + "s";
    }
}
