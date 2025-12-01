using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITestManager : MonoBehaviour
{
    [SerializeField] Button resetButton;
    [SerializeField] TextMeshProUGUI textCounter;
    private float counter = 0;

    private void Start()
    {
        resetButton.onClick.AddListener(ResetCounter);
    }

    public void ResetCounter()
    {
        counter = 0;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        textCounter.text = counter.ToString("F2");
    }
}
