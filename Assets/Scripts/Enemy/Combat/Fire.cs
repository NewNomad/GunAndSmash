using System.Collections;
using Enemy.Core;
using Game.Control;
using UnityEngine;
using UnityEngine.Pool;

namespace Enemy.Combat
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private float FireInterval = 3;
        [SerializeField] private int FireCount = 3;
        [SerializeField] private float BulletInterval = 30;
        [SerializeField] private int BulletCount = 3;
        [SerializeField] private EnemyBullet bulletPrefab;
        enum FireType
        {
            NWay,
            AllDirection,
        }
        [SerializeField] private FireType fireType;
        private IObjectPool<EnemyBullet> bulletPool;
        readonly int maxBullets = 300;
        bool isFireState = false;
        public bool IsFireState { get => isFireState; set => isFireState = value; }
        [SerializeField] ParticleSystem FirePrevParticle;

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
        // TODO: リファクタリング1
        public void FireBulletToPlayer()
        {
            Debug.Log("FireBulletToPlayer");
            if (PlayerController.instance == null) return;
            Vector2 PlayerDirection = PlayerController.instance.transform.position - transform.position;
            float FirstAngle = Mathf.Atan2(PlayerDirection.y, PlayerDirection.x) * Mathf.Rad2Deg;
            switch (fireType)
            {
                case FireType.NWay:
                    FirstAngle = FirstAngle - (BulletInterval * (BulletCount - 1) / 2);
                    FireBulletsToNWays(FirstAngle, BulletCount, BulletInterval);
                    break;
                case FireType.AllDirection:
                    float bulletInterval = 360 / BulletCount;
                    FireBulletsToNWays(FirstAngle, BulletCount, bulletInterval);
                    break;
            }

            FireTo(PlayerDirection);
        }

        private void FireBulletsToNWays(float angle, int number, float BulletInterval)
        {
            for (int i = 0; i < number; i++)
            {
                float currentAngle = angle + (BulletInterval * i);
                Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));
                FireTo(direction);
            }
        }

        private void FireTo(Vector2 direction)
        {
            EnemyBullet EnemyBullet = bulletPool.Get();
            EnemyBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            EnemyBullet.Launch();
        }

        public void FireBehaviors()
        {
            // TODO: SE
            StartCoroutine(FireBullets());
        }
        IEnumerator FireBullets()
        {
            isFireState = true;
            // クールダウンタイマー
            Instantiate(FirePrevParticle, transform.position, Quaternion.identity);
            const float waitTime = 1.5f;
            yield return new WaitForSeconds(waitTime);
            for (int i = 0; i < FireCount; i++)
            {
                if (isFireState == false) yield break;
                FireBulletToPlayer();
                yield return new WaitForSeconds(FireInterval);
            }
            yield return new WaitForSeconds(waitTime);
            isFireState = false;
        }
    }
}
