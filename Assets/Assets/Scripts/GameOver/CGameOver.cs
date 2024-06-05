using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameOver : MonoBehaviour
{
    public GameObject gameOver = null;

    void Start()
    {
        StartCoroutine(ShowUI());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(1.0f);

        gameOver.SetActive(true);

        yield break;
    }
}
