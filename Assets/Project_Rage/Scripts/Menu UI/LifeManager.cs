using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Unity.VisualScripting;

public class LifeManager : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private LifeManager lifeManager;
    private EnemyController3 enemyController;
    public int maxHealth = 100; // Максимальное здоровье
    private int currentHealth; // Текущее здоровье
    private bool isDead = false;

    // public float destroyDelay = 5f; // Задержка перед уничтожением объекта после смерти

    public int CurrentHealth
    {
        get { return currentHealth; } //you can do something like this mod function 
    }

    private void Start()
    {
        currentHealth = maxHealth; // Установка начального здоровья
        navMeshAgent = GetComponent<NavMeshAgent>(); // Получение ссылки на компонент NavMeshAgent
        lifeManager = GetComponent<LifeManager>(); // Получение ссылки на самого себя (LifeManager)
        enemyController = GetComponent<EnemyController3>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            if (enemyController != null && gameObject.CompareTag("Enemy"))
            {
                enemyController.StartChasingPlayer();
            }
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (GetComponent<PlayerController>() != null)
        {
            // Код для игрока
            // Пример: SceneManager.LoadScene("GameOverScene");
        }
        else if (GetComponent<EnemyController3>() != null)
        {
            // Остановить движение врага
            GetComponent<EnemyController3>().StopMovement();

            // Отключить компонент NavMeshAgent
            navMeshAgent.enabled = false;

            // Удалить объект врага через 10 секунд
            StartCoroutine(DestroyEnemy(10f));
        }

        // Очистить ссылку на LifeManager, чтобы избежать лишних вызовов метода TakeDamage
        lifeManager = null;
    }

    private IEnumerator DestroyEnemy(float delay)
    {
        // Ожидание перед удалением объекта врага
        yield return new WaitForSeconds(delay);

        // Удаление объекта врага
        Destroy(this.gameObject);
    }
}
