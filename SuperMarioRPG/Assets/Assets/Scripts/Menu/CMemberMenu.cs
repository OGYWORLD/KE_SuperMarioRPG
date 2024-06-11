using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMemberMenu : MonoBehaviour
{
    public Text[] levelTxt = new Text[2];
    public Text[] hpTxt = new Text[2];
    public Text[] maxHpTxt = new Text[2];

    public Image[] hpBar = new Image[2];

    public GameObject[] infoBox = new GameObject[2];

    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    void SetText()
    {
        if(MenuManager.menu.page != -1)
        {
            for (int i = 0; i <= (int)GameManager.instance.memberIndex; i++)
            {
                levelTxt[i].text = "" + GameManager.instance.members[i].m_level;
                hpTxt[i].text = "" + GameManager.instance.members[i].m_curhp;
                maxHpTxt[i].text = "" + GameManager.instance.members[i].m_maxhp;

                hpBar[i].fillAmount = (float)GameManager.instance.members[i].m_curhp / (float)GameManager.instance.members[i].m_maxhp;

                infoBox[i].SetActive(true);
            }
        }
    }
}
