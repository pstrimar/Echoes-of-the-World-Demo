using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrailleToText : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public ContentSizeFitter[] contentSizeFitters;

    const float secondsPerChar = 0.05f;

    void OnEnable()
    {        
        StopAllCoroutines();
        StartCoroutine(Translate());
    }

    IEnumerator Translate()
    {
        foreach (var contentSizeFitter in contentSizeFitters)
        {
            contentSizeFitter.enabled = true;
        }
        LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
        yield return null;
        foreach (var contentSizeFitter in contentSizeFitters)
        {
            contentSizeFitter.enabled = false;
        }
        string newText;
        int charCount = messageText.text.Length;
        for (int i = 0; i < charCount + 1; i++)
        {
            newText = messageText.text;
            if (i > 0)
            {
                newText = newText.Remove(i - 1, 20);
            }
            newText = newText.Insert(i, "<font=\"BRAILLE SDF\">");
            messageText.text = newText;
            yield return new WaitForSeconds(3.2f / charCount); // lines up with braille reading animation
        }
        newText = messageText.text.Replace("<font=\"BRAILLE SDF\">", "");
        messageText.text = newText;
    }
}
