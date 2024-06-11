using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CbtlBGM : MonoBehaviour
{
    public CBattleManager btlManager = null;
    private AudioSource btlBGM = null;

    public GameObject winBGM = null;

    void Start()
    {
        btlBGM = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(btlManager.status == 6)
        {
            btlBGM.Stop();
            winBGM.SetActive(true);
        }
        else if (btlManager.status == 7)
        {
            StartCoroutine(Loose());
        }
    }

    IEnumerator Loose()
    {
        float sumTime = 0f;

        while(sumTime > 0f)
        {
            btlBGM.pitch -= sumTime / 1f;

            sumTime += Time.deltaTime;

            yield return null;
        }

        yield break;
    }
}
