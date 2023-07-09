using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnEnemy;
    [SerializeField]
    private Transform[] spawnPoints;

    public float startSpawnerInterval;
    private float spawnerInterval;

    [SerializeField]
    private int numberOfEnemy;
    [SerializeField]
    private int nowTheEnemy;

    private int randEnemy;
    private int randPoint;

    private List<GameObject> activeEnemies = new List<GameObject>(); // ������ �������� ������

    void Start()
    {
        spawnerInterval = startSpawnerInterval;
    }

    void Update()
    {
        if (spawnerInterval <= 0 && nowTheEnemy < numberOfEnemy)
        {
            randEnemy = Random.Range(0, spawnEnemy.Length);
            randPoint = Random.Range(0, spawnPoints.Length);

            GameObject newEnemy = Instantiate(spawnEnemy[randEnemy], spawnPoints[randPoint].position, Quaternion.identity);
            activeEnemies.Add(newEnemy); // ��������� ������ ����� � ������ �������� ������

            spawnerInterval = startSpawnerInterval;
            nowTheEnemy++;
        }
        else
        {
            spawnerInterval -= Time.deltaTime;
        }

        // ��������� ���������� �������� ������ � ��������� nowTheEnemy
        nowTheEnemy = activeEnemies.Count;
        if (nowTheEnemy >= numberOfEnemy)
        {
            bool allEnemiesDead = true;
            foreach (GameObject enemy in activeEnemies)
            {
                if (enemy != null)
                {
                    allEnemiesDead = false;
                    break;
                }
            }

            if (allEnemiesDead)
            {
                nowTheEnemy = 0; // ���������� nowTheEnemy, ���� ��� ����� ����������
                activeEnemies.Clear(); // ������� ������ �������� ������
            }
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnEnemy;
    [SerializeField]
    private Transform[] spawnPoints;

    public float startSpawnerInterval;
    private float spawnerInterval;

    public int numberOfEnemy;
    private int nowTheEnemy;

    private int randEnemy;
    private int randPoint;

    private List<GameObject> activeEnemies = new List<GameObject>(); // ������ �������� ������

    void Start()
    {
        spawnerInterval = startSpawnerInterval;
    }

    void Update()
    {
        if (spawnerInterval <= 0 && nowTheEnemy < numberOfEnemy)
        {
            randEnemy = Random.Range(0, spawnEnemy.Length);
            randPoint = Random.Range(0, spawnPoints.Length);

            GameObject newEnemy = Instantiate(spawnEnemy[randEnemy], spawnPoints[randPoint].position, Quaternion.identity);
            activeEnemies.Add(newEnemy); // ��������� ������ ����� � ������ �������� ������

            spawnerInterval = startSpawnerInterval;
            nowTheEnemy++;
        }
        else
        {
            spawnerInterval -= Time.deltaTime;
        }

        // ��������� ���������� �������� ������ � ��������� nowTheEnemy
        nowTheEnemy = activeEnemies.Count;
        if (nowTheEnemy >= numberOfEnemy)
        {
            bool allEnemiesDead = true;
            foreach (GameObject enemy in activeEnemies)
            {
                if (enemy != null)
                {
                    allEnemiesDead = false;
                    break;
                }
            }

            if (allEnemiesDead)
            {
                nowTheEnemy = 0; // ���������� nowTheEnemy, ���� ��� ����� ����������
                activeEnemies.Clear(); // ������� ������ �������� ������
            }
        }
    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnEnemy;
    [SerializeField]
    private Transform[] spawnPoints;

    public float startSpawnerInterval;
    private float spawnerInterval;

    public int numberOfEnemy;
    public int nowTheEnemy;

    private int randEnemy;
    private int randPoint;

    void Start()
    {
        spawnerInterval = startSpawnerInterval;
    }

    void Update()
    {
        if (spawnerInterval <= 0 && nowTheEnemy < numberOfEnemy)
        {
            randEnemy = Random.Range(0, spawnEnemy.Length);
            randPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(spawnEnemy[randEnemy], spawnPoints[randPoint].position, Quaternion.identity);
            spawnerInterval = startSpawnerInterval;
            nowTheEnemy++;
        }
        else
        {
            spawnerInterval -= Time.deltaTime;
        }
    }
}*/