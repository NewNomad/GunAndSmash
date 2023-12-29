using Game.Control;
using Game.Core;
using UnityEngine;
namespace Game.Item
{
    [RequireComponent(typeof(ItemDestroyer))]
    public class HealItem : MonoBehaviour, IItem
    {
        [SerializeField] int healAmount = 100;
        public void UseItem()
        {
            PlayerController.instance.TryGetComponent(out Health health);
            health.Heal(healAmount);
            GetComponent<ItemDestroyer>().DestroyItem();
        }
    }
}
