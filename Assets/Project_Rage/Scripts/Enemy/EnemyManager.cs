using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnSettings
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float respawnDelayMin;
    public float respawnDelayMax;
}

public class EnemyManager : MonoBehaviour
{
    public List<SpawnSettings> spawnSettingsList = new List<SpawnSettings>();
    public int maxEnemies = 5;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

            if (activeEnemies.Count < maxEnemies)
            {
                SpawnSettings settings = GetRandomSpawnSettings();
                SpawnNewEnemy(settings);
            }
        }
    }

    private SpawnSettings GetRandomSpawnSettings()
    {
        if (spawnSettingsList.Count == 0)
            return null;

        int randomIndex = Random.Range(0, spawnSettingsList.Count);
        return spawnSettingsList[randomIndex];
    }

    private void SpawnNewEnemy(SpawnSettings settings)
    {
        if (settings == null || settings.enemyPrefab == null || settings.spawnPoint == null)
            return;

        GameObject newEnemy = Instantiate(settings.enemyPrefab, settings.spawnPoint.position, settings.spawnPoint.rotation, transform);
        activeEnemies.Add(newEnemy);

        EnemyLifeManager enemyLifeManager = newEnemy.GetComponentInChildren<EnemyLifeManager>();
        enemyLifeManager.OnDeath += HandleEnemyDeath;

        float respawnDelay = Random.Range(settings.respawnDelayMin, settings.respawnDelayMax);
        StartCoroutine(RespawnEnemy(settings, respawnDelay));
    }

    private IEnumerator RespawnEnemy(SpawnSettings settings, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (activeEnemies.Count < maxEnemies)
        {
            SpawnNewEnemy(settings);
        }
    }

    private void HandleEnemyDeath(EnemyLifeManager enemyLifeManager)
    {
        GameObject enemy = enemyLifeManager.gameObject;
        activeEnemies.Remove(enemy);
        enemyLifeManager.OnDeath -= HandleEnemyDeath;
        Destroy(enemy);

        if (activeEnemies.Count < maxEnemies)
        {
            // Если количество врагов меньше максимального значения, спавним нового врага
            SpawnSettings settings = GetRandomSpawnSettings();
            SpawnNewEnemy(settings);
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int maxEnemies = 5;
    public float respawnDelayMin = 2f;
    public float respawnDelayMax = 5f;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (activeEnemies.Count < maxEnemies)
            {
                SpawnNewEnemy();
            }

            yield return new WaitForSeconds(Random.Range(respawnDelayMin, respawnDelayMax));
        }
    }

    public void SpawnNewEnemy()
    {
        if (activeEnemies.Count >= maxEnemies)
            return;

        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
        activeEnemies.Add(newEnemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        Destroy(enemy);
    }
}*/