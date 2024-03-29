using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float flashTime = 0.1f;
    Material defaultMaterial;
    Color defaultColor;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // TryGetComponent(out material);
        TryGetComponent(out spriteRenderer);
        defaultMaterial = spriteRenderer.material;
        defaultColor = spriteRenderer.color;
    }

    public void GetFlashTime()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = Color.white;
        spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.color = defaultColor;
        spriteRenderer.material = defaultMaterial;
    }

    public void GetEnemyFlashTime(float hpPercentage)
    {
        Color color = ColorOnPercent(hpPercentage);
        StartCoroutine(EnemyFlashCoroutine(color));
    }

    private IEnumerator EnemyFlashCoroutine(Color color)
    {
        spriteRenderer.color = Color.white;
        spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.color = color;
        spriteRenderer.material = defaultMaterial;
    }

    private Color ColorOnPercent(float percentage)
    {
        float r = defaultColor.r + ((1f - percentage) * (Color.white.b - defaultColor.r));
        float g = defaultColor.g + ((1f - percentage) * (Color.white.b - defaultColor.g));
        float b = defaultColor.b + ((1f - percentage) * (Color.white.b - defaultColor.b));
        return new Color(r, g, b);
    }

    public void ChangeColorOnPercent(float percentage)
    {
        spriteRenderer.color = ColorOnPercent(percentage);
    }
}
