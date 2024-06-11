using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStoreMenu : MonoBehaviour
{
    public CMarioMove marioMove = null;

    public Animator infoAnim = null;
    public Animator shopAnim = null;
    public Animator itemAnim = null;
    public Animator equipAnim = null;

    public GameObject categoryCursorL = null;
    public GameObject categoryCursorR = null;

    void Update()
    {
        ActiveMenu();
    }

    void ActiveMenu()
    {
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.up, out RaycastHit hit, 0.5f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                ShowMenuAnim();
                CShopManager.shopManager.status = 1;
            }
        }
        else
        {
            HideMenuAnim();
            CShopManager.shopManager.status = 0;
        }
    }

    void ShowMenuAnim()
    {
        infoAnim.SetBool("state", true);
        shopAnim.SetBool("state", true);
        itemAnim.SetBool("state", true);

        if(CShopManager.shopManager.page == 1)
        {
            equipAnim.SetBool("state", true);
            categoryCursorR.SetActive(true);
            categoryCursorL.SetActive(false);
        }
        else
        {
            equipAnim.SetBool("state", false);
            categoryCursorR.SetActive(false);
            categoryCursorL.SetActive(true);
        }
    }

    void HideMenuAnim()
    {
        infoAnim.SetBool("state", false);
        shopAnim.SetBool("state", false);
        itemAnim.SetBool("state", false);
        equipAnim.SetBool("state", false);

        CShopManager.shopManager.page = 0;
    }
}
