using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController3 : MonoBehaviour
{
    public delegate void OnTargetCaught(GameObject target);
    public static event OnTargetCaught TargetCaught;

    public Transform[] patrolPoints; // Массив точек патрулирования
    public float patrolDelayMin = 1f; // Минимальная задержка на точке патрулирования
    public float patrolDelayMax = 3f; // Максимальная задержка на точке патрулирования

    public float viewRadius = 10f; // Радиус обзора NPC
    [Range(0, 360)]
    public float viewAngle = 90f; // Угол обзора NPC

    public LayerMask targetMask; // Маска целей для обнаружения
    public LayerMask obstacleMask; // Маска препятствий для обнаружения
    public float attackRange = 2f; // Расстояние для атаки мечом

    public float chaseDuration = 10f; // Продолжительность преследования в секундах

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

    private Vector3 startPosition; // Начальная позиция для возвращения после потери интереса

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

        // Заполняем список доступных для патрулирования индексов точек
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
                    // Если достигнута точка патрулирования, задержка перед движением к следующей точке
                    patrolTimer += Time.deltaTime;
                    if (patrolTimer >= GetRandomPatrolDelay())
                    {
                        SetNextPatrolPoint();
                    }
                }
            }
            else
            {
                // Передвигаться к игроку, если он обнаружен
                if (isPlayerDetected)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
                    if (distanceToPlayer <= attackRange)
                    {
                        // Начать атаку мечом
                        enemySwordAttack.Attack();
                    }

                    if (isChasingPlayer)
                    {
                        // Продолжаем преследовать игрока
                        ChasePlayer();
                    }
                    else
                    {
                        // Начинаем преследование игрока
                        StartChasingPlayer();
                    }
                }
                else
                {
                    // Вернуться к патрулированию, если игрок не обнаружен
                    isPatrolling = true;
                    SetNextPatrolPoint();
                }
            }

            FindVisibleTargets();
            if (isTakingDamage)
            {
                // Враг получил урон, начинаем преследование игрока
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

        // Выбираем случайный индекс из доступных для патрулирования
        int randomIndex = Random.Range(0, availablePatrolIndices.Count);
        currentPatrolIndex = availablePatrolIndices[randomIndex];
        patrolTimer = 0f;

        // Удаляем выбранный индекс из списка доступных
        availablePatrolIndices.RemoveAt(randomIndex);

        // Если список доступных для патрулирования точек опустел, заполняем его заново
        if (availablePatrolIndices.Count == 0)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                availablePatrolIndices.Add(i);
            }
        }

        // Установить следующую точку патрулирования как цель навигации
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);

        // Отключить обнаружение игрока, если было включено
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

            // Проверить, находится ли цель внутри угла обзора NPC
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                // Проверить, нет ли препятствий между NPC и целью
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    // Цель обнаружена
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

                // Вызвать событие обнаружения игрока
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
            // Превышена продолжительность преследования, теряем интерес
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
            // Остановить навигацию по маршруту
            navMeshAgent.isStopped = true;
            // Очистить цель навигации
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
        // Отобразить обзор NPC в редакторе сцены
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
