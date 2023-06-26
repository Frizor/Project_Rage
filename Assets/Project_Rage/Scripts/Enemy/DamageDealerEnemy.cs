using UnityEngine;

public class DamageDealerEnemy : MonoBehaviour
{
    public int damageAmount = 10;


    private void OnTriggerEnter(Collider targetCollider)
    {
        PlayerLifeManager playerLifeManager = targetCollider.GetComponent<PlayerLifeManager>();
        HealthBarUI healthBarUI = targetCollider.GetComponentInChildren<HealthBarUI>();

        if (playerLifeManager != null && healthBarUI != null)
        {
            playerLifeManager.TakeDamage(damageAmount);
            healthBarUI.SetHealth(playerLifeManager.CurrentHealth);
        }
    }

    /*    private void OnTriggerEnter(Collider targetCollider)
        {
            if (targetCollider.CompareTag("Player")) // Проверяем, что столкнулись с игроком
            {
                EnemyLifeManager enemyLifeManager = targetCollider.GetComponent<EnemyLifeManager>();
                HealthBarUI healthBarUI = targetCollider.GetComponentInChildren<HealthBarUI>();

                if (enemyLifeManager != null && healthBarUI != null)
                {
                    enemyLifeManager.TakeDamage(damageAmount);
                    healthBarUI.SetHealth(enemyLifeManager.CurrentHealth);
                }
            }
        }*/
}