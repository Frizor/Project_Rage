using UnityEngine;
using UnityEngine.UI;

public class SecondaryHealthBarUI : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}


/*using UnityEngine;
using UnityEngine.UI;

public class SecondaryHealthBarUI : MonoBehaviour
{
    public Slider healthSlider; // —сылка на Slider второй полоски здоровь€

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
}*/