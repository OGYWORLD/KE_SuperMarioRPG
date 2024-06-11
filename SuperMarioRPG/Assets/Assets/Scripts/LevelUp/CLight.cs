using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLight : MonoBehaviour
{
    private Vector3[] lightPos = new Vector3[2];

    private Animator animator = null;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        lightPos[0] = new Vector3(0f, 0f, 0f);
        lightPos[1] = new Vector3(1.16f, 0f, 1.16f);

        gameObject.GetComponent<Transform>().position = lightPos[CLevelUpManager.lu.curLevelUp];
    }

    void Update()
    {
        SetLight();
    }

    void SetLight()
    {
        if(CLevelUpManager.lu.status == 4)
        {
            animator.SetBool("e47spotlight", false);
            StartCoroutine(ChangeLight());
        }
    }

    IEnumerator ChangeLight()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.GetComponent<Transform>().position = lightPos[CLevelUpManager.lu.curLevelUp];
        animator.SetBool("e47spotlight", true);
    }
}
