using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInfo : MonoBehaviour
{
    public CBattleManager manager = null;
    public GameObject FPInfo = null;
    public GameObject CharaStatus01 = null;
    public GameObject CharaStatus02 = null;

    public GameObject winAnnounce = null;
    public GameObject winChara01 = null;
    public GameObject winChara02 = null;

    private bool isEnd = false;

    void Update()
    {
        if(manager.status > 0 && manager.status != 6)
        {
            FPInfo.SetActive(true);
            if(GameManager.instance.memberIndex == 0)
            {
                CharaStatus01.SetActive(true);
            }
            else
            {
                CharaStatus01.SetActive(true);
                CharaStatus02.SetActive(true);
            }
        }
        else if(manager.status == 6)
        {
            if(!isEnd)
            {
                StartCoroutine(ShowEndInfo());
            }
        }
    }

    IEnumerator ShowEndInfo()
    {
        isEnd = true;

        yield return new WaitForSeconds(1.5f);

        CharaStatus01.SetActive(false);
        CharaStatus02.SetActive(false);
        FPInfo.SetActive(false);

        winAnnounce.SetActive(true);

        if (GameManager.instance.memberIndex == (int)EMEMBER.MARIO)
        {
            winChara01.SetActive(true);
        }
        else
        {
            winChara01.SetActive(true);
            winChara02.SetActive(true);
        }

    }
}
