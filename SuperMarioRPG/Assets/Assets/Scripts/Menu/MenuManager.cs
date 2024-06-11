using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager menu;

    // -1: not open, 0: member, 1: equip, 2: item
    public int page { get; set; } = -1;

    // use in equip menu
    public EMEMBER curInfo { get; set; } = EMEMBER.MARIO;

    // 0: equip, 1: take
    public int equipPage { get; set; } = 0;

    // 0: weapon, 1: clothes
    public int whichEquip { get; set; } = 0;
    // Check player setting in equip menu
    public bool isEquip { get; set; } = false;

    void Awake()
    {
        // Checking Instance Num for SINGLETON
        if (menu == null)
        {
            menu = this;
        }
        else // !Warning: Two MenuManager Exist
        {
            Destroy(gameObject);
        }
    }
}
