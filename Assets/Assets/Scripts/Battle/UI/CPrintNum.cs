using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPrintNum : MonoBehaviour
{
    public GameObject background = null;

    public GameObject[] damageNumbers_1;
    public GameObject[] damageNumbers_2;

    private Vector3[] backgroundPos;

    void Start()
    {
        // 0: left Mon, 1: right Mon, 2: Mario, 3: Mellow
        backgroundPos = new Vector3[] { new Vector3(850f, 550f, 0f), new Vector3(1400f, 470f, 0f), new Vector3(700f, 300f, 0f), new Vector3(1000f, 300f, 0f) };
    }

    public enum EWHO
    {
        MARIO,
        MELLOW,
        LEFT_MON,
        RIGHT_MON
    }
    public enum ECategory
    {
        DAMAGE,
        HEAL
    }

    public void PrintNum(int _n, int _p)
    {
        StartCoroutine(SetNumObj(_n, _p));
    }

    public void PrintRockCandy()
    {
        StartCoroutine(PrintRockCandyNum());
    }

    IEnumerator SetNumObj(int _n, int _p)
    {
        string num = _n.ToString();
        int Len = num.Length;

        background.GetComponent<Transform>().position = backgroundPos[_p];

        if (Len == 1)
        {
            damageNumbers_1[num[0] - 48].GetComponent<Transform>().position = backgroundPos[_p] + new Vector3(5f, 0f, 0f);
            damageNumbers_1[num[0] - 48].SetActive(true);
        }
        else
        {
            damageNumbers_1[num[0] - 48].GetComponent<Transform>().position = backgroundPos[_p] + new Vector3(-15f, 0f, 0f);
            damageNumbers_1[num[0] - 48].SetActive(true);

            damageNumbers_2[num[1] - 48].GetComponent<Transform>().position = backgroundPos[_p] + new Vector3(25f, 0f, 0f);
            damageNumbers_2[num[1] - 48].SetActive(true);
        }

        background.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        if (Len == 1)
        {
            damageNumbers_1[num[0] - 48].SetActive(false);
        }
        else
        {
            damageNumbers_1[num[0] - 48].SetActive(false);
            damageNumbers_2[num[1] - 48].SetActive(false);
        }

        background.SetActive(false);
    }

    IEnumerator PrintRockCandyNum()
    {
        background.GetComponent<Transform>().position = backgroundPos[0] + new Vector3(300f, 0f, 0f); ;

        damageNumbers_1[9].GetComponent<Transform>().position = backgroundPos[0] + new Vector3(285f, 0f, 0f);
        damageNumbers_1[9].SetActive(true);

        damageNumbers_2[9].GetComponent<Transform>().position = backgroundPos[0] + new Vector3(325f, 0f, 0f);
        damageNumbers_2[9].SetActive(true);

        background.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        damageNumbers_1[9].SetActive(false);
        damageNumbers_2[9].SetActive(false);

        background.SetActive(false);
    }
}
