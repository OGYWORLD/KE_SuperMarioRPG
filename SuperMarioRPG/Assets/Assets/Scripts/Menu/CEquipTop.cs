using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CEquipTop : MonoBehaviour
{
    public GameObject[] cursorIcon = new GameObject[2];
    public GameObject[] icon = new GameObject[2];
    // name, curHp, maxHp, level, curEXP, leftEXP
    public Text[] charaInfo = new Text[6];
    public Image hpBar = null;

    public GameObject[] YaR = new GameObject[4];
    public Text[] preNum = new Text[4];
    public Text[] postNum = new Text[4];

    // Update is called once per frame
    void Update()
    {
        SetTopIcon();
        SetIcon();
        SetTopInfo();
        SetStat();
    }

    void SetTopIcon()
    {
        if(GameManager.instance.memberIndex == EMEMBER.MARIO)
        {
            cursorIcon[(int)EMEMBER.MELLOW].SetActive(false);
        }
        else
        {
            cursorIcon[(int)EMEMBER.MELLOW].SetActive(true);
        }
    }

    void SetIcon()
    {
        if (MenuManager.menu.curInfo == EMEMBER.MARIO)
        {
            icon[(int)EMEMBER.MARIO].SetActive(true);
            icon[(int)EMEMBER.MELLOW].SetActive(false);
        }
        else if (MenuManager.menu.curInfo == EMEMBER.MELLOW)
        {
            icon[(int)EMEMBER.MARIO].SetActive(false);
            icon[(int)EMEMBER.MELLOW].SetActive(true);
        }
    }

    void SetTopInfo()
    {
        // name, curHp, maxHp, level, curEXP, leftEXP
        charaInfo[0].text = "" + GameManager.instance.members[(int)MenuManager.menu.curInfo].m_name;
        charaInfo[1].text = "" + GameManager.instance.members[(int)MenuManager.menu.curInfo].m_curhp;
        charaInfo[2].text = "" + GameManager.instance.members[(int)MenuManager.menu.curInfo].m_maxhp;
        charaInfo[3].text = "" + GameManager.instance.members[(int)MenuManager.menu.curInfo].m_level;
        charaInfo[4].text = "" + GameManager.instance.members[(int)MenuManager.menu.curInfo].m_exp;
        charaInfo[5].text = "" + (GameManager.instance.members[(int)MenuManager.menu.curInfo].m_leftExp - GameManager.instance.members[(int)MenuManager.menu.curInfo].m_exp);

        hpBar.fillAmount = (float)GameManager.instance.members[(int)MenuManager.menu.curInfo].m_curhp / (float)GameManager.instance.members[(int)MenuManager.menu.curInfo].m_maxhp;
    }

    void SetStat()
    {
        if (MenuManager.menu.equipPage == 0)
        {
            for (int i = 0; i < YaR.Length; i++)
            {
                YaR[i].SetActive(false);
                preNum[i].text = "";
                postNum[i].text = "" + GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat[i];

            }
        }
    }
}
