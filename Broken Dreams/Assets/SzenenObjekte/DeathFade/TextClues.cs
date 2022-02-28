using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextClues : MonoBehaviour
{
    public TMP_Text clue;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TextClue(string Hinweis)
    {
        clue.text = Hinweis;
        yield return StartCoroutine(FadeInText(1f, clue));
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(FadeOutText(1f, clue));
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