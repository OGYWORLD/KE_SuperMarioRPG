using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COtherMenu : MonoBehaviour
{
    public CBattleManager btlManager = null;

    private Animator animator = null;

    public GameObject cursor = null;
    private Animator cursorAnim = null;

    private AudioSource otherMenuAudio = null;

    void Start()
    {
        otherMenuAudio = gameObject.GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
        cursorAnim = cursor.GetComponent<Animator>();
    }

    void Update()
    {
        if (btlManager.status == 3)
        {
            ShowOtherMenu();
            PressedButton();
        }
        else
        {
            InitMenu();
        }
    }

    void ShowOtherMenu()
    {
        animator.SetInteger("state", btlManager.status); // show Other Menu

        // Can't Run in Boss Battle
        if(!GameManager.instance.isBossBtl)
        {
            cursorAnim.SetBool("MembersCursorStatus", true); // show cursor
        }
    }

    void InitMenu()
    {
        animator.SetInteger("state", 0);
        cursorAnim.SetBool("MembersCursorStatus", false);
    }

    void PressedButton()
    {
        if(Input.GetKeyDown(KeyCode.K) && !GameManager.instance.isBossBtl)
        {
            otherMenuAudio.Play();

            btlManager.status = 8;
        }
    }
}
