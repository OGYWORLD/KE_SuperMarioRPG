using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEquipInput : MonoBehaviour
{
    public GameObject cursor01 = null;
    public GameObject cursor02 = null;

    public GameObject topCursor01 = null;
    public GameObject topCursor02 = null;

    public GameObject euqipMenu = null;
    public GameObject euqipSetMenu = null;

    public CEquipSet set = null;

    // Audio
    private AudioSource equipMenuAudio = null;
    public AudioClip passButton = null;
    public AudioClip click = null;
    public AudioClip pressed = null;

    void Start()
    {
        equipMenuAudio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        // Equip Menu in / out
        if(Input.GetKeyDown(KeyCode.RightArrow) && MenuManager.menu.page == 1 && !MenuManager.menu.isEquip)
        {
            equipMenuAudio.clip = passButton;
            equipMenuAudio.Play();

            MenuManager.menu.isEquip = true;
            cursor01.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.M) && MenuManager.menu.equipPage == 0)
        {
            equipMenuAudio.clip = passButton;
            equipMenuAudio.Play();

            MenuManager.menu.isEquip = false;
            cursor01.SetActive(false);
            cursor02.SetActive(false);
        }

        // Change Character
        if (Input.GetKeyDown(KeyCode.R) && GameManager.instance.memberIndex == EMEMBER.MELLOW && MenuManager.menu.isEquip && MenuManager.menu.equipPage == 0)
        {
            equipMenuAudio.clip = click;
            equipMenuAudio.Play();

            MenuManager.menu.curInfo = EMEMBER.MELLOW;

            set.totalListNum = 0;
            set.cursorPos = 0;

            cursor01.SetActive(true);
            cursor02.SetActive(false);

            topCursor01.SetActive(false);
            topCursor02.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.L) && GameManager.instance.memberIndex == EMEMBER.MELLOW && MenuManager.menu.isEquip && MenuManager.menu.equipPage == 0)
        {
            equipMenuAudio.clip = click;
            equipMenuAudio.Play();

            MenuManager.menu.curInfo = EMEMBER.MARIO;

            set.totalListNum = 0;
            set.cursorPos = 0;

            cursor01.SetActive(true);
            cursor02.SetActive(false);

            topCursor01.SetActive(true);
            topCursor02.SetActive(false);
        }

        // Weapon or Clothes
        if(Input.GetKeyDown(KeyCode.UpArrow) && MenuManager.menu.isEquip && MenuManager.menu.equipPage == 0)
        {
            equipMenuAudio.clip = passButton;
            equipMenuAudio.Play();

            MenuManager.menu.whichEquip = 0;
            cursor01.SetActive(true);
            cursor02.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && MenuManager.menu.isEquip && MenuManager.menu.equipPage == 0)
        {
            equipMenuAudio.clip = passButton;
            equipMenuAudio.Play();

            MenuManager.menu.whichEquip = 1;
            cursor01.SetActive(false);
            cursor02.SetActive(true);
        }

        // In Equip setting Menu
        if(Input.GetKeyDown(KeyCode.K) && MenuManager.menu.page == 1 && MenuManager.menu.isEquip && MenuManager.menu.equipPage == 0)
        {
            equipMenuAudio.clip = pressed;
            equipMenuAudio.Play();

            MenuManager.menu.equipPage = 1;
            euqipMenu.SetActive(false);
            euqipSetMenu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.M) && MenuManager.menu.page == 1 && MenuManager.menu.isEquip && MenuManager.menu.equipPage == 1)
        {
            equipMenuAudio.clip = pressed;
            equipMenuAudio.Play();

            MenuManager.menu.equipPage = 0;
            euqipMenu.SetActive(true);
            euqipSetMenu.SetActive(false);
        }
    }
}
