using System.Collections;
using Game.Core;
using UnityEngine;
namespace Game.Combat
{
    public class FireHomingLaser : MonoBehaviour
    {
        [SerializeField] HomingLaser laserPrefab;
        [SerializeField] float targetTime = 6f;
        [SerializeField] float laserSpeed = 200f;
        IDamageable target;
        Transform targetTransform;
        Rigidbody2D rb;
        private void Awake()
        {
            TryGetComponent(out rb);
        }

        public void FireHomingLaserAtTarget()
        {
            if (!IsTargetSet()) { return; }
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.Setup(targetTransform, GetHomingVelocity());
            Debug.Log(targetTransform);
        }

        // 敵とは反対で180度のランダムで飛ぶようにする
        private Vector3 GetHomingVelocity()
        {
            // FIXME: 敵がいなくてもエラーにならないようにする
            Vector3 direction = (targetTransform.position - transform.position).normalized;
            Vector3 oppositeDirection = new Vector3(-direction.x, -direction.y, 0);

            float randomAngle = Random.Range(0, 180);
            Vector3 randomDirection = Quaternion.Euler(0, 0, randomAngle) * oppositeDirection;
            return randomDirection * laserSpeed;
        }

        public bool IsTargetSet() => target != null;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable)) { return; }
            target = damageable;
            targetTransform = other.transform;
            StartCoroutine(targetTimer());
        }

        IEnumerator targetTimer()
        {
            yield return new WaitForSeconds(targetTime);
            target = null;
            targetTransform = null;
            yield break;
        }
    }
}
