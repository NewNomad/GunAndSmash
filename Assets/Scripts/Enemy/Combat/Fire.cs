using System.Collections.Generic;
using Enemy.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace Enemy.Combat
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private EnemyBullet bulletPrefab;
        [SerializeField] private float getEnemiesDistance = 10f;
        private IObjectPool<EnemyBullet> bulletPool;
        readonly int maxBullets = 40;
        private void Awake()
        {
            bulletPool = new ObjectPool<EnemyBullet>(CreateBullet, OnGet, OnRelease, OnRemove, maxSize: maxBullets);
        }
        private EnemyBullet CreateBullet()
        {
            EnemyBullet EnemyBullet = Instantiate(bulletPrefab);
            EnemyBullet.SetPool(bulletPool);
            return EnemyBullet;
        }
        private void OnGet(EnemyBullet EnemyBullet)
        {
            EnemyBullet.gameObject.SetActive(true);
            EnemyBullet.transform.position = transform.position;
        }
        private void OnRelease(EnemyBullet EnemyBullet) => EnemyBullet.gameObject.SetActive(false);
        private void OnRemove(EnemyBullet EnemyBullet) => Destroy(EnemyBullet.gameObject);
        public void FireBullet()
        {
            GameObject enemy = GetNearestEnemy();
            if (enemy != null)
            {
                Vector2 EnemyDirection = enemy.transform.position - transform.position;
                FireTo(EnemyDirection);
            }
            else
            {
                FireTo(new Vector2(0, 1));
            }
        }

        private void FireTo(Vector2 direction)
        {
            EnemyBullet EnemyBullet = bulletPool.Get();
            EnemyBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            EnemyBullet.Launch();
        }

        private GameObject GetNearestEnemy()
        {
            GameObject[] enemies = GetEnemiesInDistance(getEnemiesDistance);
            GameObject nearestEnemy = null;
            float nearestDistance = Mathf.Infinity;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
            return nearestEnemy;
        }

        private GameObject[] GetEnemiesInDistance(float distance)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            List<GameObject> enemiesInDistance = new List<GameObject>();
            foreach (GameObject enemy in enemies)
            {
                if (Vector2.Distance(transform.position, enemy.transform.position) <= distance)
                {
                    enemiesInDistance.Add(enemy);
                }
            }
            return enemiesInDistance.ToArray();
        }
    }
}
