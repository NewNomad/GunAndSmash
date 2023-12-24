using Game.Core;
using UnityEngine;

namespace Enemy.Core
{
    [RequireComponent(typeof(FlashObject))]
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 280;
        private int health;
        FlashObject flashObject;
        [SerializeField] ParticleSystem stunParticles;

        private void Awake()
        {
            health = maxHealth;
            TryGetComponent(out flashObject);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            Debug.Log("Enemy health: " + health);
            SetEnemyFlash();
            if (health <= 0)
            {
                // TODO: DIE()
            }
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
