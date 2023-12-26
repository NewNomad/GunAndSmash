using UnityEngine;
namespace Enemy.Combat
{
    public class Charged : MonoBehaviour, ICharged
    {
        Rigidbody2D rb;
        private void Awake()
        {
            TryGetComponent(out rb);
        }

        public void OnCharged(Vector2 direction)
        {
        }
    }
}
