using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI levelText; // Добавленная ссылка на текст уровня

    public void SetMaxExperience(int experience)
    {
        slider.maxValue = experience;
    }

    public void SetExperience(int experience)
    {
        slider.value = experience;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetLevel(int level)
    {
        if (levelText != null)
        {
            levelText.text = level.ToString();
        }
    }
}

/*using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public void SetMaxExperience(int experience)
    {
        slider.maxValue = experience;
    }

    public void SetExperience(int experience)
    {
        slider.value = experience;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}*/