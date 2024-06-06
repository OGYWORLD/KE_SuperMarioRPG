using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLUInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(CLevelUpManager.lu.status != 0)
        {
            GetInput();
        }
        if(CLevelUpManager.lu.status == 3)
        {
            SelectBonus();
        }
    }

    void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            CLevelUpManager.lu.status++;
        }
    }

    void SelectBonus()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CLevelUpManager.lu.whichSelect++;
            if(CLevelUpManager.lu.whichSelect >= 3)
            {
                CLevelUpManager.lu.whichSelect = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Mushroom
        {
            CLevelUpManager.lu.whichSelect--;
            if (CLevelUpManager.lu.whichSelect <= 0)
            {
                CLevelUpManager.lu.whichSelect = 0;
            }
        }
    }
}
