using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI healthText;

    private PlayerLifeManager playerLifeManager;
    private SecondaryHealthBarUI secondaryHealthBarUI;

    private void Start()
    {
        playerLifeManager = FindObjectOfType<PlayerLifeManager>();
        secondaryHealthBarUI = FindObjectOfType<SecondaryHealthBarUI>();

        if (playerLifeManager != null)
        {
            SetMaxHealth(playerLifeManager.maxHealth);
            SetHealth(playerLifeManager.CurrentHealth);
        }
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = playerLifeManager.CurrentHealth;
        fill.color = gradient.Evaluate(1f);

        if (secondaryHealthBarUI != null)
        {
            secondaryHealthBarUI.SetMaxHealth(health);
        }
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        healthText.text = health.ToString(); // ќбновление текстового значени€ здоровь€

        if (secondaryHealthBarUI != null)
        {
            secondaryHealthBarUI.SetHealth(health);
        }
    }
}


/*using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private SecondaryHealthBarUI secondaryHealthBarUI; // ƒобавленна€ ссылка на SecondaryHealthBarUI

    [SerializeField][Range(0f, 100f)] private float maxHealth = 100f;
    [SerializeField][Range(0f, 100f)] private float currentHealth = 100f;

    private void Start()
    {
        InitializeHealthBar();
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        slider.maxValue = Mathf.RoundToInt(health);
        slider.value = Mathf.RoundToInt(health);
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        slider.value = Mathf.RoundToInt(health);
        fill.color = gradient.Evaluate(slider.normalizedValue);
        healthText.text = health.ToString(); // ќбновление текстового значени€ здоровь€

        if (secondaryHealthBarUI != null)
        {
            secondaryHealthBarUI.SetHealth(health); // ѕередача значени€ на вторую полоску здоровь€
        }
    }

    private void InitializeHealthBar()
    {
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }
}*/

/*using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField][Range(0f, 100f)] private float maxHealth = 100f;
    [SerializeField][Range(0f, 100f)] private float currentHealth = 100f;

    private void Start()
    {
        InitializeHealthBar();
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        slider.maxValue = Mathf.RoundToInt(health);
        slider.value = Mathf.RoundToInt(health);
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        slider.value = Mathf.RoundToInt(health);
        fill.color = gradient.Evaluate(slider.normalizedValue);
        healthText.text = health.ToString(); // ќбновление текстового значени€ здоровь€
    }

    private void InitializeHealthBar()
    {
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }
}*/