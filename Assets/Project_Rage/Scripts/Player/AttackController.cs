using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    public JoystickController rotationJoystick;
    public Button attackButton;
    public float attackRange = 2f;
    public float attackCooldown = 1f;

    private Transform playerTransform;
    private float attackTimer = 0f;
    private bool canAttack = true;

    private void Start()
    {
        playerTransform = transform;
        attackButton.onClick.AddListener(OnAttackButtonClick);
    }

    private void Update()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0f;
            }
        }
    }

    private void OnAttackButtonClick()
    {
        if (canAttack && rotationJoystick.GetJoystickAxes() != Vector2.zero)
        {
            Vector3 attackDirection = new Vector3(rotationJoystick.GetJoystickAxes().x, 0f, rotationJoystick.GetJoystickAxes().y).normalized;
            Attack(attackDirection);
        }
    }

    private void Attack(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, direction, out hit, attackRange))
        {
            // Произведите действия, связанные с атакой на объект hit
            Debug.Log("Attack!");

            canAttack = false;
        }
    }
}