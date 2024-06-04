using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBattleManager : MonoBehaviour
{
    // Playable Character
    public GameObject mario = null;
    public GameObject mellow = null;

    // Cur Status Info
    private int totalMstNum = 2;

    // 0: Monster Turn, 1: Select Menu, 2: Attack, 3: ETC, 4: Special, 5: Item, 6: Win, 7: Lose
    public int status { get; set; } = 0; // Command Status
    public int curTurn { get; set; } = 0; // 0: playable, 1: Monster
    public int curPlrTurn { get; set; } = 0; // 0: Mario, 1: Mellow
    public int curMstTurn { get; set; } = 0; // Who Turn !ATTACK! belong Monster 0: left, 1: right
    public int curAttack { get; set; } = 0; // Who Turn !GET ATTACK! belong Monsters 0: left, 1: right
    public bool isAttacked { get; set; } = false;

    // Playable Dead Info
    public Vector3[] plrPos { get; set; } = { new Vector3(0.33f, 0f, 3.309f), new Vector3(-1.74f, 0f, 2.91f) };
    public bool isMarioDead { get; set; } = false;
    public bool isMellowDead { get; set; } = false;

    // Monster Info
    public int[] monsterHp { get; set; }
    public int[] monsterAtk { get; set; }
    public int[] monsterExp { get; set; }
    public int[] monsterCoin { get; set; }

    public bool isLeftMstDead { get; set; } = false;
    public bool isRightMstDead { get; set; } = false;

    // Who Use a Mushroom Item
    public EMEMBER whoUseMushroom { get; set; } = EMEMBER.MARIO;

    void Start()
    {
        if(GameManager.instance.memberIndex == EMEMBER.MARIO)
        {
            mellow.SetActive(false);
        }
        else
        {
            mellow.SetActive(true);
        }

        monsterHp = new int[totalMstNum];
        monsterAtk = new int[totalMstNum];
        monsterExp = new int[totalMstNum];
        monsterCoin = new int[totalMstNum];

        for (int i = 0; i < totalMstNum; i++)
        {
            monsterHp[i] = GameManager.instance.monStats[GameManager.instance.btlMoster].m_hp;
            monsterAtk[i] = GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk;
            monsterExp[i] = GameManager.instance.monStats[GameManager.instance.btlMoster].m_exp;
            monsterCoin[i] = GameManager.instance.monStats[GameManager.instance.btlMoster].m_coin;
        }
    }

    void Update()
    {
        CheckMember();
        CheckMstDead();
        CheckPlrDead();
    }

    void CheckMember()
    {
        if(GameManager.instance.memberIndex == EMEMBER.MARIO)
        {
            curPlrTurn = 0;
        }
    }

    void CheckMstDead()
    {
        if (monsterHp[0] <= 0 && !isLeftMstDead)
        {
            curMstTurn = 1; // pass leftMstTurn To rightMst
            isLeftMstDead = true;
        }
        if (monsterHp[1] <= 0 && !isRightMstDead)
        {
            curMstTurn = 0; // pass rightMstTurn To leftMst
            isRightMstDead = true;
        }
        if(isLeftMstDead && isRightMstDead)
        {
            status = 6; // WIN
        }
    }

    void CheckPlrDead()
    {
        if (GameManager.instance.members[0].m_curhp <= 0)
        {
            curPlrTurn = 1; // pass MarioTurn To Mellow
            isMarioDead = true;
            // !!! NEED DEAD SITUATION
        }
        if (GameManager.instance.members[1].m_curhp <= 0)
        {
            curPlrTurn = 0; // pass MellowTurn To Mario
            isMellowDead = true;
            // !!! NEED DEAD SITUATION
        }
        if (GameManager.instance.memberIndex == EMEMBER.MARIO && isMarioDead)
        {
            status = 7; // LOSE
        }
        if (GameManager.instance.memberIndex == EMEMBER.MELLOW && isMarioDead && isMellowDead)
        {
            status = 7; // LOSE
        }
    }
}
