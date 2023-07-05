using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GameObject NavigationSurface;
    public float cameraHeight = 5f;
    public float minFieldOfView = 30f;
    public float maxFieldOfView = 90f;
    public float zoomSpeed = 5f;
    public float accelerationSpeed = 1000f;
    public float accelerationDuration = 0.5f;
    public float accelerationCooldown = 10f;
    public float rotationSpeed = 10f; // Speed of rotation on right-click

    public bool useJoystickControl = true; // Enable or disable joystick control in the inspector

    private NavMeshAgent _navMeshAgent;
    private Camera _mainCamera;
    private Vector3 _cameraOffset;
    private bool _isAccelerating;
    private float _originalSpeed;
    private float _accelerationTimer;
    private float _cooldownTimer;

    private JoystickController _joystickController; // Reference to the JoystickController script

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
        _cameraOffset = _mainCamera.transform.position - transform.position;
        _originalSpeed = _navMeshAgent.speed;
        _accelerationTimer = 0f;
        _cooldownTimer = 0f;

        _joystickController = FindObjectOfType<JoystickController>();
    }

    private void LateUpdate()
    {
        if (useJoystickControl && _joystickController != null)
        {
            // Move and rotate the player with the joystick
            MovePlayerWithJoystick();
            RotatePlayerWithJoystick();
        }
        else
        {
            // Move and rotate the player with the mouse (for PC)
            MovePlayerWithMouse();
            RotatePlayerWithMouse();
        }

        // Update camera position and field of view
        Vector3 cameraPosition = transform.position + _cameraOffset;
        _mainCamera.transform.position = cameraPosition;
        _mainCamera.transform.LookAt(transform.position);

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        float fieldOfView = _mainCamera.fieldOfView;
        fieldOfView -= scrollWheel * zoomSpeed;
        fieldOfView = Mathf.Clamp(fieldOfView, minFieldOfView, maxFieldOfView);
        _mainCamera.fieldOfView = fieldOfView;

        // Handle acceleration and cooldown
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isAccelerating && _cooldownTimer <= 0f)
            {
                StartAcceleration();
            }
        }

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

    private void MovePlayerWithMouse()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Set the destination for NavMeshAgent
                _navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    private void MovePlayerWithJoystick()
    {
        Vector2 joystickAxes = _joystickController.GetJoystickAxes();
        if (joystickAxes.magnitude > 0.01f)
        {
            Vector3 movement = new Vector3(joystickAxes.x, 0f, joystickAxes.y);
            movement.Normalize();

            // Set the destination for NavMeshAgent
            _navMeshAgent.SetDestination(transform.position + movement);
        }
    }

    private void RotatePlayerWithMouse()
    {
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 targetDirection = hit.point - transform.position;
                targetDirection.y = 0f;
                if (targetDirection.magnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void RotatePlayerWithJoystick()
    {
        Vector2 joystickAxes = _joystickController.GetJoystickAxes();
        if (joystickAxes.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(joystickAxes.x, joystickAxes.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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



/*using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GameObject NavigationSurface;
    public float cameraHeight = 5f;
    public float minFieldOfView = 30f;
    public float maxFieldOfView = 90f;
    public float zoomSpeed = 5f;
    public float accelerationSpeed = 1000f;
    public float accelerationDuration = 0.5f;
    public float accelerationCooldown = 10f;
    public float rotationSpeed = 10f; // Speed of rotation on right-click

    private NavMeshAgent _navMeshAgent;
    private Camera _mainCamera;
    private Vector3 _cameraOffset;
    private bool _isAccelerating;
    private float _originalSpeed;
    private float _accelerationTimer;
    private float _cooldownTimer;

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
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("LMB");
            MovePlayerTo(Input.mousePosition);
        }

        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("RMB");
            RotatePlayer();
        }

        Vector3 cameraPosition = transform.position + _cameraOffset;
        _mainCamera.transform.position = cameraPosition;
        _mainCamera.transform.LookAt(transform.position);

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        float fieldOfView = _mainCamera.fieldOfView;
        fieldOfView -= scrollWheel * zoomSpeed;
        fieldOfView = Mathf.Clamp(fieldOfView, minFieldOfView, maxFieldOfView);
        _mainCamera.fieldOfView = fieldOfView;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isAccelerating && _cooldownTimer <= 0f)
            {
                StartAcceleration();
            }
        }

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

    private void RotatePlayer()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0f;
            if (targetDirection.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
*/