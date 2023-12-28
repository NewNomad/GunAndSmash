using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Game.Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 5;
        private int health;
        [SerializeField] float invincibleTime = 1.5f;
        private float currentInvincibleTime = 0f;
        SpriteRenderer spriteRenderer;
        Color defaultColor;
        public UnityEvent<int, int> OnHealthChanged;

        private void Awake()
        {
            TryGetComponent(out spriteRenderer);
            health = maxHealth;
            defaultColor = spriteRenderer.color;
        }

        private void Update()
        {
            currentInvincibleTime += Time.deltaTime;
        }

        public void TakeDamage(int damage)
        {
            if (currentInvincibleTime < invincibleTime) return;
            HitStopController.Instance.HitStop();
            currentInvincibleTime = 0f;
            health -= damage;
            OnHealthChanged.Invoke(health, maxHealth);
            StartCoroutine(InvincibleBlink());
            if (health <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            // TODO: 死亡時の処理
        }

        // 無敵時の点滅
        private IEnumerator InvincibleBlink()
        {
            const float blinkColorValue = 0.1f;
            Color blinkColor = new Color(defaultColor.r * blinkColorValue, defaultColor.g * blinkColorValue, defaultColor.b * blinkColorValue);

            while (currentInvincibleTime < invincibleTime)
            {
                spriteRenderer.color = blinkColor;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = defaultColor;
                yield return new WaitForSeconds(0.1f);
            }
            spriteRenderer.color = defaultColor;
            yield return null;
        }
    }
}
