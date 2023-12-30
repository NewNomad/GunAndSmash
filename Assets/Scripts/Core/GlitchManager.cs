using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;
namespace Game.Core
{
    public class GlitchManager : MonoBehaviour
    {
        public static GlitchManager instance { get; private set; }
        [SerializeField] Volume volume;
        AnalogGlitchVolume analogGlitch;
        DigitalGlitchVolume digitalGlitch;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            volume.profile.TryGet(out analogGlitch);
            volume.profile.TryGet(out digitalGlitch);
        }

        void Start()
        {
        }

        public void SetGlitch(float time)
        {
            StartCoroutine(SetGlitchCoroutine(time));
        }

        IEnumerator SetGlitchCoroutine(float time)
        {
            analogGlitch.active = true;
            digitalGlitch.active = true;
            yield return new WaitForSeconds(time);
            analogGlitch.active = false;
            digitalGlitch.active = false;
        }
    }
}
