using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COtherMenu : MonoBehaviour
{
    public CBattleManager btlManager = null;

    private Animator animator = null;
    private Transform trans = null;

    public GameObject cursor = null;
    private Animator cursorAnim = null;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        cursorAnim = cursor.GetComponent<Animator>();
        trans = cursor.GetComponent<Transform>();
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
        cursorAnim.SetBool("MembersCursorStatus", true); // show cursor
    }

    void InitMenu()
    {
        animator.SetInteger("state", 0);
        cursorAnim.SetBool("MembersCursorStatus", false);
    }

    void PressedButton()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            btlManager.status = 8;
        }
    }
}
