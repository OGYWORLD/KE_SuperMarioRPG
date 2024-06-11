using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTopInfo : MonoBehaviour
{
    public Text curStage = null;

    public Text curFP = null;   
    public Text maxFP = null;   

    public Text coinTex = null;   

    void Update()
    {
        PrintText();
    }

    void PrintText()
    {
        curStage.text = GameManager.instance.curStage;
        curFP.text = "" + GameManager.instance.curFP;
        maxFP.text = "" + GameManager.instance.maxFP;
        coinTex.text = "" + GameManager.instance.coin;
    }
}
