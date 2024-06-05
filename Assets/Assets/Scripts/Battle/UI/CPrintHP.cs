using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPrintHP : MonoBehaviour
{
    public Text maxMarioHp = null;
    public Text curMarioHp = null;

    public Text maxMellowHp = null;
    public Text curMellowHp = null;

    void Start()
    {
        maxMarioHp.text = "" + GameManager.instance.members[0].m_maxhp;
        curMarioHp.text = "" + GameManager.instance.members[0].m_curhp;

        maxMellowHp.text = "" + GameManager.instance.members[1].m_maxhp;
        curMellowHp.text = "" + GameManager.instance.members[1].m_curhp;
    }

    // Update is called once per frame
    void Update()
    {
        maxMarioHp.text = "" + GameManager.instance.members[0].m_maxhp;
        curMarioHp.text = "" + GameManager.instance.members[0].m_curhp;

        maxMellowHp.text = "" + GameManager.instance.members[1].m_maxhp;
        curMellowHp.text = "" + GameManager.instance.members[1].m_curhp;
    }
}
