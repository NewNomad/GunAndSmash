using System.Collections;
using naichilab.EasySoundPlayer.Scripts;
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
        [SerializeField] ParticleSystem takeDamageParticles;
        [SerializeField] ParticleSystem deathParticles;
        public UnityEvent onDeath;

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
            SePlayer.Instance.Play("etfx_explosion_dark01");
            Instantiate(takeDamageParticles, transform.position, Quaternion.identity);
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
            Instantiate(deathParticles, transform.localPosition, Quaternion.identity, transform);
            onDeath.Invoke();
            Destroy(gameObject, deathParticles.main.duration - 1f);
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

        public void Heal(int healAmount)
        {
            health = Mathf.Min(health + healAmount, maxHealth);
            OnHealthChanged.Invoke(health, maxHealth);
        }

        // TODO: 今回のために作った強制死亡装置
        public void ForceDeath()
        {
            if (health <= 0) return;
            health = 0;
            Death();
        }
    }
}
