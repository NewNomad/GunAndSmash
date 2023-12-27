namespace Game.Core
{
    using System.Collections.Generic;
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
            for (int i = 0; i < respawnPoints.Count; i++)
            {
                respawnPoints.Add(transform.GetChild(i).gameObject);
            }
        }

        private void Update()
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer > respawnInterval)
            {
                TryRespawnEnemies();
                respawnTimer = 0f;
            }
        }

        void TryRespawnEnemies()
        {
            int emeniesToSpawn = Mathf.Min(maxSpawnEnemiesCount, maxEnemies - activeEnemies.Count);
            for (int i = 0; i < emeniesToSpawn; i++)
            {
                GameObject enemyPrefab = ChooseEnemyPrefabs();
                if (enemyPrefab == null) { return; }
                SpawnEnemyAt(GetRespawnPoint(), enemyPrefab);
            }
        }

        void SpawnEnemyAt(Vector3 position, GameObject prefab)
        {
            GameObject newEnemy = Instantiate(prefab, position, Quaternion.identity);
            activeEnemies.Add(newEnemy);
            if (respawnParticles != null)
            {
                Instantiate(respawnParticles, position, Quaternion.identity);
            }
        }

        public Vector3 GetRespawnPoint()
        {

            for (int i = 0; i < respawnPoints.Count; i++)
            {
                if (Vector3.Distance(PlayerController.instance.transform.position, respawnPoints[i].transform.position) < maxDistanceFromPlayer)
                {
                    return respawnPoints[i].transform.position;
                }
            }
            return new Vector3(0, 0, 0);
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
}
