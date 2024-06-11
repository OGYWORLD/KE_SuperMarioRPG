using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CFadeOut : MonoBehaviour
{
    public Image fadeImage = null;
    public GameObject gameOver = null;

    public void SetFadeOut(bool isLevelUp, int _s, float _t)
    {
        gameObject.SetActive(true);

        if (_s == 7) // active when game over
        {
            gameOver.SetActive(true);
        }

        StartCoroutine(Fadeout(isLevelUp, _s, _t));
    }

    IEnumerator Fadeout(bool isLevelUp, int _s, float _t)
    {
        float sumTime = 0f;

        while (sumTime <= _t)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0, 1, sumTime / _t);
            fadeImage.color = color;

            sumTime += Time.deltaTime;

            yield return null;
        }

        GameManager.instance.isNowBattle = false;

        if (_s == 7) // Lose
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (isLevelUp)
        {
            // LevelUp Scene
            SceneManager.LoadScene("e47in");
        }
        else
        {
            GameManager.instance.isWin = false;
            SceneManager.LoadScene(GameManager.instance.beforeSceneName);
        }

        yield break;
    }
}
