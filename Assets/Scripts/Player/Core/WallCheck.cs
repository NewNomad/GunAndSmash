using UnityEngine;

namespace Game.Core
{
    public class WallCheck : MonoBehaviour
    {

        [Header("Ground Check")]
        [SerializeField] LayerMask wallLayer;
        [SerializeField] float wallCheckDistance = 0.1f;
        [SerializeField] float groundCheckDistance = 0.1f;

        public bool IsTouchingGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, wallLayer);
            return hit.collider != null;
        }

        public bool IsTouchingWall()
        {
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);
            if (hitRight.collider != null)
            {
                return true;
            }
            else
            {
                RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer);
                return hitLeft.collider != null;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
            Gizmos.DrawRay(transform.position, Vector2.right * wallCheckDistance);
            Gizmos.DrawRay(transform.position, Vector2.left * wallCheckDistance);
        }
    }
}
