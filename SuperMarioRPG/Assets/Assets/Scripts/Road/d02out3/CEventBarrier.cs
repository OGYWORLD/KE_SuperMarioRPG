using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEventBarrier : MonoBehaviour
{
    public GameObject canvas = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.isHammerBroDead && GameManager.instance.isHammerBroEvent == 1)
        {
            canvas.SetActive(true);
        }
    }
}
