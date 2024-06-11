using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CEquipSet : MonoBehaviour
{
    const int totalWeapon = 1;
    const int totalArmor = 2;

    public Text weaponTxt = null;
    public Text clothesTxt = null;

    // In Equip setting menu
    public GameObject[] categoryWeapon = new GameObject[2];
    public GameObject[] categoryArmor = new GameObject[2];
    public GameObject[] listBase = new GameObject[2];
    public Text[] listTxt = new Text[2];

    public GameObject[] cursor = new GameObject[2];

    // In Equip setting menu stats
    public GameObject[] YaR = new GameObject[4];
    public Text[] preNum = new Text[4];
    public Text[] postNum = new Text[4];

    // explain text
    public Text explain = null;

    private List<string> itemNames;

    public int totalListNum { get; set; } = 0;
    public int cursorPos { get; set; } = 0; // 0: up, 1: down
    private int curChooseItem = 0; // -1: nothing, 0: hammer, 1: shirt, 2: pants

    void Start()
    {
        itemNames = new List<string>();
        itemNames.Add("�ظ�");
        itemNames.Add("����۾���");
        itemNames.Add("�������");
    }

    // Update is called once per frame
    void Update()
    {
        defaultTxtSetting();

        SettingWeapon();
        GetInput();
        PrintStat();
        SetChooseItem();
        Init();
    }

    void defaultTxtSetting()
    {
        // no set any weapon
        if (GameManager.instance.members[(int)MenuManager.menu.curInfo].m_weapon.Length == 0)
        {
            weaponTxt.text = "����";
        }
        else
        {
            weaponTxt.text = GameManager.instance.members[(int)MenuManager.menu.curInfo].m_weapon;
        }

        // no set any clothes
        if (GameManager.instance.members[(int)MenuManager.menu.curInfo].m_clothes.Length == 0)
        {
            clothesTxt.text = "��ű�";
        }
        else
        {
            clothesTxt.text = GameManager.instance.members[(int)MenuManager.menu.curInfo].m_clothes;
        }
    }

    void SettingWeapon()
    {
        if (MenuManager.menu.equipPage == 1 && MenuManager.menu.whichEquip == 0)
        {
            int index = 0;
            for (int i = 0; i < totalWeapon; i++)
            {
                if (GameManager.instance.validItem[i][4] == (int)MenuManager.menu.curInfo && GameManager.instance.validItem[i][5] != 0)
                {
                    listBase[index].SetActive(true);
                    categoryWeapon[index].SetActive(true);
                    listTxt[index].text = itemNames[i];

                    index++;

                }
            }

            totalListNum = index;

            SetNoItem(index);
        }
        else if(MenuManager.menu.equipPage == 1 && MenuManager.menu.whichEquip == 1)
        {
            int index = 0;
            for (int i = 1; i <= totalArmor; i++)
            {
                if (GameManager.instance.validItem[i][4] == (int)MenuManager.menu.curInfo && GameManager.instance.validItem[i][5] != 0)
                {
                    listBase[index].SetActive(true);
                    categoryArmor[index].SetActive(true);
                    listTxt[index].text = itemNames[i];

                    index++;

                }
            }

            totalListNum = index;

            SetNoItem(index);
        }
    }

    void Init()
    {
        if(MenuManager.menu.equipPage == 0)
        {
            for (int i = 0; i < categoryArmor.Length; i++)
            {
                categoryWeapon[i].SetActive(false);
                categoryArmor[i].SetActive(false);
                listBase[i].SetActive(false);
                listTxt[i].text = "";
            }

            cursor[0].SetActive(true);
            cursor[1].SetActive(false);

            cursorPos = 0;
            curChooseItem = 0;
            explain.text = "";
        }
    }

    void SetNoItem(int _i)
    {
        listBase[_i].SetActive(true);
        if(MenuManager.menu.whichEquip == 0)
        {
            categoryWeapon[_i].SetActive(true);
        }
        else
        {
            categoryArmor[_i].SetActive(true);
        }
        listTxt[_i].text = "��� ����";
    }

    void GetInput()
    {
        if(MenuManager.menu.equipPage == 1 && Input.GetKeyDown(KeyCode.UpArrow) && totalListNum != 0)
        {
            cursor[0].SetActive(true);
            cursor[1].SetActive(false);
            cursorPos = 0;
        }
        else if (MenuManager.menu.equipPage == 1 && Input.GetKeyDown(KeyCode.DownArrow) && totalListNum != 0)
        {
            cursor[0].SetActive(false);
            cursor[1].SetActive(true);
            cursorPos = 1;
        }
    }

    void SetChooseItem()
    {
        if(MenuManager.menu.equipPage == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            if (totalListNum == 0)
            {
                curChooseItem = -1;

                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_weapon = "";
                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_clothes = "";
            }
            else
            {
                if (MenuManager.menu.whichEquip == 0)
                {
                    if (cursorPos == 0)
                    {
                        // ���� ���� Ȯ��
                        if (!GameManager.instance.validItem[(int)ECLOTHES.HAMMER].m_isTake)
                        {
                            // ������ �� �ߴ� ���¿���
                            GameManager.instance.members[(int)MenuManager.menu.curInfo].m_curAttak = 1;

                            GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat = new Stats(
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_attak + 10,
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_defense,
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicAttack,
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicDefense
                                );

                        }
                        else
                        {
                            // �ߴ� ����
                        }

                        // �ظ� ����
                        GameManager.instance.members[(int)MenuManager.menu.curInfo].m_weapon = "�ظ�";
                        GameManager.instance.validItem[(int)ECLOTHES.HAMMER].m_isTake = true;

                    }
                    else
                    {
                        // ���� ���� Ȯ��
                        if (!GameManager.instance.validItem[(int)ECLOTHES.HAMMER].m_isTake)
                        {
                            // ������ �� �ߴ� ���¿���

                        }
                        else
                        {
                            // �ߴ� ����
                            GameManager.instance.members[(int)MenuManager.menu.curInfo].m_curAttak = 0;

                            GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat = new Stats(
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_attak - 10,
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_defense,
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicAttack,
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicDefense
                                );
                        }

                        // �ƹ��͵� �ƴ�
                        curChooseItem = -1;
                        GameManager.instance.members[(int)MenuManager.menu.curInfo].m_weapon = "";
                        GameManager.instance.validItem[(int)ECLOTHES.HAMMER].m_isTake = false;
                    }
                }
                else if (MenuManager.menu.whichEquip == 1)
                {
                    if (MenuManager.menu.curInfo == EMEMBER.MARIO)
                    {
                        if (cursorPos == 0)
                        {
                            // ���� ���� Ȯ��
                            if (!GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_isTake)
                            {
                                // ������ �� �ߴ� ���¿���
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat = new Stats(
                                    GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_attak,
                                    GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_defense + 6,
                                    GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicAttack,
                                    GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicDefense + 6
                                    );

                            }
                            else
                            {
                                // �ߴ� ����
                            }

                            // �븻 ���� ����
                            GameManager.instance.members[(int)MenuManager.menu.curInfo].m_clothes = "����۾���";
                            GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_isTake = true;
                        }
                        else
                        {
                            // ���� ���� Ȯ��
                            if (!GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_isTake)
                            {
                                // ������ �� �ߴ� ���¿���

                            }
                            else
                            {
                                // �ߴ� ����
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat = new Stats(
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_attak,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_defense - 6,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicAttack,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicDefense - 6
                                   );
                            }

                            // �ƹ��͵� �ƴ�
                            curChooseItem = -1;
                            GameManager.instance.members[(int)MenuManager.menu.curInfo].m_clothes = "";
                            GameManager.instance.validItem[(int)ECLOTHES.PANTS].m_isTake = false;
                        }
                    }
                    else if (MenuManager.menu.curInfo == EMEMBER.MELLOW)
                    {
                        if (cursorPos == 0)
                        {
                            // ���� ���� Ȯ��
                            if (!GameManager.instance.validItem[(int)ECLOTHES.PANTS].m_isTake)
                            {
                                // ������ �� �ߴ� ���¿���
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat = new Stats(
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_attak,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_defense + 6,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicAttack,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicDefense + 3
                                   );
                            }
                            else
                            {
                                // �ߴ� ����
                            }

                            // �븻 ���� ����
                            GameManager.instance.members[(int)MenuManager.menu.curInfo].m_clothes = "�������";
                            GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_isTake = true;
                        }
                        else
                        {
                            // ���� ���� Ȯ��
                            if (!GameManager.instance.validItem[(int)ECLOTHES.PANTS].m_isTake)
                            {
                                // ������ �� �ߴ� ���¿���
                            }
                            else
                            {
                                // �ߴ� ����
                                GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat = new Stats(
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_attak,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_defense - 6,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicAttack,
                                   GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat.m_magicDefense - 3
                                   );
                            }

                            // �ƹ��͵� �ƴ�
                            curChooseItem = -1;
                            GameManager.instance.members[(int)MenuManager.menu.curInfo].m_clothes = "";
                            GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_isTake = false;
                        }
                    }
                }
            }
        }
    }

    void PrintStat()
    {
        if (MenuManager.menu.equipPage == 1)
        {
            for(int i = 0; i < YaR.Length; i++)
            {
                YaR[i].SetActive(true);
            }

            if (totalListNum == 0)
            {
                explain.text = "";
                nothingStat();
            }
            else
            {
                if (MenuManager.menu.whichEquip == 0) // hammer
                {
                    if (cursorPos == 0)
                    {
                        explain.text = "�ε���� ��η��߸��ϴ�.";
                        // ���� ���� Ȯ��
                        if (!GameManager.instance.validItem[(int)ECLOTHES.HAMMER].m_isTake)
                        {
                            // ������ �� �ߴ� ���¿���
                            PrintDetailStat(0, 0);
                        }
                        else
                        {
                            // �ߴ� ����
                            nothingStat();
                        }
                    }
                    else
                    {
                        explain.text = "";

                        // ���� ���� Ȯ��
                        if (!GameManager.instance.validItem[(int)ECLOTHES.HAMMER].m_isTake)
                        {
                            // ������ �� �ߴ� ���¿���
                            nothingStat();
                        }
                        else
                        {
                            // �ߴ� ����
                            PrintDetailStat(0, 1);
                        }
                    }
                }
                else if (MenuManager.menu.whichEquip == 1)
                {
                    if (MenuManager.menu.curInfo == EMEMBER.MARIO)
                    {
                        if (cursorPos == 0)
                        {
                            explain.text = "����� �۾����Դϴ�.";
                            // ���� ���� Ȯ��
                            if (!GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_isTake)
                            {
                                PrintDetailStat(1, 0);

                            }
                            else
                            {
                                // �ߴ� ����
                                nothingStat();
                            }
                        }
                        else
                        {
                            explain.text = "";
                            // ���� ���� Ȯ��
                            if (!GameManager.instance.validItem[(int)ECLOTHES.SHIRT].m_isTake)
                            {
                                // ������ �� �ߴ� ���¿���
                                nothingStat();
                            }
                            else
                            {
                                // �ߴ� ����
                                PrintDetailStat(1, 1);
                            }
                        }
                    }
                    else if (MenuManager.menu.curInfo == EMEMBER.MELLOW)
                    {
                        if (cursorPos == 0)
                        {
                            explain.text = "����� �����Դϴ�.";
                            // ���� ���� Ȯ��
                            if (!GameManager.instance.validItem[(int)ECLOTHES.PANTS].m_isTake)
                            {
                                PrintDetailStat(2, 0);
                            }
                            else
                            {
                                // �ߴ� ����
                                nothingStat();
                            }
                        }
                        else
                        {
                            explain.text = "";
                            // ���� ���� Ȯ��
                            if (!GameManager.instance.validItem[(int)ECLOTHES.PANTS].m_isTake)
                            {
                                // ������ �� �ߴ� ���¿���
                                nothingStat();
                            }
                            else
                            {
                                // �ߴ� ����
                                PrintDetailStat(2, 1);
                            }
                        }
                    }
                }
            }
        }

    }

    void nothingStat()
    {
        for(int i = 0; i < preNum.Length; i++)
        {
            preNum[i].text = "-";
            postNum[i].text = "-";
        }
    }

    void PrintDetailStat(int _i, int _pn)
    {
        for (int i = 0; i < preNum.Length; i++)
        {
            if(_pn == 0) // plus
            {
                preNum[i].text = "" + GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat[i];
                postNum[i].text = "" + (GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat[i] + GameManager.instance.validItem[_i][i]);
            }
            else // minus
            {
                preNum[i].text = "" + GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat[i];
                postNum[i].text = "" + (GameManager.instance.members[(int)MenuManager.menu.curInfo].m_stat[i] - GameManager.instance.validItem[_i][i]);
            }
        }
    }
}
