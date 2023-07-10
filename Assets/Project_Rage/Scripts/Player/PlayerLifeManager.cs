using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private PlayerHealthBarUI playerHealthBarUI;

    private void Start()
    {
        currentHealth = maxHealth;
        playerHealthBarUI = FindObjectOfType<PlayerHealthBarUI>();

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetMaxHealth(maxHealth);
            playerHealthBarUI.SetHealth(currentHealth);
        }
    }

    public float CurrentHealth
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
            // Выполните действия, связанные с проигрышем или смертью игрока
        }

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetHealth(currentHealth);
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
            playerHealthBarUI.SetHealth(currentHealth);
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
            playerHealthBarUI.SetHealth(currentHealth);
        }
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetMaxHealth(maxHealth);
        }
    }
}

/*using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private PlayerHealthBarUI playerHealthBarUI;

    private void Start()
    {
        currentHealth = maxHealth;
        playerHealthBarUI = FindObjectOfType<PlayerHealthBarUI>();

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetMaxHealth(maxHealth);
            playerHealthBarUI.SetHealth(currentHealth);
        }
    }

    public float CurrentHealth
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
            // Выполните действия, связанные с проигрышем или смертью игрока
        }

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetHealth(currentHealth);
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
            playerHealthBarUI.SetHealth(currentHealth);
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
            playerHealthBarUI.SetHealth(currentHealth);
        }
    }
}*/

/*using UnityEngine;

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
            playerHealthBarUI.SetMaxHealth(maxHealth);
            playerHealthBarUI.SetHealth(currentHealth);
        }
    }

    public float CurrentHealth
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
            // Выполните действия, связанные с проигрышем или смертью игрока
        }

        if (playerHealthBarUI != null)
        {
            playerHealthBarUI.SetHealth(currentHealth);
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
            playerHealthBarUI.SetHealth(currentHealth);
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
            playerHealthBarUI.SetHealth(currentHealth);
        }
    }
}*/

/*using UnityEngine;

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

    public float CurrentHealth // Добавленное свойство CurrentHealth
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
            // Выполните действия, связанные с проигрышем или смертью игрока
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
}*/