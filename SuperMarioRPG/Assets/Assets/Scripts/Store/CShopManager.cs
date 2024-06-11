using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShopManager : MonoBehaviour
{
    public static CShopManager shopManager; // ShopManager

    // status 1: raycast On, status 2: raycast Off
    public int status { get; set; } = 0;

    // 0: item, 1: equip
    public int page { get; set; } = 0;

    // 0: top, 1: down
    public int listIdx { get; set; } = 0;

    void Awake()
    {
        // Checking Instance Num for SINGLETON
        if (shopManager == null)
        {
            shopManager = this;
        }
        else // !Warning: Two GameManager Exist
        {
            Destroy(gameObject);
        }
    }
}
