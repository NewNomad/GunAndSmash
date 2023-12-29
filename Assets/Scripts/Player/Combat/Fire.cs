using System.Collections.Generic;
using Game.Core;
using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Combat
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float getEnemiesDistance = 10f;
        private IObjectPool<Bullet> bulletPool;
        readonly int maxBullets = 40;
        private void Awake()
        {
            bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGet, OnRelease, OnRemove, maxSize: maxBullets);
        }
        private Bullet CreateBullet()
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.SetPool(bulletPool);
            return bullet;
        }
        private void OnGet(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = transform.position;
        }
        private void OnRelease(Bullet bullet) => bullet.gameObject.SetActive(false);
        private void OnRemove(Bullet bullet) => Destroy(bullet.gameObject);
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
            SePlayer.Instance.Play("etfx_shoot_pistol02");

            Bullet bullet = bulletPool.Get();
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            bullet.Launch();
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
