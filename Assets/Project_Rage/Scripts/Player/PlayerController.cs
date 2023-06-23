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
