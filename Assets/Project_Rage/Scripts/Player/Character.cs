using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int currentHealth = 200;
    [SerializeField]
    private int maxHealth = 200;
    [SerializeField]
    private int currentExperience;
    [SerializeField]
    private int maxExperience;
    [SerializeField]
    private int currentLevel = 1;

    private ExperienceBarUI experienceBarUI;

    private void Start()
    {
        currentHealth = 200;
        maxHealth = 200;
        currentExperience = 0;
        maxExperience = 100;
        currentLevel = 1;

        experienceBarUI = FindObjectOfType<ExperienceBarUI>();
        if (experienceBarUI != null)
        {
            experienceBarUI.SetMaxExperience(maxExperience);
            experienceBarUI.SetExperience(currentExperience);
            experienceBarUI.SetLevel(currentLevel);
        }
    }

    private void OnEnable()
    {
        // Подписываемся на событие
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        // Отписываемся от события
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }

    private void HandleExperienceChange(int newExperience)
    {
        currentExperience += newExperience;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
        else if (experienceBarUI != null)
        {
            experienceBarUI.SetExperience(currentExperience);
        }
    }

    private void LevelUp()
    {
        maxHealth += 10;
        currentHealth = maxHealth;

        currentLevel++;

        currentExperience = 0;
        maxExperience += 100;

        if (experienceBarUI != null)
        {
            experienceBarUI.SetMaxExperience(maxExperience);
            experienceBarUI.SetExperience(currentExperience);
            experienceBarUI.SetLevel(currentLevel);
        }
    }
}

/*using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int currentHealth = 200;
    [SerializeField]
    private int maxHealth = 200;
    [SerializeField]
    private int currentExperience;
    [SerializeField]
    private int maxExperience;
    [SerializeField]
    private int currentLevel = 1;

    private ExperienceBarUI experienceBarUI;

    private void Start()
    {
        currentHealth = 200;
        maxHealth = 200;
        currentExperience = 0;
        maxExperience = 100;
        currentLevel = 1;

        experienceBarUI = FindObjectOfType<ExperienceBarUI>();
        if (experienceBarUI != null)
        {
            experienceBarUI.SetMaxExperience(maxExperience);
            experienceBarUI.SetExperience(currentExperience);
        }
    }

    private void OnEnable()
    {
        // Подписываемся на событие
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        // Отписываемся от события
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }

    private void HandleExperienceChange(int newExperience)
    {
        currentExperience += newExperience;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
        else if (experienceBarUI != null)
        {
            experienceBarUI.SetExperience(currentExperience);
        }
    }

    private void LevelUp()
    {
        maxHealth += 10;
        currentHealth = maxHealth;

        currentLevel++;

        currentExperience = 0;
        maxExperience += 100;

        if (experienceBarUI != null)
        {
            experienceBarUI.SetMaxExperience(maxExperience);
            experienceBarUI.SetExperience(currentExperience);
        }
    }
}*/