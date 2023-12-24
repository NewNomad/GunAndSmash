using Game.Control;
using UnityEngine;

namespace Enemy.Movement
{
    public class Mover : MonoBehaviour
    {

        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float distanceToPlayer = 1f;
        [SerializeField] float maxChangeStateDuration = 5f;
        [SerializeField] float minChangeStateDuration = 3f;
        float changeStateDuration = Mathf.Infinity;
        bool isMoveState = false;
        public bool IsMoveState { get => isMoveState; set => isMoveState = value; }
        Rigidbody2D rb;
        private void Awake()
        {
            TryGetComponent(out rb);
        }

        private void FixedUpdate()
        {
            changeStateDuration -= Time.fixedDeltaTime;
            MoveToPlayer();
        }

        private void MoveTo(Vector2 target)
        {
            Vector2 moveDirection = (target - (Vector2)transform.position).normalized;
            rb.AddForce(moveDirection * moveSpeed);
        }

        private void MoveToPlayer()
        {
            if (PlayerController.instance == null) return;

            if (!IsMoveState) return;
            if (IsPlayerInRange()) return;
            MoveTo(PlayerController.instance.transform.position);
        }

        public bool IsPlayerInRange()
        {
            return Vector2.Distance(transform.position, PlayerController.instance.transform.position) < distanceToPlayer;
        }

        public void SetChangeStateDuration()
        {
            changeStateDuration = Random.Range(minChangeStateDuration, maxChangeStateDuration);
        }

        public bool IsChangeStateDurationOver()
        {
            return changeStateDuration <= 0;
        }
    }
}
