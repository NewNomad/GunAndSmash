using Game.Core;
using UnityEngine;

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
            Debug.Log("stunHealth: " + stunHealth);
            stunHealth += 1;
            stunHealth = Mathf.Clamp(stunHealth, 0, stunMaxHealth);
            flashObject.ChangeColorOnPercent(GetHealthPercentage());
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
    }
}
