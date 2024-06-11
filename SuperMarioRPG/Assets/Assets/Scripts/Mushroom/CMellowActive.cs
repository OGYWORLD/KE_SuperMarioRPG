using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMellowActive : MonoBehaviour
{
    void Start()
    {
        if(GameManager.instance.isWithMellow)
        {
            gameObject.SetActive(false);
        }
    }
}
