using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using UnityEngine;
namespace Game.Combat
{
    public class FireHomingLaser : MonoBehaviour
    {
        [SerializeField] HomingLaser laserPrefab;
        [SerializeField] float targetTime = 6f;
        [SerializeField] float laserSpeed = 200f;
        List<IDamageable> target = new List<IDamageable>();
        List<Transform> targetTransform = new List<Transform>();
        Rigidbody2D rb;
        private void Awake()
        {
            TryGetComponent(out rb);
        }

        public void FireHomingLaserAtTarget()
        {
            foreach (Transform target in targetTransform)
            {
                if (target == null) { continue; }
                HomingLaser laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                laser.Setup(target, GetHomingVelocity(target.transform.position));
            }
        }

        // 敵とは反対で180度のランダムで飛ぶようにする
        private Vector3 GetHomingVelocity(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            Vector3 oppositeDirection = new Vector3(-direction.x, -direction.y, 0);

            float randomAngle = Random.Range(0, 180);
            Vector3 randomDirection = Quaternion.Euler(0, 0, randomAngle) * oppositeDirection;
            return randomDirection * laserSpeed;
        }

        public bool IsTargetSet() => target.Count != 0;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out IDamageable damageable)) { return; }
            StartCoroutine(targetTimer(damageable, other.transform));
        }

        IEnumerator targetTimer(IDamageable target, Transform targetTransform)
        {
            this.target.Add(target);
            this.targetTransform.Add(targetTransform);
            // 重複を削除
            this.target = this.target.Distinct().ToList();
            this.targetTransform = this.targetTransform.Distinct().ToList();
            yield return new WaitForSeconds(targetTime);

            // 対象の要素がなければ終了
            if (!this.target.Contains(target)) { yield break; }

            this.target.Remove(target);
            this.targetTransform.Remove(targetTransform);
            yield break;
        }
    }
}
