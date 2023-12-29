using UnityEngine;
namespace Game.Core
{
    public class UseItemOnHit : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out IItem item)) { return; }
            item.UseItem();
        }
    }
}
