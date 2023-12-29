using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
namespace Game.UI
{
    public class AddTime : MonoBehaviour
    {
        [SerializeField] int flickerCount = 10; // 点滅の回数
        [SerializeField] float maxFlickerDuration = 0.2f; // 最大の点滅間隔
        [SerializeField] float scrollDuration = 2.0f; // スクロールの持続時間
        [SerializeField] float fadeOutDuration = 1.0f; // フェードアウトの持続時間
        [SerializeField] Vector3 scrollOffset = new Vector3(0, 50f, 0); // 上に移動する距離

        TextMeshProUGUI flickerText;

        private void Awake()
        {
            flickerText = GetComponent<TextMeshProUGUI>();
            flickerText.rectTransform.position = new Vector3(0, -200f, 0);

        }

        public void Initiate(float addTime)
        {
            flickerText.text = $"+{addTime:F1}s";
            StartCoroutine(FlickerAndScrollTextRoutine());
        }

        private IEnumerator FlickerAndScrollTextRoutine()
        {
            RectTransform rectTransform = flickerText.rectTransform;
            Vector2 initialPosition = rectTransform.anchoredPosition;
            Vector2 targetPosition = initialPosition + new Vector2(scrollOffset.x, scrollOffset.y);

            rectTransform.DOAnchorPos(targetPosition, scrollDuration).SetEase(Ease.OutQuad);

            Sequence sequence = DOTween.Sequence();

            for (int i = 0; i < flickerCount; i++)
            {
                float randomDuration = Random.Range(0.01f, maxFlickerDuration);
                sequence.Append(flickerText.DOFade(Random.Range(0f, 1f), randomDuration));
            }

            sequence.Append(flickerText.DOFade(1f, maxFlickerDuration));
            sequence.Append(flickerText.DOFade(0f, fadeOutDuration)).OnComplete(() => Destroy(gameObject));

            yield return sequence.WaitForCompletion();
        }

    }
}
