using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CToadAssist : MonoBehaviour
{
    public CBattleManager btlManager = null;
    public Image gaugeImage = null;

    public GameObject waitButton = null;
    private Animator animator = null;

    public GameObject Box = null;
    private Transform boxTrans = null;
    private Animator boxAnim = null;

    // Items (0: rock candy, 1: mushroom, 2: flower)
    public GameObject[] toadItems = new GameObject[3];
    public GameObject cloudParticle = null;
    public GameObject toad = null;
    private int toadItmIdx = 0;

    // To Coroutine
    private bool isJump = false;

    void Start()
    {
        GameManager.instance.gauge = 100; // !!!!!!!! For DEBUG !!! delete plz
        animator = GetComponent<Animator>();
        boxTrans = Box.GetComponent<Transform>();
        boxAnim = Box.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FullEff();

        ShowBox();
        BoxAnimation();

        // hide when monster attack, win, loose
        if (btlManager.status != 0 && btlManager.status != 6 && btlManager.status != 7)
        {
            ShowButton();
        }
        else
        {
            HideButton();
        }
    }

    void ShowButton()
    {
        animator.SetBool("state", true);
    }

    void HideButton()
    {
        animator.SetBool("state", false);
    }

    void FullEff()
    {
        if(GameManager.instance.gauge >= 100)
        {
            waitButton.SetActive(false);
            PressedButton();
        }
        else
        {
            waitButton.SetActive(true);
        }
    }

    void PressedButton()
    {
        // Can Use in Default Menu
        if (btlManager.status == 1 && Input.GetKeyDown(KeyCode.Minus))
        {
            GameManager.instance.gauge = 0;
            waitButton.SetActive(true);
            StartCoroutine(ResetGauge());
        }

    }

    void ShowBox()
    {
        if(btlManager.status == 10)
        {
            Box.SetActive(true);
            StartCoroutine(BoxMotion());
        }
        else if(btlManager.status == 11) // rock candy
        {

        }
        else if (btlManager.status == 12) // mushroom
        {

        }
        else if (btlManager.status == 13) // flower essence
        {

        }
        else
        {
            boxTrans.position = new Vector3(0.2f, 4f, 3.5f);
            cloudParticle.SetActive(false);
            btlManager.jumpCount = 0;
        }
    }

    void BoxAnimation()
    {
        if(btlManager.jumpCount == 1 && !isJump)
        {
            isJump = true;
            boxAnim.SetInteger("JumpState", 1);
            StartCoroutine(ShowItmes());
        }
        else if(btlManager.jumpCount == 2 && isJump)
        {
            isJump = false;
            boxAnim.SetInteger("JumpState", 2);
            StartCoroutine(WaitClosedBox());
        }
    }

    IEnumerator ShowItmes()
    {
        while(btlManager.jumpCount == 1)
        {
            toadItmIdx++;

            if (toadItmIdx == 3)
            {
                toadItmIdx = 0;
            }

            toadItems[toadItmIdx].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            toadItems[toadItmIdx].SetActive(false);
        }
        toadItems[toadItmIdx].SetActive(true);

        yield break;
    }

    IEnumerator WaitClosedBox()
    {
        yield return new WaitForSeconds(1.5f);

        btlManager.jumpCount = 3;
        boxAnim.SetInteger("JumpState", 3);
        toadItems[toadItmIdx].SetActive(false);
        btlManager.status = 11 + toadItmIdx; // change btl Status
        Box.SetActive(false);
        cloudParticle.SetActive(true);
    }

    IEnumerator ResetGauge()
    {
        for (int i = 0; i < 100; i++)
        {
            gaugeImage.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.005f);
        }

        btlManager.status = 10;

        yield break;
    }

    IEnumerator BoxMotion()
    {
        Transform trans = Box.GetComponent<Transform>();
        float sumTime = 0f;
        float TotalTime = 50.0f;

        while (sumTime <= TotalTime)
        {
            trans.position = Vector3.Lerp(boxTrans.position, new Vector3(0.2f, 2.5f, 3.5f), sumTime / TotalTime);

            sumTime += Time.deltaTime;


            yield return null;
        }

        yield break;
    }
}
