using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public delegate void OnPlayerExit(GameObject target);
    public static event OnPlayerExit PlayerExit;

    public GameObject NavigationSurface;
    public GameObject Target;
    public float cameraDistance = 10f;
    public float cameraHeight = 5f;

    private NavMeshAgent _navMeshAgent;
    private bool _gameFinished;
    private Camera _mainCamera;
    private Vector3 _cameraOffset;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
        _cameraOffset = _mainCamera.transform.position - transform.position;
    }

    private void LateUpdate()
    {
        /*if (_gameFinished)
        {
            return;
        }

        if (Vector3.Distance(Target.transform.position, transform.position) < 2)
        {
            if (PlayerExit != null)
            {
                PlayerExit(gameObject);
                _gameFinished = true;
            }
        }*/

        // mouse click and hold
        if (Input.GetMouseButton(0))
        {
            MovePlayerTo(Input.mousePosition);
        }

        // Обновление позиции камеры
        Vector3 cameraPosition = transform.position + _cameraOffset;
        _mainCamera.transform.position = cameraPosition;
        _mainCamera.transform.LookAt(transform.position);
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
}
