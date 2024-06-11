using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGetInput : MonoBehaviour
{
    // Cursor
    public Animator cursorTopAnim = null;
    public Animator cursorBottomAnim = null;

    private Dictionary<int, int> Cost = new Dictionary<int, int>();

    // Audio
    private AudioSource storeAudio = null;

    public AudioClip pass = null;
    public AudioClip pressed = null;
    public AudioClip click = null;

    void Start()
    {
        storeAudio = gameObject.GetComponent<AudioSource>();

        // set cost
        Cost[0] = 4; // mushroom;
        Cost[1] = 10; // Honey Shyrup
        Cost[2] = 7; // normal shirt
        Cost[3] = 7; // normal pants

        cursorTopAnim.SetBool("MembersCursorStatus", true);
    }

    void Update()
    {
        if(!GameManager.instance.isMenu)
        {
            MoveCursor();
            SwitchCategory();

            BuyItem();
        }

        if (CShopManager.shopManager.status == 0)
        {
            CursorInit();
        }
    }

    void MoveCursor()
    {
        // Down
        if(Input.GetKeyDown(KeyCode.M))
        {
            storeAudio.clip = pass;
            storeAudio.Play();

            cursorTopAnim.SetBool("MembersCursorStatus", false);
            cursorBottomAnim.SetBool("MembersCursorStatus", true);

            CShopManager.shopManager.listIdx = 1;
        }

        // Up
        if (Input.GetKeyDown(KeyCode.I))
        {
            storeAudio.clip = pass;
            storeAudio.Play();

            cursorTopAnim.SetBool("MembersCursorStatus", true);
            cursorBottomAnim.SetBool("MembersCursorStatus", false);

            CShopManager.shopManager.listIdx = 0;
        }
    }


    // category switch
    void SwitchCategory()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            storeAudio.clip = click;
            storeAudio.Play();

            CShopManager.shopManager.page = 0;
            CursorInit();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            storeAudio.clip = click;
            storeAudio.Play();

            CShopManager.shopManager.page = 1;
            CursorInit();
        }
    }

    void CursorInit()
    {
        cursorTopAnim.SetBool("MembersCursorStatus", false);
        cursorBottomAnim.SetBool("MembersCursorStatus", false);

        CShopManager.shopManager.listIdx = 0;

        cursorTopAnim.SetBool("MembersCursorStatus", true);
    }

    void BuyItem()
    {
        int curCoin = GameManager.instance.coin;

        if (Input.GetKeyDown(KeyCode.K))
        {
            storeAudio.clip = pressed;
            storeAudio.Play();

            if (CShopManager.shopManager.page == 0) // item
            {
                if(curCoin >= Cost[CShopManager.shopManager.listIdx])
                {
                    GameManager.instance.coin -= Cost[CShopManager.shopManager.listIdx];
                    GameManager.instance.m_items[CShopManager.shopManager.listIdx] += 1;
                }
            }
            else if (CShopManager.shopManager.page == 1) // equip
            {
                if (curCoin >= Cost[CShopManager.shopManager.listIdx + 2])
                {
                    GameManager.instance.coin -= Cost[CShopManager.shopManager.listIdx + 2];
                    GameManager.instance.validItem[(CShopManager.shopManager.listIdx + 1)].m_num += 1;
                }
            }
        }
    }
}
