using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLUText : MonoBehaviour
{
    // Message 01
    public Text m01Name = null;
    public Text m01Level = null;

    // Message 02
    public Text[] m02 = new Text[10];

    // Message 03
    public Text[] param01 = new Text[3];
    public Text[] param02 = new Text[3];

    void Update()
    {
        SetText();
    }

    void SetText()
    {
        // Message 01 Set
        m01Name.text = GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_name + "의 레벨이";
        m01Level.text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_level + 1);

        // Message 02 Set
        // HP
        m02[0].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_maxhp;
        m02[1].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_maxhp + 5);
        
        // Attack
        m02[2].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_attak;
        m02[3].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_attak + 3);

        // Defense
        m02[4].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_defense;
        m02[5].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_defense + 2);

        // Magic Attack
        m02[6].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_magicAttack;
        m02[7].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_magicAttack + 2);

        // Magic Defense
        m02[8].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_magicDefense;
        m02[9].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_magicDefense + 2);

        // Message 03
        if(CLevelUpManager.lu.whichSelect == 0) // Hammer
        {
            // param 01
            param01[0].text = "공격";
            param01[1].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_attak;
            param01[2].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_attak + 2);

            // param 02
            param02[0].text = "방어";
            param02[1].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_defense;
            param02[2].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_defense + 1);
        }
        else if (CLevelUpManager.lu.whichSelect == 1) // Mushroom
        {
            // param 01
            param01[0].text = "최대 HP";
            param01[1].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_maxhp;
            param01[2].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_maxhp + 3);
        }
        else if (CLevelUpManager.lu.whichSelect == 2) // Flower
        {
            // param 01
            param01[0].text = "마법 공격";
            param01[1].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_magicAttack;
            param01[2].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_magicAttack + 1);

            // param 02
            param02[0].text = "마법 방어";
            param02[1].text = "" + GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_magicDefense;
            param02[2].text = "" + (GameManager.instance.members[CLevelUpManager.lu.curLevelUp].m_stat.m_magicDefense + 1);
        }
    } 
}
