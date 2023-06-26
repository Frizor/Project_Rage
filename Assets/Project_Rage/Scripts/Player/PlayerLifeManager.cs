using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private HealthBarUI healthBarUI;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarUI = GetComponentInChildren<HealthBarUI>();

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public float CurrentHealth // ƒобавленное свойство CurrentHealth
    {
        get { return currentHealth; }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            // ¬ыполните действи€, св€занные с проигрышем или смертью игрока
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }
}