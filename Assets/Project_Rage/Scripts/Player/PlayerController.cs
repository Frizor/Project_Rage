using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public GameObject NavigationSurface;
    public float cameraHeight = 5f;
    public float minFieldOfView = 30f; // Минимальный обзор
    public float maxFieldOfView = 90f; // Максимальный обзор
    public float zoomSpeed = 5f; // Скорость приближения/отдаления камеры
    public float accelerationSpeed = 1000f; // Скорость ускорения
    public float accelerationDuration = 0.5f; // Продолжительность ускорения
    public float accelerationCooldown = 10f; // Период восстановления ускорения

    private NavMeshAgent _navMeshAgent;
    private bool _gameFinished;
    private Camera _mainCamera;
    private Vector3 _cameraOffset;
    private bool _isAccelerating; // Флаг активации ускорения
    private float _originalSpeed; // Исходная скорость NavMeshAgent
    private float _accelerationTimer; // Таймер для ускорения
    private float _cooldownTimer; // Таймер перезарядки

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
        _cameraOffset = _mainCamera.transform.position - transform.position;
        _originalSpeed = _navMeshAgent.speed;
        _accelerationTimer = 0f;
        _cooldownTimer = 0f;
    }

    private void LateUpdate()
    {
        // mouse click and hold
        if (Input.GetMouseButton(0))
        {
            MovePlayerTo(Input.mousePosition);
        }

        // Обновление позиции камеры
        Vector3 cameraPosition = transform.position + _cameraOffset;
        _mainCamera.transform.position = cameraPosition;
        _mainCamera.transform.LookAt(transform.position);

        // Управление камерой через Field of View (FOV)
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        float fieldOfView = _mainCamera.fieldOfView;
        fieldOfView -= scrollWheel * zoomSpeed;
        fieldOfView = Mathf.Clamp(fieldOfView, minFieldOfView, maxFieldOfView);
        _mainCamera.fieldOfView = fieldOfView;

        // Ускорение при нажатии на пробел (Spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isAccelerating && _cooldownTimer <= 0f)
            {
                StartAcceleration();
            }
        }

        // Обновление таймеров
        if (_isAccelerating)
        {
            _accelerationTimer -= Time.deltaTime;
            if (_accelerationTimer <= 0f)
            {
                StopAcceleration();
            }
        }
        else if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    private void MovePlayerTo(Vector3 position)
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == NavigationSurface)
            {
                _navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    private void StartAcceleration()
    {
        _isAccelerating = true;
        _navMeshAgent.speed = accelerationSpeed;
        _accelerationTimer = accelerationDuration;
    }

    private void StopAcceleration()
    {
        _isAccelerating = false;
        _navMeshAgent.speed = _originalSpeed;
        _cooldownTimer = accelerationCooldown;
    }
}