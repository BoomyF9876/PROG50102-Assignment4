using UnityEngine;
using UnityEngine.UI;

public class BarProgress : MonoBehaviour
{
    [SerializeField] Image progressBar;
    float progress = 0;
    float progressSpeed = 25;
    float progressMax = 100;

    // Update is called once per frame
    private void Update()
    {
        progress += progressSpeed * Time.deltaTime;
        progressBar.fillAmount = progress / progressMax;

        if (progress > progressMax) progress = 0;
    }
}
