
using System.Collections;
using System.Collections.Generic;
using Enemy.Core;
using Game.Control;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [System.Serializable]

    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int spawnProbability;
        public int requireKillCount;
    }
    [SerializeField] List<EnemySpawnInfo> enemyTypes;
    [SerializeField] int maxEnemies = 10;
    [SerializeField] int maxSpawnEnemiesCount = 5;
    [SerializeField] float respawnInterval = 5f;
    [SerializeField] float maxDistanceFromPlayer = 10f;
    [SerializeField] ParticleSystem respawnParticles;
    [SerializeField] float respawnEnemiesTime = 0.1f;
    List<GameObject> respawnPoints = new List<GameObject>();
    List<GameObject> activeEnemies = new List<GameObject>();
    private float respawnTimer = 0f;
    private int totalKills = 0;

    private void Awake()
    {
        InitializeRespawnPoint();
    }

    void InitializeRespawnPoint()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            respawnPoints.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        respawnTimer += Time.deltaTime;
        if (respawnTimer > respawnInterval)
        {
            StartCoroutine(TryRespawnEnemies());
            CheckAndRelocateEnemies();
            respawnTimer = 0f;
        }
    }

    IEnumerator TryRespawnEnemies()
    {
        int emeniesToSpawn = Mathf.Min(maxSpawnEnemiesCount, maxEnemies - activeEnemies.Count);
        for (int i = 0; i < emeniesToSpawn; i++)
        {
            GameObject enemyPrefab = ChooseEnemyPrefabs();
            if (enemyPrefab != null)
            {
                Vector3 respawnPoint = GetRespawnPoint();
                SpawnEnemyAt(respawnPoint, enemyPrefab);
                yield return new WaitForSeconds(respawnEnemiesTime);
            }
        }
    }

    void SpawnEnemyAt(Vector3 position, GameObject prefab)
    {
        GameObject newEnemy = Instantiate(prefab, position, Quaternion.identity);
        newEnemy.GetComponent<Health>().OnDead.AddListener(OnEnemyDead);
        activeEnemies.Add(newEnemy);
        if (respawnParticles != null)
        {
            Instantiate(respawnParticles, position, Quaternion.identity);
        }
    }

    void OnEnemyDead(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        totalKills++;
    }

    public Vector3 GetRespawnPoint()
    {
        List<GameObject> suitablePoints = new List<GameObject>();

        for (int i = 0; i < respawnPoints.Count; i++)
        {
            if (Vector3.Distance(PlayerController.instance.transform.position, respawnPoints[i].transform.position) < maxDistanceFromPlayer)
            {
                suitablePoints.Add(respawnPoints[i]);
            }
        }

        if (suitablePoints.Count > 0)
        {
            int randomPoint = Random.Range(0, suitablePoints.Count);
            return suitablePoints[randomPoint].transform.position;
        }
        if (respawnPoints.Count > 0)
        {

            int randomPoint = Random.Range(0, respawnPoints.Count);
            return respawnPoints[randomPoint].transform.position;
        }
        return new Vector3(0, 30, 0);
    }

    void CheckAndRelocateEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null && Vector3.Distance(PlayerController.instance.transform.position, enemy.transform.position) > maxDistanceFromPlayer)
            {
                enemy.transform.position = GetRespawnPoint();
                if (respawnParticles != null)
                {
                    Instantiate(respawnParticles, enemy.transform.position, Quaternion.identity);
                }
            }
        }
    }

    GameObject ChooseEnemyPrefabs()
    {
        List<EnemySpawnInfo> availableEnemies = enemyTypes.FindAll(enemy => enemy.requireKillCount <= totalKills);
        int totalProbability = 0;
        foreach (EnemySpawnInfo enemy in availableEnemies)
        {
            totalProbability += enemy.spawnProbability;
        }
        int randomPoint = Random.Range(0, totalProbability);
        int currentProbability = 0;
        foreach (EnemySpawnInfo enemy in availableEnemies)
        {
            currentProbability += enemy.spawnProbability;
            if (currentProbability > randomPoint)
            {
                return enemy.enemyPrefab;
            }
        }
        return null;
    }
}
