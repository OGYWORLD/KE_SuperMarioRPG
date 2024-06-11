using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CmitemMenu : MonoBehaviour
{
    public Text[] itemName = new Text[2];
    public Text[] multiple = new Text[2];
    public Text[] itemCnt = new Text[2];

    public GameObject noItem = null;

    public Dictionary<int, string> itemNameList;
    
    void Start()
    {
        itemNameList = new Dictionary<int, string>();
        itemNameList[0] = "버섯";
        itemNameList[1] = "허니 시럽";
    }

    // Update is called once per frame
    void Update()
    {
        if(MenuManager.menu.page == 2)
        {
            PrintText();
        }
        else
        {
            Init();
        }
    }

    void PrintText()
    {
        int checkEmpty = 0;

        for(int i = 0; i < itemName.Length; i++)
        {
            if(GameManager.instance.m_items[i] != 0)
            {
                itemName[checkEmpty].text = itemNameList[i];
                multiple[checkEmpty].text = "x";
                itemCnt[checkEmpty].text = "" + GameManager.instance.m_items[i];

                checkEmpty++;
            }
        }

        if(checkEmpty == 0)
        {
            noItem.SetActive(true);
        }
        else
        {
            noItem.SetActive(false);
        }
    }

    void Init()
    {
        for (int i = 0; i < itemName.Length; i++)
        {
            itemName[i].text = "";
            multiple[i].text = "";
            itemCnt[i].text = "";
        }
    }
}
