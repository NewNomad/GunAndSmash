using Game.Core;
using UnityEngine;

namespace Enemy.Core
{
    [RequireComponent(typeof(FlashObject))]
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 280;
        private int health;
        [SerializeField] private int stunMaxHealth = 180;
        private int stunHealth;
        FlashObject flashObject;
        [SerializeField] ParticleSystem stunParticles;

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
            if (stunHealth <= 0)
            {
                Debug.Log("Enemy is dead");
                Instantiate(stunParticles, transform.position, Quaternion.identity);
                HitStopController.Instance.HitStop();
                // TODO: DIE()
            }

        }

        private void FixedUpdate()
        {
            stunHealth += 1;
            stunHealth = Mathf.Clamp(stunHealth, 0, stunMaxHealth);
        }


        private void SetEnemyFlash()
        {
            if (flashObject == null) return;
            flashObject.GetEnemyFlashTime(GetHealthPercentage());
        }

        private float GetHealthPercentage()
        {
            return (float)health / (float)maxHealth;
        }
    }
}
