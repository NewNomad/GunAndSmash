using System.Collections;
using Enemy.Core;
using Game.Combat;
using UnityEngine;
namespace Enemy.Combat
{
    public class Charged : MonoBehaviour, ICharged
    {
        Rigidbody2D rb;
        Health health;
        Stun stun;
        Charge charge;
        [SerializeField] float timeToTakeDamage = 1f;
        [SerializeField] ParticleSystem chargedParticles;

        private void Awake()
        {
            TryGetComponent(out rb);
            TryGetComponent(out health);
            TryGetComponent(out stun);
            TryGetComponent(out charge);
        }

        public void OnCharged(Vector2 direction, int damage, float knockback, bool isPlayer)
        {

            if (isPlayer && !stun.IsStun) { return; }
            HitStopController.Instance.HitStop();
            Instantiate(chargedParticles, transform.position, Quaternion.identity);
            rb.AddForce(direction * knockback, ForceMode2D.Impulse);
            StartCoroutine(TakeDamageAfterCharged(damage));
        }

        IEnumerator TakeDamageAfterCharged(int damage)
        {
            charge.CanCharge = true;
            yield return new WaitForSeconds(timeToTakeDamage);
            health.TakeDamage(damage);
            stun.IsStun = false; // 食らったらスタン解除
            charge.CanCharge = false;
            yield break;
        }
    }
}
