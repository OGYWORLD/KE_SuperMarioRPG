using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLeftMenu : MonoBehaviour
{
    public Text[] listTxt = new Text[3];

    public GameObject[] cursor = new GameObject[3];

    public GameObject[] menuPage = new GameObject[3];

    // Audio
    private AudioSource leftMenuAudio = null;

    void Start()
    {
        leftMenuAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SetLeftList();
        GetInput();
    }

    void SetLeftList()
    {
        if (MenuManager.menu.page == 0) // member page
        {
            SetMenuPage(0);
        }
        else if (MenuManager.menu.page == 1) // equip page
        {
            SetMenuPage(1);
        }
        else if (MenuManager.menu.page == 2) // item page
        {
            SetMenuPage(2);
        }
        else // closed menu
        {
            ClosedMenu();
        }
    }

    void SetMenuPage(int _p)
    {
        for(int i = 0; i < listTxt.Length; i++)
        {
            if(i == _p)
            {
                menuPage[i].SetActive(true);
                cursor[i].SetActive(true);
                listTxt[i].color = Color.white;
            }
            else
            {
                menuPage[i].SetActive(false);
                cursor[i].SetActive(false);
                listTxt[i].color = new Color(147f/255f, 91f/255f, 21f/255f);
            }
        }
    }

    void GetInput()
    {
        if(MenuManager.menu.page != -1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !MenuManager.menu.isEquip)
            {
                leftMenuAudio.Play();
                MenuManager.menu.page--;
                if (MenuManager.menu.page < 0)
                {
                    MenuManager.menu.page = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && !MenuManager.menu.isEquip)
            {
                leftMenuAudio.Play();
                MenuManager.menu.page++;
                if (MenuManager.menu.page > 2)
                {
                    MenuManager.menu.page = 2;
                }
            }
        }
    }

    void ClosedMenu()
    {
        for (int i = 0; i < listTxt.Length; i++)
        {
            menuPage[i].SetActive(false);
        }
    }
}
