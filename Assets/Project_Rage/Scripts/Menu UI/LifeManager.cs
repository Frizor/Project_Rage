using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeManager : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье
    private int currentHealth; // Текущее здоровье

    // public float destroyDelay = 5f; // Задержка перед уничтожением объекта после смерти

    public int CurrentHealth
    {
        get { return currentHealth; } //you can do something like this mod function 
    }

    private void Start()
    {
        currentHealth = maxHealth; // Установка начального здоровья
    }

    public void TakeDamage(int damageAmount)
    {
      /*  Debug.Log($"damageAmount = {damageAmount}");*/
        if (currentHealth > 0)
        {
			currentHealth -= damageAmount;
      /*      Debug.Log($"currentHealth = {currentHealth}");*/
         
        } else {
			Die();
		}
    }

     private void Die()
     {
        Debug.Log("DIE");
       /*  if (gameObject.CompareTag("Player"))
         {
             // Действия при смерти игрока
             // Например, вызов контекстного меню для выбора возрождения
    
             // Запустить корутину для удаления объекта игрока через 10 секунд
             StartCoroutine(DestroyPlayer(10f));
         }
         else
         {
            // Действия при смерти врага
             // Например, воспроизведение анимации, добавление очков игроку и т. д.
             // Остановить движение врага
             GetComponent<EnemyController3>().StopMovement();
    
             // Запустить корутину для удаления объекта врага через 10 секунд
             StartCoroutine(DestroyEnemy(10f));
        }*/
     }

    // private IEnumerator DestroyPlayer(float delay)
    // {
    //     // Ожидание перед удалением объекта игрока
    //     yield return new WaitForSeconds(delay);
    //
    //     // Удаление объекта игрока
    //     Destroy(gameObject);
    // }
    //
    // private IEnumerator DestroyEnemy(float delay)
    // {
    //     // Ожидание перед удалением объекта врага
    //     yield return new WaitForSeconds(delay);
    //
    //     // Удаление объекта врага
    //     Destroy(gameObject);
    // }
}