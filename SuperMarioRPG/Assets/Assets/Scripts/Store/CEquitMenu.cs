using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CEquitMenu : MonoBehaviour
{
    const int EQUIP_NUM = 4;

    // character icon anim
    public Animator marioIconAnim = null;
    public Animator mellowIconAnim = null;

    // character icon
    public GameObject marioIcon = null;
    public GameObject mellowIcon = null;

    // text
    public Text[] marioText = new Text[4];
    public Text[] mellowText = new Text[4];


    void Update()
    {
        HideIcon(); // check current member idx
        ShowEquip(); // show equip sub menu

    }

    void ShowEquip()
    {
        // equip
        if(CShopManager.shopManager.page == 1)
        {
            // noraml Shirt
            if(CShopManager.shopManager.listIdx == 0)
            {
                IconSetting(0);

                PrintText(ECLOTHES.SHIRT);

            }
            else if (CShopManager.shopManager.listIdx == 1)// noraml Pants
            {
                IconSetting(1);

                PrintText(ECLOTHES.PANTS);

            }
        }
    }

    void IconSetting(int _idx)
    {
        if(_idx == 0)
        {
            marioIconAnim.SetBool("state", true);
            mellowIconAnim.SetBool("state", false);
        }
        else
        {
            marioIconAnim.SetBool("state", false);
            mellowIconAnim.SetBool("state", true);
        }
    }

    void HideIcon()
    {
        if(GameManager.instance.memberIndex == EMEMBER.MARIO)
        {
            mellowIcon.SetActive(false);
        }
        else
        {
            mellowIcon.SetActive(true);
        }
    }

    void PrintText(ECLOTHES c)
    {
        if(GameManager.instance.validItem[(int)c].m_whoCanUse == EMEMBER.MARIO)
        {
            for (int i = 0; i < EQUIP_NUM; i++)
            {
                marioText[i].text = "" + GameManager.instance.validItem[(int)c][i];
                mellowText[i].text = "-";
            }
        }
        else if (GameManager.instance.validItem[(int)c].m_whoCanUse == EMEMBER.MELLOW)
        {
            for (int i = 0; i < EQUIP_NUM; i++)
            {
                marioText[i].text = "-";
                mellowText[i].text = "" + GameManager.instance.validItem[(int)c][i];
            }
        }

        if(GameManager.instance.memberIndex == EMEMBER.MARIO)
        {
            for (int i = 0; i < EQUIP_NUM; i++)
            {
                mellowText[i].text = "";
            }
        }
    }
}
