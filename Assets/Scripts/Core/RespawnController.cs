
using System.Collections;
using System.Collections.Generic;
using Game.Control;
using Game.Core;
using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;
using UnityEngine.Events;
namespace Game.Core
{
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
        [SerializeField] float minDistanceFromPlayer = 5f;
        [SerializeField] ParticleSystem respawnParticles;
        [SerializeField] float respawnEnemiesTime = 0.1f;
        List<GameObject> activeEnemies = new List<GameObject>();
        private float respawnTimer = 0f;
        [SerializeField] int maxKills = 150;
        public int MaxKills { get => maxKills; }
        private int totalKills = 0;
        public int TotalKills { get => totalKills; }
        public UnityEvent<int, int> onKillCountChanged;
        CountDownTimer countDownTimer;
        [SerializeField] private bool IsRespawnControllerEnable = false;
        public bool EnableRespawnController { get => IsRespawnControllerEnable; set => IsRespawnControllerEnable = value; }
        public UnityEvent onKillAllEnemies;

        private void Awake()
        {
            TryGetComponent(out countDownTimer);
        }

        private void Update()
        {
            if (!IsRespawnControllerEnable) { return; }

            respawnTimer += Time.deltaTime;
            if (respawnTimer > respawnInterval)
            {
                StartCoroutine(TryRespawnEnemies());
                StartCoroutine(CheckAndRelocateEnemies());
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
            newEnemy.GetComponent<Enemy.Core.Health>().OnDead.AddListener(OnEnemyDead);
            activeEnemies.Add(newEnemy);
            if (respawnParticles != null)
            {
                Instantiate(respawnParticles, position, Quaternion.identity);
                SePlayer.Instance.Play("etfx_shoot_magic");
            }
        }

        void OnEnemyDead(GameObject enemy)
        {
            handleOnEnemyDead(enemy);
            handleOnKillCountChanged(totalKills, maxKills);
        }

        void handleOnEnemyDead(GameObject enemy)
        {
            activeEnemies.Remove(enemy);
            totalKills++;
            const float addTime = 0.5f; // FIXME: マジックナンバー
            countDownTimer.AddTime(addTime);

            if (totalKills >= maxKills) // 全ての敵を倒した場合
            {
                onKillAllEnemies.Invoke();
            }
        }

        void handleOnKillCountChanged(int totalKills, int maxKills)
        {
            if (onKillCountChanged != null)
            {
                onKillCountChanged.Invoke(totalKills, maxKills);
            }
        }

        public Vector3 GetRespawnPoint()
        {
            int maxAttempts = 100;
            float checkRadius = 1.3f; // 当たり判定をチェックする半径

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 randomPosition = PlayerController.instance.transform.position + (Vector3)(randomDirection * Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer));
                Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPosition, checkRadius);
                if (colliders.Length == 0)
                {
                    return randomPosition;
                }
            }
            return new Vector3(0, 0, 0); // 空いている位置が見つからない場合のデフォルト値
        }

        IEnumerator CheckAndRelocateEnemies()
        {
            foreach (GameObject enemy in activeEnemies)
            {
                if (enemy != null && Vector3.Distance(PlayerController.instance.transform.position, enemy.transform.position) > maxDistanceFromPlayer)
                {
                    enemy.transform.position = GetRespawnPoint();
                    if (respawnParticles != null)
                    {
                        Instantiate(respawnParticles, enemy.transform.position, Quaternion.identity);
                        SePlayer.Instance.Play("etfx_shoot_magic");
                    }
                    yield return new WaitForSeconds(respawnEnemiesTime);
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

        public void Reset()
        {
            foreach (GameObject enemy in activeEnemies)
            {
                Destroy(enemy);
            }
            activeEnemies.Clear();
            totalKills = 0;
        }
    }
}
