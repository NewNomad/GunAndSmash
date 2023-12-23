using Game.Core;
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
    }
}
