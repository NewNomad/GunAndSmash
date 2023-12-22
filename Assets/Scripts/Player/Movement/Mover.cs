using UnityEngine;

namespace Game.Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 moveDirection)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Impulse);
        }
    }
}
