using System.Collections;
using Enemy.Core;
using Game.Combat;
using Game.Core;
using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;
namespace Enemy.Combat
{
    public class Charged : MonoBehaviour, ICharged
    {
        Rigidbody2D rb;
        Enemy.Core.Health health;
        Stun stun;
        Charge charge;
        [SerializeField] float timeToTakeDamage = 1f;
        [SerializeField] ParticleSystem chargedParticles;
        [SerializeField] float addTimeOnCharged = 1f;

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
            SePlayer.Instance.Play("etfx_shoot_lightning2");
            rb.AddForce(direction * knockback, ForceMode2D.Impulse);
            CountDownTimer.instance.AddTime(1f);
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
