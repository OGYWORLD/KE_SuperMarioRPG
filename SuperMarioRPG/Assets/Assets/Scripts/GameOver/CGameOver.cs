using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameOver : MonoBehaviour
{
    public GameObject gameOver = null;

    private AudioSource pressedAudio = null;

    void Start()
    {
        pressedAudio = gameObject.GetComponent<AudioSource>();

        StartCoroutine(ShowUI());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.instance.isLoose = false;
            pressedAudio.Play();
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
