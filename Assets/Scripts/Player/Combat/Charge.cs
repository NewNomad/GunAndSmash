using UnityEngine;
namespace Game.Combat
{
    public class Charge : MonoBehaviour
    {
        [SerializeField] int damage = 600;
        [SerializeField] float knockback = 100f;
        [SerializeField] ParticleSystem chargedParticles;
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out ICharged charged)) { return; }
            Vector2 directionToOther = (other.transform.position - transform.position).normalized;
            charged.OnCharged(directionToOther, damage, knockback);
        }
    }
}
