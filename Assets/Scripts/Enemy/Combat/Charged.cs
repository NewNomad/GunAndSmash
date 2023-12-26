using Enemy.Core;
using UnityEngine;
namespace Enemy.Combat
{
    public class Charged : MonoBehaviour, ICharged
    {
        Rigidbody2D rb;
        Health health;
        private void Awake()
        {
            TryGetComponent(out rb);
            TryGetComponent(out health);
        }

        public void OnCharged(Vector2 direction, int damage, float knockback)
        {
            HitStopController.Instance.HitStop();
            rb.AddForce(direction * knockback, ForceMode2D.Impulse);
            // health.TakeDamage(damage);
        }
    }
}
