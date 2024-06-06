using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSelectBonus : MonoBehaviour
{
    public GameObject param01 = null;
    public GameObject param02 = null;

    public GameObject[] lightIcon = new GameObject[3];

    public GameObject cursor = null;
    private Transform cursorTrans = null;

    // Update is called once per frame
    void Update()
    {
        cursorTrans = cursor.GetComponent<Transform>();
        ShowText();

        if(CLevelUpManager.lu.status != 3)
        {
            Init();
        }
    }

    void ShowText()
    {
        if (CLevelUpManager.lu.whichSelect == 0) // Hammer
        {
            cursorTrans.position = new Vector3(583f, 605f, 0f);
            SetLightIcon(0);
            param01.SetActive(true);
            param02.SetActive(true);
        }
        else if (CLevelUpManager.lu.whichSelect == 1) // Mushroom
        {
            cursorTrans.position = new Vector3(849f, 605f, 0f);
            SetLightIcon(1);
            param01.SetActive(true);
            param02.SetActive(false);
        }
        else if (CLevelUpManager.lu.whichSelect == 2) // Flower
        {
            cursorTrans.position = new Vector3(1115f, 605f, 0f);
            SetLightIcon(2);
            param01.SetActive(true);
            param02.SetActive(true);
        }
    }

    void SetLightIcon(int _i)
    {
        for(int i = 0; i < lightIcon.Length; i++)
        {
            lightIcon[i].SetActive(false);
        }

        lightIcon[_i].SetActive(true);
    }

    void Init()
    {
        cursorTrans.position = new Vector3(583f, 605f, 0f);
        SetLightIcon(0);
        param01.SetActive(true);
        param02.SetActive(true);
    }
}
