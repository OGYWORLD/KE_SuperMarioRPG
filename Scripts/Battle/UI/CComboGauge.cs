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

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(FirstShow());
    }

    void Update()
    {
        if(btlManager.status != 6 && btlManager.status != 7)
        {
            ShowGauge();
        }
        else
        {
            anim.SetBool("state", false);
            textBox.SetActive(false);
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
