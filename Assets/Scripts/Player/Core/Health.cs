using UnityEngine;
namespace Game.Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 5;
        private int health;
        [SerializeField] float invincibleTime = 1f;
        private float currentInvincibleTime = 0f;

        private void Awake()
        {
            health = maxHealth;
        }

        private void Update()
        {
            currentInvincibleTime += Time.deltaTime;
        }

        public void TakeDamage(int damage)
        {
            if (currentInvincibleTime < invincibleTime) return;
            currentInvincibleTime = 0f;
            health -= damage;
            Debug.Log("Health: " + health);
            if (health <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            // TODO: 死亡時の処理
        }
    }
}

