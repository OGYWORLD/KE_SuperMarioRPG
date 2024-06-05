using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWindow : MonoBehaviour
{
    public CLevelUpManager luManager = null;

    private Animator animator = null;

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
        if(luManager.status == 1)
        {
            animator.SetBool("state", true);
        }
    }
}
