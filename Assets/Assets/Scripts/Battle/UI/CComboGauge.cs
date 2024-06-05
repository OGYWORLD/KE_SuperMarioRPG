using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CComboGauge : MonoBehaviour
{
    public CBattleManager btlManager = null;

    private Animator anim = null;

    public Text gauge = null;
    public Text gaugePercent = null;
    public GameObject textBox = null;

    public Image gaugeImage = null;

    public Animator matorFullEff = null;

    private bool isGameSrt = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(FirstShow());
    }

    void Update()
    {
        SetGaugeFillAmount();

        if (btlManager.status != 6 && btlManager.status != 7)
        {
            ShowFullEff();
            ShowGauge();
        }
        else
        {
            gaugePercent.text = "";
            anim.SetBool("state", false);
            textBox.SetActive(false);
            matorFullEff.SetBool("state", false);
            isGameSrt = false;
        }
    }

    void ShowGauge()
    {
        if(GameManager.instance.gauge > 100)
        {
            GameManager.instance.gauge = 100;
        }

        gauge.text = "" + GameManager.instance.gauge;
    }

    IEnumerator FirstShow()
    {
        yield return new WaitForSeconds(2.5f);

        anim.SetBool("state", true);
        gaugePercent.text = "%";
        textBox.SetActive(true);
        isGameSrt = true;
    }

    void SetGaugeFillAmount()
    {
        // Do Not Update When Default Menu State
        if(btlManager.status != 1)
        {
            gaugeImage.fillAmount = GameManager.instance.gauge * 0.01f;
        }
    }

    void ShowFullEff()
    {
        if(isGameSrt)
        {
            if (gaugeImage.fillAmount >= 1)
            {
                matorFullEff.SetBool("state", true);
            }
            else
            {
                matorFullEff.SetBool("state", false);
            }
        }
    }

    public void SetGauge10()
    {
        anim.SetTrigger("10");
    }

    public void updateGaugeImage(int _n)
    {
        gaugeImage.fillAmount += 0.01f * _n;
    }
}
