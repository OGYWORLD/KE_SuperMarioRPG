using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLUPlayer : MonoBehaviour
{
    public CPlayerValue playerValues = null;

    private Animator animator = null;

    public bool isSrt { get; set; } = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(!isSrt)
        {
            isSrt = true;
            StartCoroutine(DanceMotion());
        }
    }

    void SetLUAnim()
    {
        animator.SetBool("isCB", false);
        animator.SetBool("isLU", true);
    }

    void SetCBAnim()
    {
        animator.SetBool("isLU", false);
        animator.SetBool("isCB", true);
    }

    IEnumerator DanceMotion()
    {
        if (CLevelUpManager.lu.curLevelUp != playerValues.Me)
        {
            animator.SetBool("isLU", false);
        }

        // wait Light set
        yield return new WaitForSeconds(1.0f);

        if (CLevelUpManager.lu.curLevelUp == playerValues.Me)
        {
            SetLUAnim();

            yield return new WaitForSeconds(1.0f);
        }
        else
        {
            // wait LU Player's Thums Up
            yield return new WaitForSeconds(1.0f);
            SetCBAnim();
        }

        CLevelUpManager.lu.status = 1;
    }
}
