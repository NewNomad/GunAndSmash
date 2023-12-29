using System.Collections;
using UnityEngine;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;
namespace Game.Core
{
    public class GlitchManager : MonoBehaviour
    {
        public static GlitchManager instance { get; private set; }
        [SerializeField] AnalogGlitchVolume analogGlitchFeature;
        [SerializeField] DigitalGlitchVolume digitalGlitchFeature;

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
            analogGlitchFeature.active = true;
            digitalGlitchFeature.active = true;
            yield return new WaitForSeconds(time);
            analogGlitchFeature.active = false;
            digitalGlitchFeature.active = false;
        }
    }
}
