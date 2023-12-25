using Game.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy.Core
{
    [RequireComponent(typeof(FlashObject))]
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 580;
        private int health;
        [SerializeField] private int stunMaxHealth = 280;
        private int stunHealth;
        FlashObject flashObject;
        [SerializeField] ParticleSystem stunParticles;
        [SerializeField] ParticleSystem deathParticles;
        public UnityEvent onStunned;
        private bool canHealStun = true;
        public bool CanHealStun { get => canHealStun; set => canHealStun = value; }

        private void Awake()
        {
            health = maxHealth;
            stunHealth = stunMaxHealth;
            TryGetComponent(out flashObject);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            stunHealth -= damage;
            SetEnemyFlash();
            if (stunHealth <= 0 && canHealStun)
            {
                canHealStun = false;
                onStunned.Invoke();
                Instantiate(stunParticles, transform.position, Quaternion.identity);
                HitStopController.Instance.HitStop();
            }
            if (health <= 0)
            {
                Dead();
            }
        }

        private void HealStun()
        {
            if (!canHealStun) return;
            stunHealth += 1;
            stunHealth = Mathf.Clamp(stunHealth, 0, stunMaxHealth);
            flashObject.ChangeColorOnPercent(GetHealthPercentage());
        }

        private void Dead()
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            HealStun();
        }


        private void SetEnemyFlash()
        {
            if (flashObject == null) return;
            flashObject.GetEnemyFlashTime(GetHealthPercentage());
        }

        private float GetHealthPercentage()
        {
            return (float)stunHealth / (float)stunMaxHealth;
        }

        public void SetStunFull()
        {
            stunHealth = stunMaxHealth;
        }
    }
}
