using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterAction : MonoBehaviour
{
    private Animator animator = null;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isBattled", GameManager.instance.isNowBattle);
    }


}
