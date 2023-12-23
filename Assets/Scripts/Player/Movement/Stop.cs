using UnityEngine;

namespace Game.Move
{
    public class Stop : MonoBehaviour
    {
        Rigidbody2D rb;
        float defaultGravityScale;
        private void Awake()
        {
            defaultGravityScale = GetComponent<Rigidbody2D>().gravityScale;
            rb = GetComponent<Rigidbody2D>();
        }
        public void StopMove()
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
        public void FinishMove()
        {
            rb.gravityScale = defaultGravityScale;
        }
    }
}
