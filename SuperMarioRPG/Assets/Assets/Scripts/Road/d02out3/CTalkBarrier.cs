using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTalkBarrier : MonoBehaviour
{
    public GameObject canvas = null;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !GameManager.instance.isHammerBroDead && !GameManager.instance.isMenu)
        {
            canvas.SetActive(true);
        }
    }
}
