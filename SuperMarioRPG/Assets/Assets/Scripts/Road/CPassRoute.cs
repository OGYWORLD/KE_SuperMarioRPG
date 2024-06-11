using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CPassRoute : MonoBehaviour
{
    public Image fadeImage = null;

    public SceneNames names = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Scene curScene = SceneManager.GetActiveScene();

            // In Store, contain scene name m01out
            if(curScene.name != "m01i1f1")
            {
                GameManager.instance.beforeSceneName = curScene.name;
            }
            else
            {
                GameManager.instance.beforeSceneName = "m01out";
            }

            StartCoroutine(Fadeout());
        }
    }

    IEnumerator Fadeout()
    {
        float sumTime = 0f;
        float totalTime = 0.5f;

        while (sumTime <= totalTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0, 1, sumTime / totalTime);
            fadeImage.color = color;

            sumTime += Time.deltaTime;

            yield return null;
        }

        SceneManager.LoadScene(names.name);

        yield break;
    }
}
