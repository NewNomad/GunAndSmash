using UnityEngine;

namespace Game.Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 moveDirection, float moveSpeed)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Impulse);
        }
    }
}
