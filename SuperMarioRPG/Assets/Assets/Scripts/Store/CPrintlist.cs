using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPrintlist : MonoBehaviour
{
    // name, buying, price, info
    public Text[] itemListUp = new Text[4];
    public Text[] itemListDown = new Text[4];


    void Start()
    {
        
    }

    void Update()
    {
        PrintText();
    }

    void PrintText()
    {
        // item
        if(CShopManager.shopManager.page == 0)
        {
            itemListUp[0].text = "버섯";
            itemListUp[1].text = "" + GameManager.instance.m_items[(int)EITEMS.MUSHROOM];
            itemListUp[2].text = "4";

            itemListDown[0].text = "허니시럽";
            itemListDown[1].text = "" + GameManager.instance.m_items[(int)EITEMS.HONEY_SYRUP];
            itemListDown[2].text = "10";

            if(CShopManager.shopManager.listIdx == 0)
            {
                itemListUp[3].text = "HP 30을 회복합니다.";
            }
            else
            {
                itemListDown[3].text = "FP 10을 회복합니다.";
            }
        }
        else // equip
        {
            itemListUp[0].text = "노말작업복";
            itemListUp[1].text = "" + GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_num;
            itemListUp[2].text = "7";

            itemListDown[0].text = "노말팬츠";
            itemListDown[1].text = "" + GameManager.instance.validItem[(int)ECLOTHES.PANTS].m_num;
            itemListDown[2].text = "7";

            if (CShopManager.shopManager.listIdx == 0)
            {
                itemListUp[3].text = "평범한 작업복입니다.";
            }
            else
            {
                itemListDown[3].text = "평범한 바지입니다.";
            }
        }
    }
}
