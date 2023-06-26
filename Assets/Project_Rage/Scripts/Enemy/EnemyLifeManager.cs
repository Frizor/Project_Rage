using UnityEngine;

public class EnemyLifeManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float destroyDelay = 10f; // �������� ����� ������������ �����
    private bool isDead = false; // ����, �����������, ��� ���� �����

    private EnemyHealthBarUI healthBarUI;
    private EnemyController3 enemyController3;
    private EnemySwordAttack enemySwordAttack;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarUI = GetComponentInChildren<EnemyHealthBarUI>();
        enemyController3 = GetComponent<EnemyController3>();
        enemySwordAttack = GetComponent<EnemySwordAttack>();

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f && !isDead)
        {
            currentHealth = 0f;
            isDead = true;

            // ��������� ���������� ������������ � ����� �����
            if (enemyController3 != null)
                enemyController3.enabled = false;

            if (enemySwordAttack != null)
                enemySwordAttack.enabled = false;

            // �������� �������� ��� ����������� ����� ����� �������� ��������
            StartCoroutine(DestroyAfterDelay());
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);

        // ���������� ���� ������ EnemyPrefab
        Destroy(gameObject);
    }
}


/*using UnityEngine;

public class EnemyLifeManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float destroyDelay = 10f; // �������� ����� ������������ �����
    private bool isDead = false; // ����, �����������, ��� ���� �����

    private EnemyHealthBarUI healthBarUI;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarUI = GetComponentInChildren<EnemyHealthBarUI>();

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f && !isDead)
        {
            currentHealth = 0f;
            isDead = true;

            // �������� �������� ��� ����������� ����� ����� �������� ��������
            StartCoroutine(DestroyAfterDelay());
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);

        // ���������� ���� ������ EnemyPrefab
        Destroy(gameObject);
    }
}*/


/*using UnityEngine;

public class EnemyLifeManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float disappearDelay = 10f; // �������� ����� ������������� �����
    private bool isDead = false; // ����, �����������, ��� ���� �����

    private EnemyHealthBarUI healthBarUI;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarUI = GetComponentInChildren<EnemyHealthBarUI>();

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f && !isDead)
        {
            currentHealth = 0f;
            isDead = true;

            // �������� �������� ��� ������������ ����� ����� �������� ��������
            StartCoroutine(DisappearAfterDelay());
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }

    private System.Collections.IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);

        // ��������� ��������, ��������� � ������������� �����
        gameObject.SetActive(false);
    }
}*/


/*using UnityEngine;

public class EnemyLifeManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private EnemyHealthBarUI healthBarUI;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarUI = GetComponentInChildren<EnemyHealthBarUI>();

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            // ��������� ��������, ��������� � ������������ �����
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
*/