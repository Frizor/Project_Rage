/*//Странно но интересно работающий код
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerTouch : MonoBehaviour
{
    public JoystickController movementJoystick;
    public JoystickController attackJoystick;
    public Animator movementAnimator;
    public Animator attackAnimator;
    public Camera mainCamera;
    public Vector3 cameraOffset;
    public Vector3 cameraRotation;
    public float rotationSpeed = 10f;

    private Vector2 movementAxes = Vector2.zero;
    private bool isAttacking = false;

    private void Update()
    {
        HandleMovement();
        HandleAttack();
        UpdateCameraPosition();
    }

    private void HandleMovement()
    {
        movementAxes = movementJoystick.GetJoystickAxes();
        if (movementAxes != Vector2.zero)
        {
            // Обновляем позицию игрока на основе данных сенсорного джойстика
            Vector3 movement = new Vector3(movementAxes.x, 0f, movementAxes.y);
            movement.Normalize();
            transform.position += movement * Time.deltaTime;

            // Поворачиваем игрока в сторону движения
            Quaternion toRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (!movementAnimator.GetBool("Moving"))
            {
                movementAnimator.SetBool("Moving", true);
            }
        }
        else
        {
            if (movementAnimator.GetBool("Moving"))
            {
                movementAnimator.SetBool("Moving", false);
            }
        }
    }

    private void HandleAttack()
    {
        Vector2 attackAxes = attackJoystick.GetJoystickAxes();
        if (attackAxes != Vector2.zero)
        {
            // Рассчитываем угол ротации на основе вектора атаки
            float angle = Mathf.Atan2(attackAxes.x, attackAxes.y) * Mathf.Rad2Deg;

            // Поворачиваем игрока в сторону атаки
            Quaternion toRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (!isAttacking)
            {
                isAttacking = true;
                attackAnimator.SetBool("Attack", true);
            }
        }
        else
        {
            if (isAttacking)
            {
                isAttacking = false;
                attackAnimator.SetBool("Attack", false);
            }
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 cameraPosition = transform.position + cameraOffset;
        mainCamera.transform.position = cameraPosition;
        mainCamera.transform.rotation = Quaternion.Euler(cameraRotation);
        mainCamera.transform.LookAt(transform.position);
    }
}*/

//Работающий код
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerTouch : MonoBehaviour
{
    public float moveSpeed = 5f;
    public JoystickController movementJoystick;
    public JoystickController attackJoystick;
    public Animator attackAnimator;

    private Vector3 movementDirection;
    private bool isAttacking = false;

    private Camera mainCamera;
    private Vector3 cameraOffset;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - transform.position;
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
        UpdateCameraPosition();
    }

    private void HandleMovement()
    {
        // Получаем направление движения от джойстика передвижения
        Vector2 joystickAxes = movementJoystick.GetJoystickAxes();
        movementDirection = new Vector3(joystickAxes.x, 0f, joystickAxes.y).normalized;

        // Двигаем игрока по направлению
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

        // Поворачиваем игрока в сторону движения
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
    }

    private void HandleAttack()
    {
        if (attackJoystick.GetJoystickAxes() != Vector2.zero)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                attackAnimator.SetBool("Attack", true);
            }
        }
        else
        {
            if (isAttacking)
            {
                isAttacking = false;
                attackAnimator.SetBool("Attack", false);
            }
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 cameraPosition = transform.position + cameraOffset;
        mainCamera.transform.position = cameraPosition;
        mainCamera.transform.LookAt(transform.position);
    }
}

/*using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTouch : MonoBehaviour
{
    public JoystickController movementJoystick;
    public JoystickController rotationJoystick;
    public float rotationSpeed = 10f;

    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private Camera mainCamera;
    private Vector3 cameraOffset;

    private void Start()
    {
        playerTransform = transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - playerTransform.position;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();

        // Update camera position and rotation
        Vector3 cameraPosition = playerTransform.position + cameraOffset;
        mainCamera.transform.position = cameraPosition;
        mainCamera.transform.LookAt(playerTransform.position);
    }

    private void HandleMovement()
    {
        Vector3 movement = new Vector3(movementJoystick.GetJoystickAxes().x, 0f, movementJoystick.GetJoystickAxes().y);
        navMeshAgent.Move(movement * navMeshAgent.speed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector3 rotation = new Vector3(rotationJoystick.GetJoystickAxes().x, 0f, rotationJoystick.GetJoystickAxes().y);

        if (rotation != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rotation);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}*/

/*using UnityEngine;

public class PlayerControllerTouch : MonoBehaviour
{
    public JoystickController movementJoystick;
    public JoystickController rotationJoystick;
    public float movementSpeed = 5f;

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector2 joystickAxes = movementJoystick.GetJoystickAxes();
        Vector3 movement = new Vector3(joystickAxes.x, 0f, joystickAxes.y);
        movement.Normalize();

        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector2 joystickAxes = rotationJoystick.GetJoystickAxes();
        float angle = Mathf.Atan2(joystickAxes.x, joystickAxes.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
        transform.rotation = targetRotation;
    }
}*/