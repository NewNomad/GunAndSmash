using UnityEngine;
namespace Game.Combat
{
    public class Charge : MonoBehaviour
    {
        [SerializeField] int damage = 600;
        [SerializeField] float knockback = 100f;
        [SerializeField] bool canCharge = true; // プレイヤーは常時チャージできるが、敵はStun状態の時のみチャージできる
        public bool CanCharge { set => canCharge = value; }
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!canCharge) { return; }
            if (!other.gameObject.TryGetComponent(out ICharged charged)) { return; }
            Vector2 directionToOther = (other.transform.position - transform.position).normalized;
            charged.OnCharged(directionToOther, damage, knockback);
        }
    }
}
