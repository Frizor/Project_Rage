using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController3 : MonoBehaviour
{
    public delegate void OnTargetCaught(GameObject target);
    public static event OnTargetCaught TargetCaught;

    public Transform[] patrolPoints; // ������ ����� ��������������
    public float patrolDelayMin = 1f; // ����������� �������� �� ����� ��������������
    public float patrolDelayMax = 3f; // ������������ �������� �� ����� ��������������

    public float viewRadius = 10f; // ������ ������ NPC
    [Range(0, 360)]
    public float viewAngle = 90f; // ���� ������ NPC

    public LayerMask targetMask; // ����� ����� ��� �����������
    public LayerMask obstacleMask; // ����� ����������� ��� �����������
    public float attackRange = 2f; // ���������� ��� ����� �����

    public float chaseDuration = 10f; // ����������������� ������������� � ��������

    private NavMeshAgent navMeshAgent;
    //private LifeManager lifeManager;
    private EnemySwordAttack enemySwordAttack;
    private int currentPatrolIndex;
    private float patrolTimer;
    private bool isPatrolling = true;
    private bool isPlayerDetected = false;
    private bool isChasingPlayer = false;
    private bool isTakingDamage = false;
    private float chaseTimer;

    private Vector3 startPosition; // ��������� ������� ��� ����������� ����� ������ ��������

    private List<int> availablePatrolIndices = new List<int>();

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //lifeManager = GetComponent<LifeManager>();
        enemySwordAttack = GetComponent<EnemySwordAttack>();

    }

    private void Start()
    {
        startPosition = transform.position;

        // ��������� ������ ��������� ��� �������������� �������� �����
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            availablePatrolIndices.Add(i);
        }

        SetNextPatrolPoint();
    }

    private void Update()
    {
        if (navMeshAgent.enabled)
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
                    float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
                    if (distanceToPlayer <= attackRange)
                    {
                        // ������ ����� �����
                        enemySwordAttack.Attack();
                    }

                    if (isChasingPlayer)
                    {
                        // ���������� ������������ ������
                        ChasePlayer();
                    }
                    else
                    {
                        // �������� ������������� ������
                        StartChasingPlayer();
                    }
                }
                else
                {
                    // ��������� � ��������������, ���� ����� �� ���������
                    isPatrolling = true;
                    SetNextPatrolPoint();
                }
            }

            FindVisibleTargets();
            if (isTakingDamage)
            {
                // ���� ������� ����, �������� ������������� ������
                StartChasingPlayer();
                isTakingDamage = false;
            }
        }
    }

    private void SetNextPatrolPoint()
    {
        if (availablePatrolIndices.Count == 0)
        {
            Debug.LogWarning("No patrol points assigned to the EnemyController.");
            return;
        }

        // �������� ��������� ������ �� ��������� ��� ��������������
        int randomIndex = Random.Range(0, availablePatrolIndices.Count);
        currentPatrolIndex = availablePatrolIndices[randomIndex];
        patrolTimer = 0f;

        // ������� ��������� ������ �� ������ ���������
        availablePatrolIndices.RemoveAt(randomIndex);

        // ���� ������ ��������� ��� �������������� ����� �������, ��������� ��� ������
        if (availablePatrolIndices.Count == 0)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                availablePatrolIndices.Add(i);
            }
        }

        // ���������� ��������� ����� �������������� ��� ���� ���������
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);

        // ��������� ����������� ������, ���� ���� ��������
        isPlayerDetected = false;
        isChasingPlayer = false;
    }

    private float GetRandomPatrolDelay()
    {
        return Random.Range(patrolDelayMin, patrolDelayMax);
    }

    private void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // ���������, ��������� �� ���� ������ ���� ������ NPC
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                // ���������, ��� �� ����������� ����� NPC � �����
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    // ���� ����������
                    HandleTargetSpotted(target.gameObject);
                }
            }
        }
    }

    private void HandleTargetSpotted(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            if (!isPlayerDetected)
            {
                isPatrolling = false;
                isPlayerDetected = true;

                // ������� ������� ����������� ������
                TargetCaught?.Invoke(target);

                chaseTimer = 0f;
            }
        }
    }

    public void StartChasingPlayer()
    {
        isChasingPlayer = true;
        navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    private void ChasePlayer()
    {
        if (chaseTimer >= chaseDuration)
        {
            // ��������� ����������������� �������������, ������ �������
            isChasingPlayer = false;
            isPlayerDetected = false;
            navMeshAgent.SetDestination(startPosition);
        }
        else
        {
            chaseTimer += Time.deltaTime;
            navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }

    public void StopMovement()
    {
        if (navMeshAgent != null)
        {
            // ���������� ��������� �� ��������
            navMeshAgent.isStopped = true;
            // �������� ���� ���������
            navMeshAgent.ResetPath();
        }
    }

    public void TakeDamage()
    {
        if (!isChasingPlayer)
        {
            isTakingDamage = true;
        }
    }


    private void OnEnable()
    {
        TargetCaught += HandleTargetSpotted;
    }

    private void OnDisable()
    {
        TargetCaught -= HandleTargetSpotted;
    }

    private void OnDrawGizmosSelected()
    {
        // ���������� ����� NPC � ��������� �����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
