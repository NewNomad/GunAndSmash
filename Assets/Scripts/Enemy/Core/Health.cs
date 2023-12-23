using UnityEngine;

namespace Enemy.Core
{
    public class Health : MonoBehaviour
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
