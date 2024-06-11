using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLUInput : MonoBehaviour
{
    private AudioSource luAudio = null;

    public AudioClip pass = null;
    public AudioClip pressed = null;

    void Start()
    {
        luAudio = gameObject.GetComponent<AudioSource>();
    }

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
            luAudio.clip = pressed;
            luAudio.Play();

            CLevelUpManager.lu.status++;
        }
    }

    void SelectBonus()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            luAudio.clip = pass;
            luAudio.Play();

            CLevelUpManager.lu.whichSelect++;
            if(CLevelUpManager.lu.whichSelect >= 3)
            {
                CLevelUpManager.lu.whichSelect = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Mushroom
        {
            luAudio.clip = pass;
            luAudio.Play();

            CLevelUpManager.lu.whichSelect--;
            if (CLevelUpManager.lu.whichSelect <= 0)
            {
                CLevelUpManager.lu.whichSelect = 0;
            }
        }
    }
}
