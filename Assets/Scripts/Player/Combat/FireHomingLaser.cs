using System.Collections;
using Game.Core;
using UnityEngine;
namespace Game.Combat
{
    public class FireHomingLaser : MonoBehaviour
    {
        [SerializeField] HomingLaser laserPrefab;
        [SerializeField] float targetTime = 6f;
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
            laser.Setup(targetTransform);
            Debug.Log(targetTransform);
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
