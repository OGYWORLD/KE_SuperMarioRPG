using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotateHammer : MonoBehaviour
{
    void Start()
    {
        if (GameManager.instance.isHammerBroEvent == 0 || GameManager.instance.isHammerBroEvent == 2)
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        gameObject.GetComponent<Transform>().Rotate(Vector3.forward * 1f);
    }
}
