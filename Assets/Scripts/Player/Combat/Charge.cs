using UnityEngine;
namespace Game.Combat
{
    public class Charge : MonoBehaviour
    {
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out ICharged charged)) { return; }
            Vector2 directionToOther = other.transform.position - transform.position;
            charged.OnCharged(rb.velocity.normalized);
        }
    }
}
