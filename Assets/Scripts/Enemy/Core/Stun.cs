using System.Collections;
using UnityEngine;
namespace Enemy.Core
{
    public class Stun : MonoBehaviour
    {
        [SerializeField] float stunTime = 5f;
        [SerializeField] float stunBlinkTime = 0.2f;
        float currentStunTime = 0f;
        bool isStun = false;
        public bool IsStun { get => isStun; set => isStun = value; }
        SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            currentStunTime += Time.deltaTime;
        }

        public void StunEnemy()
        {
            isStun = true;
            currentStunTime = 0f;
            StartCoroutine(StunBlink());
        }

        // 点滅
        private IEnumerator StunBlink()
        {
            const float blinkColorValue = 0.2f;
            Color defaultColor = spriteRenderer.color;
            Color blinkColor = new Color(defaultColor.r * blinkColorValue, defaultColor.g * blinkColorValue, defaultColor.b * blinkColorValue);
            while (currentStunTime < stunTime)
            {
                float blinkTime = currentStunTime > stunTime / 2 ? stunBlinkTime / 2 : stunBlinkTime;
                spriteRenderer.color = blinkColor;
                yield return new WaitForSeconds(blinkTime);
                spriteRenderer.color = defaultColor;
                yield return new WaitForSeconds(blinkTime);
            }
            spriteRenderer.color = defaultColor;
            isStun = false;
            yield return null;
        }
    }
}
