using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHBActive : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(!GameManager.instance.isHammerBroDead);   
    }
}
