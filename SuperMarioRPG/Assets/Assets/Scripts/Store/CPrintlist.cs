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
            itemListUp[0].text = "����";
            itemListUp[1].text = "" + GameManager.instance.m_items[(int)EITEMS.MUSHROOM];
            itemListUp[2].text = "4";

            itemListDown[0].text = "��Ͻ÷�";
            itemListDown[1].text = "" + GameManager.instance.m_items[(int)EITEMS.HONEY_SYRUP];
            itemListDown[2].text = "10";

            if(CShopManager.shopManager.listIdx == 0)
            {
                itemListUp[3].text = "HP 30�� ȸ���մϴ�.";
            }
            else
            {
                itemListDown[3].text = "FP 10�� ȸ���մϴ�.";
            }
        }
        else // equip
        {
            itemListUp[0].text = "�븻�۾���";
            itemListUp[1].text = "" + GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_num;
            itemListUp[2].text = "7";

            itemListDown[0].text = "�븻����";
            itemListDown[1].text = "" + GameManager.instance.validItem[(int)ECLOTHES.PANTS].m_num;
            itemListDown[2].text = "7";

            if (CShopManager.shopManager.listIdx == 0)
            {
                itemListUp[3].text = "����� �۾����Դϴ�.";
            }
            else
            {
                itemListDown[3].text = "����� �����Դϴ�.";
            }
        }
    }
}
