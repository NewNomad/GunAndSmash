using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameStartEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float countDownDuration = 1.0f; // 各カウントの持続時間
    [SerializeField] private float typeWriterDuration = 2.0f;
    [SerializeField] private float fadeOutDuration = 1.0f;
    [SerializeField] private Vector3 scaleUp = new Vector3(1.5f, 1.5f, 1.5f); // スケールアップの大きさ

    void Start()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        for (int i = 3; i > 0; i--)
        {
            textMeshPro.text = i.ToString();
            textMeshPro.transform.localScale = Vector3.zero;
            textMeshPro.DOFade(1, 0);
            textMeshPro.transform.DOScale(scaleUp, countDownDuration).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(countDownDuration);
            textMeshPro.DOFade(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }

        StartCoroutine(ShowGameStartText());
    }

    IEnumerator ShowGameStartText()
    {
        // タイプライター効果の初期化
        textMeshPro.maxVisibleCharacters = 0;

        // グリッチ効果を適用するシーケンス
        Sequence glitchSequence = DOTween.Sequence();
        for (int i = 0; i < 10; i++)
        {
            glitchSequence.Append(textMeshPro.DOFade(Random.Range(0f, 1f), 0.1f));
        }

        // タイプライター効果とグリッチ効果の同時開始
        DOTween.To(() => textMeshPro.maxVisibleCharacters, x => textMeshPro.maxVisibleCharacters = x, textMeshPro.text.Length, typeWriterDuration);
        yield return glitchSequence.Play().WaitForCompletion();

        // 徐々にフェードアウト
        textMeshPro.DOFade(0f, fadeOutDuration);
    }
}
