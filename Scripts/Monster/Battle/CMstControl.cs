using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMstControl : MonoBehaviour
{
    enum EMONSTER
    {
        LEFT,
        RIGHT
    }

    // Values for delay
    private float timeCheck = 0f;

    // Values for Coroutine
    private bool isAttack = false;
    private bool isTiming = false;
    private bool isScAtkCmd = false;

    private bool isWin = false;

    // Values for move speed
    private float fowardSpeed = 4f;

    // Battle Manager
    public CBattleManager btlManager = null;

    // Playerable Character
    public CPlayerBattle[] player = new CPlayerBattle[2];

    // Values for Monsters(Left, Right)
    public Vector3[] mstPos { get; set; } = new Vector3[2];
    public Vector3[] mstFinPos { get; set; } = new Vector3[2];
    public GameObject[] mstObj { get; set; } = new GameObject[2];
    public Transform[] mstTrans { get; set; } = new Transform[2];
    public Animator[] animator { get; set; } = new Animator[2];

    // Particles
    private GameObject[] attackedParticle = new GameObject[2];
    private GameObject[] acAttackedParticle = new GameObject[2];
    public GameObject[] deadParticle = new GameObject[2]; // this particle has to active when disapear monsters
    public GameObject[] coinParticle = new GameObject[2];

    // Monster Attacks To Who
    public EMEMBER attackTo { get; set; } = EMEMBER.MARIO;

    // Damage Num
    public CPrintNum printNum = null;

    // HPBar
    public Image[] HPBar = new Image[2];

    // Combo Gauge
    public CComboGauge combo = null;

    void Awake()
    {
        // Set Monster Fin(Default) Pos
        string monster = GameManager.instance.btlMoster;

        mstFinPos[(int)EMONSTER.LEFT] = new Vector3(1.86f, GameManager.instance.curMoster[monster].m_monsterPosY, -1.19f);
        mstFinPos[(int)EMONSTER.RIGHT] = new Vector3(-1.28f, GameManager.instance.curMoster[monster].m_monsterPosY, -3.29f);

        // Set Monster Battle Start Pos
        mstPos[(int)EMONSTER.LEFT] = new Vector3(1.86f, 1f, -5.29f);
        mstPos[(int)EMONSTER.RIGHT] = new Vector3(-1.28f, 1f, -5.29f);

        // Instantiate Monster
        mstObj[(int)EMONSTER.LEFT] = Instantiate(GameManager.instance.curMoster[monster].m_monsterPrefeb, mstPos[(int)EMONSTER.LEFT], Quaternion.identity);
        mstObj[(int)EMONSTER.RIGHT] = Instantiate(GameManager.instance.curMoster[monster].m_monsterPrefeb, mstPos[(int)EMONSTER.RIGHT], Quaternion.identity);
        mstObj[(int)EMONSTER.LEFT].SetActive(false);
        mstObj[(int)EMONSTER.RIGHT].SetActive(false);

        // Get Monster's Trans and Anim
        mstTrans[(int)EMONSTER.LEFT] = mstObj[(int)EMONSTER.LEFT].GetComponent<Transform>();
        mstTrans[(int)EMONSTER.RIGHT] = mstObj[(int)EMONSTER.RIGHT].GetComponent<Transform>();

        animator[(int)EMONSTER.LEFT] = mstObj[(int)EMONSTER.LEFT].GetComponent<Animator>();
        animator[(int)EMONSTER.RIGHT] = mstObj[(int)EMONSTER.RIGHT].GetComponent<Animator>();

        // Set Attacked Particle (Prefab has)
        attackedParticle[(int)EMONSTER.LEFT] = mstObj[(int)EMONSTER.LEFT].transform.GetChild(2).gameObject;
        attackedParticle[(int)EMONSTER.RIGHT] = mstObj[(int)EMONSTER.RIGHT].transform.GetChild(2).gameObject;

        acAttackedParticle[(int)EMONSTER.LEFT] = mstObj[(int)EMONSTER.LEFT].transform.GetChild(3).gameObject;
        acAttackedParticle[(int)EMONSTER.RIGHT] = mstObj[(int)EMONSTER.RIGHT].transform.GetChild(3).gameObject;

        StartCoroutine(ShowMonster());

    }

    void Update()
    {
        CheckDead();
        AttackToWho();

        timeCheck += Time.deltaTime;
        if (timeCheck > 1.5f && btlManager.curTurn == 0)
        {
            ResetPos();
        }
        else if (btlManager.status == 0 && btlManager.curTurn == 1)
        {
            AttackMove();
            if (!isAttack)
            {
                Attack();
            }
        }

        if (isTiming)
        {
            CheckActionCommand((int)attackTo); // Call Player Check Action Command Func
        }

        if(btlManager.status == 6 && !isWin)
        {
            isWin = true;
            UpdateInfo();
        }
    }

    IEnumerator ShowMonster()
    {
        yield return new WaitForSeconds(0.5f);
        ActiveMonster();
    }
    void ActiveMonster()
    {
        mstObj[(int)EMONSTER.LEFT].SetActive(true);
        mstObj[(int)EMONSTER.RIGHT].SetActive(true);
    }

    // Reset Monster Position
    void ResetPos()
    {
        mstTrans[(int)EMONSTER.LEFT].position = Vector3.Lerp(mstTrans[(int)EMONSTER.LEFT].position, mstFinPos[(int)EMONSTER.LEFT], Time.deltaTime * fowardSpeed);
        mstTrans[(int)EMONSTER.RIGHT].position = Vector3.Lerp(mstTrans[(int)EMONSTER.RIGHT].position, mstFinPos[(int)EMONSTER.RIGHT], Time.deltaTime * fowardSpeed);
    }

    // Attack to Playable
    void AttackMove()
    {
        mstTrans[btlManager.curMstTurn].position = Vector3.Lerp(mstTrans[btlManager.curMstTurn].position, btlManager.plrPos[(int)attackTo] - new Vector3(0f, 0f, 1f), Time.deltaTime * fowardSpeed);
    }

    void Attack()
    {
        isTiming = true;
        isAttack = true;
        StartCoroutine(AttackMotion(btlManager.curMstTurn, attackTo));
    }

    // Call from CPlayerBattle
    public void ShowAttackedMon()
    {
        animator[btlManager.curAttack].SetBool("isAttacked", true);
        attackedParticle[btlManager.curAttack].SetActive(true);
    }

    // Call from CPlayerBattle
    public void SetMonState()
    {
        attackedParticle[(int)EMONSTER.LEFT].SetActive(false);
        attackedParticle[(int)EMONSTER.RIGHT].SetActive(false);

        animator[(int)EMONSTER.LEFT].SetBool("isAttacked", false);
        animator[(int)EMONSTER.RIGHT].SetBool("isAttacked", false);
    }

    // Call from CPlayerBattle
    public void ShowACParticle()
    {
        acAttackedParticle[btlManager.curAttack].SetActive(true);
    }

    public void HideACParticle()
    {
        acAttackedParticle[(int)EMONSTER.LEFT].SetActive(false);
        acAttackedParticle[(int)EMONSTER.RIGHT].SetActive(false);
    }

    // For Showing Dead Particle When Monster Dead
    void CheckDead()
    {
        if (btlManager.isLeftMstDead)
        {
            StartCoroutine(showDead((int)EMONSTER.LEFT));
        }
        if (btlManager.isRightMstDead)
        {
            StartCoroutine(showDead((int)EMONSTER.RIGHT));
        }
    }

    void UpdatePlrHP()
    {
        if (isScAtkCmd)
        {
            int dam = GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk - GameManager.instance.members[(int)attackTo].m_stat.m_defense;
            if(dam < 0)
            {
                dam = 0;
            }
            GameManager.instance.members[(int)attackTo].m_curhp -= dam;
            StartCoroutine(UpdateBarAnim((int)attackTo, GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk - GameManager.instance.members[(int)attackTo].m_stat.m_defense));
        }
        else
        {
            GameManager.instance.members[(int)attackTo].m_curhp -= GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk;
            StartCoroutine(UpdateBarAnim((int)attackTo, GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk));
        }

        if (GameManager.instance.members[(int)attackTo].m_curhp < 0)
        {
            GameManager.instance.members[(int)attackTo].m_curhp = 0;
        }
    }

    void AttackToWho()
    {
        if (GameManager.instance.memberIndex == (int)EMEMBER.MARIO)
        {
            attackTo = EMEMBER.MARIO;
        }
        else // With Mellow
        {
            if (GameManager.instance.members[(int)EMEMBER.MARIO].m_curhp > GameManager.instance.members[(int)EMEMBER.MELLOW].m_curhp)
            {
                attackTo = EMEMBER.MARIO;
            }
            else
            {
                attackTo = EMEMBER.MELLOW;
            }
        }
    }

    // For Showing Dead Particle When Monster Dead
    IEnumerator showDead(int _p)
    {
        animator[_p].SetBool("isDead", true);
        yield return new WaitForSeconds(1.0f);
        mstObj[_p].SetActive(false);
        deadParticle[_p].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        coinParticle[_p].SetActive(true);
    }

    IEnumerator AttackMotion(int _p, EMEMBER _m)
    {
        yield return new WaitForSeconds(0.3f); // run to character

        animator[_p].SetBool("isAttack", true); // Play Attack Motion

        yield return new WaitForSeconds(0.5f);

        player[(int)_m].timingIcon.SetActive(true);

        // Wait for Player Check Action Command Func

        yield return new WaitForSeconds(0.1f);

        isTiming = false;

        if (isScAtkCmd) // Check Action Command
        {
            int dam = GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk - GameManager.instance.members[(int)_m].m_stat.m_defense;
            if(dam < 0)
            {
                dam = 0;
            }
            printNum.PrintNum(dam, (int)_m + 2); // print Damage Num

            player[(int)_m].SuccessDfActionCommand();
            player[(int)_m].defenseParticle.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            player[(int)_m].defenseParticle.SetActive(false);
        }
        else
        {
            printNum.PrintNum(GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk, (int)_m + 2); // print Damage Num
            player[(int)_m].ShowHitAnim();

            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(0.3f);

        btlManager.status = 1; // Turn to Show Command Menu
        btlManager.curTurn = 0; // Turn to Playable
        isAttack = false;
        CheckMonsterTurn();

        player[(int)_m].StopHitAnim();
        player[(int)_m].StopDFActionCommand();
        player[(int)_m].timingIcon.SetActive(false);
        animator[_p].SetBool("isAttack", false);

        UpdatePlrHP();
        isScAtkCmd = false;
    }

    void CheckMonsterTurn()
    {
        if (btlManager.isLeftMstDead)
        {
            btlManager.curMstTurn = 1;
        }
        else if (btlManager.isRightMstDead)
        {
            btlManager.curMstTurn = 0;
        }
        else if (btlManager.curMstTurn == 0)
        {
            btlManager.curMstTurn = 1;
        }
        else
        {
            btlManager.curMstTurn = 0;
        }
    }

    public void CheckActionCommand(int _m)
    {
        if (isTiming)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                combo.SetGauge10();
                GameManager.instance.gauge += 2; // defense Action Command Update
                combo.updateGaugeImage(2);

                isScAtkCmd = true;
                isTiming = false;
            }
        }
    }

    void UpdateInfo()
    {
        // Update EXP
        for (int i = 0; i < (int)GameManager.instance.memberIndex + 1; i++)
        {
            GameManager.instance.members[i].m_exp += GameManager.instance.monStats[GameManager.instance.btlMoster].m_exp * 2;
        }

        // Update Coin
        GameManager.instance.coin += GameManager.instance.monStats[GameManager.instance.btlMoster].m_coin * 2;
    }

    IEnumerator UpdateBarAnim(int _w, int _n)
    {
        for (int i = 0; i < _n; i++)
        {
            HPBar[_w].fillAmount -= 1f / (float)GameManager.instance.members[_w].m_maxhp;
            yield return new WaitForSeconds(0.005f);
        }

        yield break;
    }
}
