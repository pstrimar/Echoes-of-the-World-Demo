using System.Collections;
using TMPro;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    TextMeshProUGUI text;
    const float textSpacingTime = 3f;
    const float maxCharSpacing = 20f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
        StartCoroutine(StretchText());
    }

    void OnDisable()
    {
        canvasGroup.alpha = 0;
        text.characterSpacing = 0;
    }

    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut());
    }

    IEnumerator StretchText()
    {
        while (text.characterSpacing < maxCharSpacing)
        {
            text.characterSpacing += Time.deltaTime * (maxCharSpacing / textSpacingTime);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        enabled = false;
    }
}
