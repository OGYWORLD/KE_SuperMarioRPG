using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFadeIn : MonoBehaviour
{
    public Image fadeImage = null;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float sumTime = 0f;
        float fadeTime = 0.5f;

        while (sumTime <= fadeTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(1, 0, sumTime / fadeTime);
            fadeImage.color = color;

            sumTime += Time.deltaTime;

            yield return null;
        }

        yield break;
    }
}
