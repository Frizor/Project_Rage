using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 10;

    [SerializeField] private Canvas canvas;

    LifeManager playerLifeManager;
    HealthBarUI _healthBarUI;

    private void Start()
    {
        playerLifeManager = GameObject.FindWithTag("Player").GetComponentInChildren<LifeManager>();
    }

    private void OnTriggerEnter(Collider targetCollider)
    {
        LifeManager lifeManager = targetCollider.GetComponent<LifeManager>();
        HealthBarUI healthBarUI = targetCollider.GetComponentInChildren<HealthBarUI>();

        /*        Debug.Log(canvas);*/

        if (lifeManager != null && healthBarUI != null)
        {
            lifeManager.TakeDamage(damageAmount);
            healthBarUI.SetHealth(lifeManager.CurrentHealth);

            if (canvas != null && playerLifeManager != null)
            {
                _healthBarUI = canvas.GetComponentInChildren<HealthBarUI>();
                _healthBarUI.SetHealth(playerLifeManager.CurrentHealth);
            }
        }
    }
}