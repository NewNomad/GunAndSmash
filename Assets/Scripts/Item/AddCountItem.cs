
using Game.Core;
using UnityEngine;
namespace Game.Item
{
    [RequireComponent(typeof(ItemDestroyer))]
    public class AddCountItem : MonoBehaviour, IItem
    {
        [SerializeField] int addCount = 30;
        public void UseItem()
        {
            CountDownTimer.instance.AddTime(addCount);
            GetComponent<ItemDestroyer>().DestroyItem();
        }
    }
}
