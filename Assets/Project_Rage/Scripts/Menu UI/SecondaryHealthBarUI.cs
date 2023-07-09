using UnityEngine;
using UnityEngine.UI;

public class SecondaryHealthBarUI : MonoBehaviour
{
    public Slider healthSlider; // Ссылка на Slider второй полоски здоровья
    public Gradient gradient; // Градиент для второй полоски здоровья
    public Image fill; // Изображение для второй полоски здоровья

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
    public Slider healthSlider; // Ссылка на Slider второй полоски здоровья

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
}*/