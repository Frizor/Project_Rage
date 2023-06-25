using UnityEngine;

public class EnemySwordAttack : MonoBehaviour
{
    public GameObject sword; // Ссылка на объект меча
    public float attackDelay = 1f; // Задержка между атаками
    public Animator anim;

    private bool isAttackReady = true;

    public void Attack()
    {
        if (isAttackReady)
        {
            // Атаковать игрока
            sword.SetActive(true);
            anim.SetBool("Attack", true);
            isAttackReady = false;
            Invoke("ResetAttack", attackDelay);
        }
    }

    private void ResetAttack()
    {
        // Сбросить состояние атаки
        sword.SetActive(false);
        anim.SetBool("Attack", false);
        isAttackReady = true;
    }
}
