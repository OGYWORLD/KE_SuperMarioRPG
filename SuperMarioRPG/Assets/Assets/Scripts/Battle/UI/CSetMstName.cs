using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSetMstName : MonoBehaviour
{
    public Text mstTex = null;
    private Dictionary<string, string> mstName;
    void Start()
    {
        mstName = new Dictionary<string, string>();
        mstName["goomba"] = "굼바";
        mstName["nokonoko"] = "펄럭펄럭";
        mstName["spikey"] = "가시병";
        mstName["hidog"] = "하이도그";
        mstName["shyguy"] = "헤이호";
        mstName["hammerBro"] = "해머 브라더스";

        mstTex.text = mstName[GameManager.instance.btlMoster];
    }
}
