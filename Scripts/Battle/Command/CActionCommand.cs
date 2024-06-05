using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionCommand : MonoBehaviour
{
    private Animator animator = null;
    private Transform trans = null;

    public CBattleManager btlManager = null;

    public GameObject leftMstName = null;
    public GameObject rightMstName = null;

    public GameObject FingerCursor = null;
    private RectTransform fingerTrans = null;

    public CFadeOut fadeOut = null;

    void Start()
    {
        trans = gameObject.GetComponent<Transform>();

        fingerTrans = FingerCursor.GetComponent<RectTransform>();
        animator = gameObject.GetComponent<Animator>();

        fingerTrans.position = new Vector2(783f, 654f);
        btlManager.curAttack = 0;

        StartCoroutine(ShowMenu());
    }

    void Update()
    {
        SetStatus(btlManager.status);

        if (btlManager.curTurn == 0)
        {
            // status 1
            ShowCommandMenu();
            ShowMstName();
            GetInput();

            // status 2 & 400
            ShowCursour();
            MovingCursor();
            PressedAttack();

            // Init status
            TurnBack();
        }
        // Toad Assist
        if(btlManager.status == 10)
        {
            HideMonsterCursor();
        }
        // 6: Win, 7: Loose, 8: Run
        if (btlManager.status == 6 || btlManager.status == 7 || btlManager.status == 8)
        {
            EndBattle();
        }

        SetAnim(btlManager.status);
    }

    IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(2.5f);

        SetStatus(1);
    }

    void ShowCommandMenu()
    {
        if(btlManager.status == 1)
        {
            if(btlManager.curPlrTurn == 0)
            {
                trans.position = new Vector3(611f, 419f, 0f);
            }
            else
            {
                trans.position = new Vector3(854f, 327f, 0f);
            }
        }
    }

    void GetInput()
    {
        if (animator.GetInteger("state") == 1 && Input.GetKeyDown(KeyCode.K)) // Action
        {
            SetStatus(2);
            FingerCursor.SetActive(true);
            rightMstName.SetActive(false);
            btlManager.curAttack = 0; // Default Monster Init is 0(left)
        }
        else if (animator.GetInteger("state") == 1 && Input.GetKeyDown(KeyCode.M)) // ETC
        {
            SetStatus(3);
        }
        else if (animator.GetInteger("state") == 1 && Input.GetKeyDown(KeyCode.J)) // Special
        {
            SetStatus(4);
        }
        else if (animator.GetInteger("state") == 1 && Input.GetKeyDown(KeyCode.I)) // Items
        {
            SetStatus(5);
        }
    }

    void SetStatus(int _s)
    {
        btlManager.status = _s;
    }

    void SetAnim(int _s)
    {
        animator.SetInteger("state", _s);
    }

    /****** Active When Status is 1 (Default) ******/
    void ShowMstName()
    {
        if(btlManager.status == 1)
        {
            if(btlManager.isLeftMstDead)
            {
                leftMstName.SetActive(false);
            }
            else
            {
                leftMstName.SetActive(true);
            }

            if(btlManager.isRightMstDead)
            {
                rightMstName.SetActive(false);
            }
            else
            {
                rightMstName.SetActive(true);
            }
        }
    }

    /****** Active When Status is 2 & 300 (Attack, MagicAttack) ******/
    void ShowCursour()
    {
        if(btlManager.status == 2 || btlManager.status >= 300)
        {
            FingerCursor.SetActive(true);
        }
    }


    void MovingCursor()
    {
        // if Dead Someone
        if ((btlManager.status == 2 || btlManager.status >= 400) && btlManager.isRightMstDead && !btlManager.isLeftMstDead)
        {
            fingerTrans.position = new Vector2(783f, 654f);
            btlManager.curAttack = 0;
            leftMstName.SetActive(true);
            rightMstName.SetActive(false);
        }
        else if((btlManager.status == 2 || btlManager.status >= 400) && btlManager.isLeftMstDead && !btlManager.isRightMstDead)
        {
            fingerTrans.position = new Vector2(1287f, 604f);
            btlManager.curAttack = 1;
            leftMstName.SetActive(false);
            rightMstName.SetActive(true);
        }
        else if ((btlManager.status == 2 || btlManager.status >= 400) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            btlManager.curAttack = 1;
            fingerTrans.position = new Vector2(1287f, 604f);
            leftMstName.SetActive(false);
            rightMstName.SetActive(true);
        }
        else if ((btlManager.status == 2 || btlManager.status >= 400) && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            btlManager.curAttack = 0;
            fingerTrans.position = new Vector2(783f, 654f);
            leftMstName.SetActive(true);
            rightMstName.SetActive(false);
        }
    }

    void PressedAttack()
    {
        if (btlManager.status == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            HideMonsterCursor();

            // ex) attack is 20 + attack Num (0 is default attack)
            btlManager.status = btlManager.status * 10 + GameManager.instance.members[btlManager.curPlrTurn].m_curAttak;
        }
        else if(btlManager.status >= 400 && Input.GetKeyDown(KeyCode.Space))
        {
            HideMonsterCursor();

            // ex) MagicAttack was set int Special Menu, 400 + magic attack Num.
            // so make it  40 + magic attack Num
            // for make Choosing Monster Status
            btlManager.status = 40 + btlManager.status % 10;
        }
    }

    void HideMonsterCursor()
    {
        FingerCursor.SetActive(false);
        fingerTrans.position = new Vector2(783f, 654f); // init to left pos
        leftMstName.SetActive(false);
        rightMstName.SetActive(false);
    }

    // Reset Everyting to status 1 When Select Menu
    void TurnBack()
    {
        if((btlManager.status == 2 || btlManager.status == 3 || btlManager.status == 4 || btlManager.status == 5) && Input.GetKeyDown(KeyCode.Escape))
        {
            btlManager.status = 1;
            FingerCursor.SetActive(false);
            rightMstName.SetActive(true);
            leftMstName.SetActive(true);
            SetStatus(1);
        }
    }

    // Press K to Back to Road
    void EndBattle()
    {
        // status 7 is Loose, 8 is Run
        if(btlManager.status == 7)
        {
            fadeOut.SetFadeOut(false, btlManager.status, 4.0f);
        }
        else if(btlManager.status == 8)
        {
            fadeOut.SetFadeOut(false, btlManager.status, 1.0f);
        }
        else  if (Input.GetKeyDown(KeyCode.K))
        {
            bool isLevelUp = false;
            for (int i = 0; i < (int)GameManager.instance.memberIndex; i++)
            {
                if (GameManager.instance.members[i].m_leftExp <= GameManager.instance.members[i].m_exp)
                {
                    isLevelUp = true;
                    break;
                }
            }

            fadeOut.SetFadeOut(isLevelUp, btlManager.status, 0.5f);
        }
    }



}
