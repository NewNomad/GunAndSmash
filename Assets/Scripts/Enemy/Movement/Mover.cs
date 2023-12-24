using Game.Control;
using UnityEngine;

namespace Enemy.Movement
{
    public class Mover : MonoBehaviour
    {

        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float distanceToPlayer = 1f;

        PlayerController playerController;
        Rigidbody2D rb;
        private void Awake()
        {
            TryGetComponent(out rb);
        }

        private void FixedUpdate()
        {
            MoveToPlayer();
        }

        private void MoveTo(Vector2 target)
        {
            Vector2 moveDirection = (target - (Vector2)transform.position).normalized;
            rb.AddForce(moveDirection * moveSpeed);
        }

        private void MoveToPlayer()
        {
            Debug.Log(playerController);
            MoveTo(PlayerController.instance.transform.position);
        }

        public bool IsPlayerInRange()
        {
            return Vector2.Distance(transform.position, playerController.transform.position) < distanceToPlayer;
        }
    }
}
