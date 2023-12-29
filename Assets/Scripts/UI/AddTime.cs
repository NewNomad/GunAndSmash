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

        [SerializeField] TextMeshProUGUI flickerText;

        private void Start()
        {
            StartCoroutine(FlickerAndScrollTextRoutine());
        }

        private IEnumerator FlickerAndScrollTextRoutine()
        {
            Vector3 initialPosition = flickerText.transform.position;
            Vector3 targetPosition = initialPosition + scrollOffset;

            flickerText.transform.DOMove(targetPosition, scrollDuration).SetEase(Ease.OutQuad);

            Sequence sequence = DOTween.Sequence();

            for (int i = 0; i < flickerCount; i++)
            {
                float randomDuration = Random.Range(0.01f, maxFlickerDuration);
                sequence.Append(flickerText.DOFade(Random.Range(0f, 1f), randomDuration));
            }

            sequence.Append(flickerText.DOFade(1f, maxFlickerDuration));
            sequence.Append(flickerText.DOFade(0f, fadeOutDuration));

            yield return sequence.WaitForCompletion();
        }

    }
}
