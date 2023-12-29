
using Game.Core;
using UnityEngine;
namespace Game.Item
{
    public class AddCountItem : MonoBehaviour, IItem
    {
        [SerializeField] int addCount = 30;
        [SerializeField] ParticleSystem addCountEffect;
        public void UseItem()
        {
            CountDownTimer.instance.AddTime(addCount);

            if (addCountEffect != null)
            {
                addCountEffect.transform.parent = null;
                addCountEffect.Stop();
                Destroy(addCountEffect.gameObject, addCountEffect.main.duration);
            }
            Destroy(gameObject);
        }
    }
}
