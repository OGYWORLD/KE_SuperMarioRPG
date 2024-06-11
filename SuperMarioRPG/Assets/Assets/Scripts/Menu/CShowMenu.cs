using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShowMenu : MonoBehaviour
{
    public Animator menuCanvas = null;

    public CMarioMove marioMove = null;

    // Audio
    private AudioSource menuAudio = null;
    public AudioClip openMenu = null;
    public AudioClip closedMenu = null;

    void Start()
    {
        menuAudio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.Minus))
        {
            menuAudio.clip = openMenu;
            menuAudio.Play();

            menuCanvas.SetBool("TopMenuStatus", true);
            GameManager.instance.isMenu = true;
            MenuManager.menu.page = 0;
            Time.timeScale = 0f;
        }

        if(Input.GetKeyDown(KeyCode.M) && !MenuManager.menu.isEquip && GameManager.instance.isMenu)
        {
            menuAudio.clip = closedMenu;
            menuAudio.Play();

            menuCanvas.SetBool("TopMenuStatus", false);
            GameManager.instance.isMenu = false;
            MenuManager.menu.page = -1;
            Time.timeScale = 1f;

        }
    }
}
