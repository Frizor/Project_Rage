using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    [SerializeField][Range(0f, 100f)] private float maxHealth = 100f;
    [SerializeField][Range(0f, 100f)] private float currentHealth = 100f;

    private void Start()
    {
        InitializeHealthBar();
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        Debug.Log(health);
        currentHealth = health;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void InitializeHealthBar()
    {
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }
}