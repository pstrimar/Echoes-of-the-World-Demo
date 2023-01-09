using System.Collections;
using TMPro;
using UnityEngine;

public class BrailleToText : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    const float secondsPerChar = 0.05f;

    void Start()
    {
        StartCoroutine(Translate());
    }

    IEnumerator Translate()
    {
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
            yield return new WaitForSeconds(secondsPerChar);
        }
    }
}
