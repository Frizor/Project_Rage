using UnityEngine;

public class EnemySwordAttack : MonoBehaviour
{
    public GameObject sword; // ������ �� ������ ����
    public float attackDelay = 1f; // �������� ����� �������
    public Animator anim;

    private bool isAttackReady = true;

    public void Attack()
    {
        if (isAttackReady)
        {
            // ��������� ������
            sword.SetActive(true);
            anim.SetBool("Attack", true);
            isAttackReady = false;
            Invoke("ResetAttack", attackDelay);
        }
    }

    private void ResetAttack()
    {
        // �������� ��������� �����
        sword.SetActive(false);
        anim.SetBool("Attack", false);
        isAttackReady = true;
    }
}
