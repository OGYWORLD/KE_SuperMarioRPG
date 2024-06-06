using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTalk : MonoBehaviour
{
    public CMarioMove CMario = null;
    public GameObject Mario = null;

    public Animator marioAnim = null;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CMario.isTouched = true;
            CMario.isDrived = true;
            marioAnim.SetBool("isWalk", true);

            gameObject.SetActive(false);
        }
    }
}
