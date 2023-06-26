using UnityEngine;

public class DamageDealerEnemy : MonoBehaviour
{
    public int damageAmount = 10;
    public PlayerHealthBarUI playerHealthBarUI; // Ссылка на PlayerHealthBarUI на панели UI

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что столкнулись с игроком
        {
            PlayerLifeManager playerLifeManager = other.GetComponent<PlayerLifeManager>();

            if (playerLifeManager != null && playerHealthBarUI != null)
            {
                playerLifeManager.TakeDamage(damageAmount);
                playerHealthBarUI.SetHealth(playerLifeManager.CurrentHealth);
            }
        }
    }
}


/*using UnityEngine;

public class DamageDealerEnemy : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что столкнулись с игроком
        {
            PlayerLifeManager playerLifeManager = other.GetComponent<PlayerLifeManager>();
            PlayerHealthBarUI healthBarUI = other.GetComponentInChildren<PlayerHealthBarUI>();

            if (playerLifeManager != null && healthBarUI != null)
            {
                playerLifeManager.TakeDamage(damageAmount);
                healthBarUI.SetHealth(playerLifeManager.CurrentHealth);
            }
        }
    }
}*/
