
using UnityEngine;
namespace Game.Item
{
    public class AddCountItem : MonoBehaviour, IItem
    {
        [SerializeField] int addCount = 1;
        public void UseItem()
        {
            Debug.Log("AddCountItem used");
        }
        public int AddCount { get => addCount; }
    }
}
