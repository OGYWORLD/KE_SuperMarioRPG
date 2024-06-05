using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLUPlayer : MonoBehaviour
{
    public CPlayerValue playerValues = null;
    public CLevelUpManager luManager = null;

    private Animator animator = null;

    // To Check Coroutine
    private bool isSrt = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(!isSrt)
        {
            isSrt = true;
            StartCoroutine(DelayStart());
        }
    }

    void SetLUAnim()
    {
        animator.SetBool("isLU", true);
    }

    void SetCBAnim()
    {
        animator.SetBool("isCB", true);
    }

    void InitAnim()
    {
        animator.SetBool("isLU", false);
        animator.SetBool("isCB", false);
    }

    IEnumerator DelayStart()
    {
        // wait Light set
        yield return new WaitForSeconds(1.0f);

        if (luManager.curLevelUp == playerValues.Me)
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

        luManager.status = 1;
    }
}
