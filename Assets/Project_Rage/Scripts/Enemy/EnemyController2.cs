using FieldOfViewAsset;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public delegate void OnTargetCaught(GameObject target);
    public static event OnTargetCaught TargetCaughtEvent;

    public Transform[] patrolPoints; // ������ ����� ��������������
    public float patrolDelayMin = 1f; // ����������� �������� �� ����� ��������������
    public float patrolDelayMax = 3f; // ������������ �������� �� ����� ��������������

    private NavMeshAgent navMeshAgent;
    private int currentPatrolIndex;
    private float patrolTimer;
    private bool isPatrolling = true;

    public Transform player;
    public float viewRadius = 10f; // ������ ������ NPC
    [Range(0, 360)]
    public float viewAngle = 90f; // ���� ������ NPC

    private bool isPlayerDetected = false;
    private FieldOfView fieldOfView;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
    }

    private void Start()
    {
        SetNextPatrolPoint();
    }

    private void Update()
    {
        if (isPatrolling)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
            {
                // ���� ���������� ����� ��������������, �������� ����� ��������� � ��������� �����
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= GetRandomPatrolDelay())
                {
                    SetNextPatrolPoint();
                }
            }
        }
        else
        {
            // ������������� � ������, ���� �� ���������
            if (isPlayerDetected)
            {
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                // ��������� � ��������������, ���� ����� �� ���������
                isPatrolling = true;
                SetNextPatrolPoint();
            }
        }
    }

    private void SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned to the EnemyController.");
            return;
        }

        // ������� ��������� ����� ��������������
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        patrolTimer = 0f;

        // ���������� ��������� ����� �������������� ��� ���� ���������
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);

        // ��������� ����������� ������, ���� ���� ��������
        isPlayerDetected = false;
    }

    private float GetRandomPatrolDelay()
    {
        return Random.Range(patrolDelayMin, patrolDelayMax);
    }

    private void OnEnable()
    {
        fieldOfView.TargetSpotted += HandleTargetSpotted;
        fieldOfView.TargetLost += HandleTargetLost;
    }

    private void OnDisable()
    {
        fieldOfView.TargetSpotted -= HandleTargetSpotted;
        fieldOfView.TargetLost -= HandleTargetLost;
    }

    private void HandleTargetSpotted(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            isPatrolling = false;
            isPlayerDetected = true;

            // ������� ������� ����������� ������
            TargetCaughtEvent?.Invoke(target);
        }
    }

    private void HandleTargetLost(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }
}
