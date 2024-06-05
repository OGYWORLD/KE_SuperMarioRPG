using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPrintFP : MonoBehaviour
{
    public Text maxFp = null;
    public Text curFp = null;

    void Start()
    {
        maxFp.text = "" + GameManager.instance.maxFP;
        curFp.text = "" + GameManager.instance.curFP;
    }

    // Update is called once per frame
    void Update()
    {
        maxFp.text = "" + GameManager.instance.maxFP;
        curFp.text = "" + GameManager.instance.curFP;
    }
}
