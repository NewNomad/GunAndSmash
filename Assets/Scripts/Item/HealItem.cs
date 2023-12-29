using UnityEngine;
namespace Game.Item
{
    public class HealItem : MonoBehaviour, IItem
    {
        [SerializeField] int healAmount = 100;
        public void UseItem()
        {
            Debug.Log("HealItem used");
        }
        public int HealAmount { get => healAmount; }
    }
}
