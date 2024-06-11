using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CDoor : MonoBehaviour
{
    public Image fadeImage = null;

    private AudioSource doorAudio = null;

    void Start()
    {
        doorAudio = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorAudio.Play();

            gameObject.GetComponent<Animator>().SetTrigger("chara_hit");

            GameManager.instance.curMarioPos = new Vector3(-34f, 11.7f, 13.8f);

            StartCoroutine(InToShop());
        }
    }

    IEnumerator InToShop()
    {
        yield return new WaitForSeconds(0.5f);

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

        SceneManager.LoadScene("m01i1f1");

        yield break;
    }
}
