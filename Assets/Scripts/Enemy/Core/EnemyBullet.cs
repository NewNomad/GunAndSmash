using Game.Core;
using UnityEngine;
using UnityEngine.Pool;
namespace Enemy.Core
{
    // TODO: PlayerのBulletと共通化
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private int damage = 100;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 1f;
        private float currentLifeTime = 0f;
        Rigidbody2D rb;
        IObjectPool<EnemyBullet> bulletPool;
        bool isRelease = false;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void OnEnable()
        {
            isRelease = false;
        }

        private void OnDisable()
        {
            rb.velocity = Vector2.zero;
        }
        public void Launch()
        {
            rb.velocity = transform.up * speed;
        }
        public void SetPool(IObjectPool<EnemyBullet> bulletPool) => this.bulletPool = bulletPool;
        private void DisableThisGameObject()
        {
            if (isRelease) return;
            bulletPool.Release(this);
            isRelease = true;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
            if (other.CompareTag("Player")) return;
            DisableThisGameObject();
        }
    }

}
