using Game.Control;
using Game.Core;
using UnityEngine;
namespace Game.Item
{
    public class HealItem : MonoBehaviour, IItem
    {
        [SerializeField] int healAmount = 100;
        [SerializeField] ParticleSystem healEffect;
        public void UseItem()
        {
            PlayerController.instance.TryGetComponent(out Health health);
            health.Heal(healAmount);
            if (healEffect != null)
            {
                healEffect.transform.parent = null;
                healEffect.Stop();
                Destroy(healEffect.gameObject, healEffect.main.duration);
            }
            Destroy(gameObject);
        }
    }
}
