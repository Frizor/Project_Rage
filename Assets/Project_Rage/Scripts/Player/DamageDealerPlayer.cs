using UnityEngine;

public class DamageDealerPlayer : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider targetCollider)
    {
        if (targetCollider.CompareTag("Enemy")) // Проверяем, что столкнулись с врагом
        {
            EnemyLifeManager enemyLifeManager = targetCollider.GetComponent<EnemyLifeManager>();
            HealthBarUI healthBarUI = targetCollider.GetComponentInChildren<HealthBarUI>();

            if (enemyLifeManager != null && healthBarUI != null)
            {
                enemyLifeManager.TakeDamage(damageAmount);
                healthBarUI.SetHealth(enemyLifeManager.CurrentHealth);
            }
        }
    }
}
