using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWindow : MonoBehaviour
{
    private Animator animator = null;

    public GameObject message01 = null;
    public GameObject message02 = null;
    public GameObject message03 = null;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        SetWindow();
    }

    void SetWindow()
    {
        if (CLevelUpManager.lu.status == 1)
        {
            animator.SetBool("state", true);
            message01.SetActive(true);
        }
        else if(CLevelUpManager.lu.status == 2)
        {
            message01.SetActive(false);
            message02.SetActive(true);
        }
        else if (CLevelUpManager.lu.status == 3)
        {
            message02.SetActive(false);
            message03.SetActive(true);
        }
        else
        {
            message01.SetActive(false);
            message02.SetActive(false);
            message03.SetActive(false);
        }
    }
}
