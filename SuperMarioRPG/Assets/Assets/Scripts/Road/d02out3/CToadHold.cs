using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CToadHold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // if Event end, hide toad
        if (GameManager.instance.isHammerBroEvent == 2)
        {
            gameObject.SetActive(false);
        }
        else if(GameManager.instance.isHammerBroEvent == 0)
        {
            gameObject.GetComponent<Animator>().SetBool("isCaught", true);
        }
        else if(GameManager.instance.isHammerBroEvent == 1)
        {
            gameObject.GetComponent<Animator>().SetBool("isTalk", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
