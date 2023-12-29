using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;

namespace Game.Item
{
    public class ItemDestroyer : MonoBehaviour
    {
        [SerializeField] ParticleSystem itemEffect;
        [SerializeField] ParticleSystem destroyEffect;

        public void DestroyItem()
        {
            if (itemEffect != null)
            {
                itemEffect.transform.parent = null;
                itemEffect.Stop();
                Destroy(itemEffect.gameObject, itemEffect.main.duration);
            }
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            SePlayer.Instance.Play("etfx_explosion_flash");
            Destroy(gameObject);
        }
    }
}
