using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTurnGoomba : MonoBehaviour
{
    public CMonsterMove move = null;

    void Start()
    {
        move.rcValues.runSpeed = 0f;
        move.rcValues.walkSpeed = 0f;

        gameObject.GetComponent<Animator>().SetBool("isBattled", true);

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Transform>().Rotate(Vector3.up * 1f);
    }
}
