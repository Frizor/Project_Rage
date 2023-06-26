using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private PlayerHealthBarUI playerHealthBarUI;

    private void Start()
    {
        currentHealth = maxHealth;
        playerHealthBarUI = GetComponentInChildren<PlayerHealthBarUI>();

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetMaxHealth((int)maxHealth);
            playerHealthBarUI.SetHealth((int)currentHealth);
        }
    }

    public float CurrentHealth // ƒобавленное свойство CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            // ¬ыполните действи€, св€занные с проигрышем или смертью игрока
        }

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetHealth((int)currentHealth);
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetHealth((int)currentHealth);
        }
    }

    public void SetHealth(float health)
    {
        currentHealth = health;

        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetHealth((int)currentHealth);
        }
    }
}
