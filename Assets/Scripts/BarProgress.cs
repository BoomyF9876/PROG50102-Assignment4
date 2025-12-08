using UnityEngine;
using UnityEngine.UI;

public class BarProgress : MonoBehaviour
{
    [SerializeField] Image progressBar;
    float progress = 0;
    float progressMax = 100;

    float maxHealth = 0;
    float health = 0;

    public void TakeDamage(float _damage)
    {
        health -= _damage;
    }

    public void SetMaxHealth(float _health)
    {
        maxHealth = _health;
        health = _health;
    }

    private void Update()
    {
        progressBar.fillAmount = health / maxHealth;

        if (progress > progressMax) progress = 0;
    }
}
