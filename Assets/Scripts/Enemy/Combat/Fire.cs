using System.Collections.Generic;
using Enemy.Core;
using Game.Control;
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
            if (PlayerController.instance == null) return;
            Vector2 PlayerDirection = PlayerController.instance.transform.position - transform.position;
            FireTo(PlayerDirection);
        }

        private void FireTo(Vector2 direction)
        {
            EnemyBullet EnemyBullet = bulletPool.Get();
            EnemyBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            EnemyBullet.Launch();
        }
    }
}
