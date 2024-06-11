using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CWinInfo : MonoBehaviour
{
    public Text expText = null;
    public Text coinText = null;

    public Text chara01Text = null;
    public Text chara02Text = null;

    void Start()
    {
        expText.text = "" + (GameManager.instance.monStats[GameManager.instance.btlMoster].m_exp * 2);
        coinText.text = "" + (GameManager.instance.monStats[GameManager.instance.btlMoster].m_coin * 2);

        chara01Text.text = "" + (GameManager.instance.members[0].m_leftExp - GameManager.instance.members[0].m_exp);
        chara02Text.text = "" + (GameManager.instance.members[1].m_leftExp - GameManager.instance.members[1].m_exp);
    }

    void Update()
    {
        if(GameManager.instance.members[0].m_leftExp > GameManager.instance.members[0].m_exp)
        {
            chara01Text.text = "" + (GameManager.instance.members[0].m_leftExp - GameManager.instance.members[0].m_exp);
        }
        else
        {
            chara01Text.text = "0";
        }

        if (GameManager.instance.members[1].m_leftExp > GameManager.instance.members[1].m_exp)
        {
            chara02Text.text = "" + (GameManager.instance.members[1].m_leftExp - GameManager.instance.members[1].m_exp);
        }
        else
        {
            chara02Text.text = "0";
        }
    }
}
