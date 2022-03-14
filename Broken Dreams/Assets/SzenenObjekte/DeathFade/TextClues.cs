using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextClues : MonoBehaviour
{
    public TMP_Text clue;

    public IEnumerator TextClue(string Hinweis)
    {
        // sets text, fades it in, waits, fades out
        clue.text = Hinweis;
        yield return StartCoroutine(FadeInText(1f, clue));
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(FadeOutText(1f, clue));
        // set text back to be safe
        clue.text = "";
    }

    private IEnumerator FadeInText(float timeSpeed, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

    private IEnumerator FadeOutText(float timeSpeed, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
}