using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPrintInfoTxt : MonoBehaviour
{
    public Text curFP = null;
    public Text maxFP = null;

    public Text coin = null;

    void Update()
    {
        ShowTxt();
    }

    void ShowTxt()
    {
        curFP.text = "" + GameManager.instance.curFP;
        maxFP.text = "" + GameManager.instance.maxFP;

        coin.text = "" + GameManager.instance.coin;
    }
}
