using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBMstCtl : MonoBehaviour
{
    enum EMONSTER
    {
        LEFT,
        RIGHT
    }

    enum EATTACK
    {
        ATTACK,
        POWER_UP,
        SPECIAL
    }

    // Values for Coroutine
    private bool isAttack = false;
    private bool isTiming = false;
    private bool isScAtkCmd = false;

    private bool isWin = false;

    private int[] powerUpCnt = new int[2];

    // Values for move speed
    private float fowardSpeed = 4f;

    // Battle Manager
    public CBattleManager btlManager = null;

    // Playerable Character
    public CBPlayerBattle[] player = new CBPlayerBattle[2];

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
    public GameObject[] powerUpParticle = new GameObject[2];

    // Monster Attacks To Who
    public EMEMBER attackTo { get; set; } = EMEMBER.MARIO;

    // Damage Num
    public CPrintNum printNum = null;

    // HPBar
    public Image[] HPBar = new Image[2];

    // Combo Gauge
    public CBComboGauge combo = null;

    // Show Attack Text
    public GameObject atkBase = null;
    public Text baseTxt = null;

    private int atkOrPu = 0;

    // Audio
    private AudioSource monsterAudio = null;

    public AudioClip hammer = null;
    public AudioClip hammerDash = null;
    public AudioClip powerUp = null;
    public AudioClip defense = null;

    void Start()
    {
        // Audio
        monsterAudio = gameObject.GetComponent<AudioSource>();

        // Set Monster Fin(Default) Pos
        string monster = GameManager.instance.btlMoster;

        mstFinPos[(int)EMONSTER.LEFT] = new Vector3(1.86f, GameManager.instance.curMoster[monster].m_monsterPosY, -1.19f);
        mstFinPos[(int)EMONSTER.RIGHT] = new Vector3(-1.28f, GameManager.instance.curMoster[monster].m_monsterPosY, -3.29f);

        // Set Monster Battle Start Pos
        mstPos[(int)EMONSTER.LEFT] = new Vector3(1.86f, 0f, -5.29f);
        mstPos[(int)EMONSTER.RIGHT] = new Vector3(-1.28f, 0f, -5.29f);

        // Instantiate Monster
        mstObj[(int)EMONSTER.LEFT] = Instantiate(GameManager.instance.curMoster[monster].m_monsterPrefeb, mstPos[(int)EMONSTER.LEFT], Quaternion.identity);
        mstObj[(int)EMONSTER.RIGHT] = Instantiate(GameManager.instance.curMoster[monster].m_monsterPrefeb, mstPos[(int)EMONSTER.RIGHT], Quaternion.identity);
        mstObj[(int)EMONSTER.LEFT].SetActive(true);
        mstObj[(int)EMONSTER.RIGHT].SetActive(true);

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

        powerUpParticle[(int)EMONSTER.LEFT] = mstObj[(int)EMONSTER.LEFT].transform.GetChild(4).gameObject;
        powerUpParticle[(int)EMONSTER.RIGHT] = mstObj[(int)EMONSTER.RIGHT].transform.GetChild(4).gameObject;

        // power Up Init
        powerUpCnt[(int)EMONSTER.LEFT] = 1;
        powerUpCnt[(int)EMONSTER.RIGHT] = 1;
    }

    void Update()
    {
        CheckDead();
        AttackToWho();

        if (btlManager.status == 6 && !isWin)
        {
            isWin = true;
            UpdateInfo();
        }

        if (btlManager.curTurn == 0)
        {
            ResetPos();
        }
        else if (btlManager.status == 0 && btlManager.curTurn == 1) // Attack
        {
            AttackMove();
            if (!isAttack)
            {
                atkOrPu = Random.Range(0, 3);
                if (atkOrPu == (int)EATTACK.ATTACK || atkOrPu == (int)EATTACK.SPECIAL)
                {
                    Attack();
                }
                else if (atkOrPu == (int)EATTACK.POWER_UP)
                {
                    PowerUp();
                }
            }
        }

        if (isTiming)
        {
            CheckActionCommand((int)attackTo); // Call Player Check Action Command Func
        }
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
        mstTrans[btlManager.curMstTurn].position = Vector3.Lerp(mstTrans[btlManager.curMstTurn].position, btlManager.plrPos[(int)attackTo] - new Vector3(0f, 0f, 2f), Time.deltaTime * fowardSpeed);
    }

    void Attack()
    {
        isAttack = true;
        btlManager.CheckMstDead();
        StartCoroutine(AttackMotion(attackTo));
    }

    void PowerUp()
    {
        isAttack = true;
        powerUpCnt[btlManager.curMstTurn]++;
        StartCoroutine(PowerUpMotion());
    }

    // Call from CToadAssist
    public void ShowRockCandyAttack(int _p)
    {
        animator[_p].SetBool("isAttacked", true);
        attackedParticle[_p].SetActive(true);
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
            if (dam < 0)
            {
                dam = 0;
            }
            GameManager.instance.members[(int)attackTo].m_curhp -= (dam * powerUpCnt[btlManager.curMstTurn]);
            StartCoroutine(UpdateBarAnim((int)attackTo, (dam * powerUpCnt[btlManager.curMstTurn])));
        }
        else
        {
            int dam = GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk;
            if (dam < 0)
            {
                dam = 0;
            }
            GameManager.instance.members[(int)attackTo].m_curhp -= (dam * powerUpCnt[btlManager.curMstTurn]);
            StartCoroutine(UpdateBarAnim((int)attackTo, (dam * powerUpCnt[btlManager.curMstTurn])));
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
        yield return new WaitForSeconds(0.5f);
        mstObj[_p].SetActive(false);
        deadParticle[_p].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        coinParticle[_p].SetActive(true);
    }

    IEnumerator AttackMotion(EMEMBER _m)
    {
        btlManager.CheckMstDead();
        if(btlManager.status != 6 && btlManager.status != 7 && btlManager.status != 8)
        {
            if (atkOrPu == (int)EATTACK.ATTACK)
            {
                baseTxt.text = "해머펀치";
                atkBase.SetActive(true);

                yield return new WaitForSeconds(0.3f); // run to character

                animator[btlManager.curMstTurn].SetBool("isAttack", true); // Play Attack Motion

                monsterAudio.clip = hammer;
                monsterAudio.Play();
            }
            else if (atkOrPu == (int)EATTACK.SPECIAL)
            {
                baseTxt.text = "해머대시";
                atkBase.SetActive(true);

                yield return new WaitForSeconds(0.3f); // run to character

                animator[btlManager.curMstTurn].SetBool("isSpecial", true); // Play Attack Motion

                yield return new WaitForSeconds(0.3f);

                monsterAudio.clip = hammerDash;
                monsterAudio.Play();
            }

            yield return new WaitForSeconds(0.3f);

            player[(int)_m].timingIcon.SetActive(true);
            isTiming = true;
            // Wait for Player Check Action Command Func

            yield return new WaitForSeconds(0.3f);

            isTiming = false;

            if (isScAtkCmd) // Check Action Command
            {
                int dam = (GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk * powerUpCnt[btlManager.curMstTurn]) - GameManager.instance.members[(int)_m].m_stat.m_defense;
                if (dam < 0)
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
                printNum.PrintNum((GameManager.instance.monStats[GameManager.instance.btlMoster].m_atk * powerUpCnt[btlManager.curMstTurn]), (int)_m + 2); // print Damage Num
                player[(int)_m].ShowHitAnim();

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(0.3f);

            animator[btlManager.curMstTurn].SetBool("isAttack", false);
            animator[btlManager.curMstTurn].SetBool("isSpecial", false);

            btlManager.status = 1; // Turn to Show Command Menu
            btlManager.curTurn = 0; // Turn to Playable
            isAttack = false;

            player[(int)_m].StopHitAnim();
            player[(int)_m].StopDFActionCommand();
            player[(int)_m].timingIcon.SetActive(false);

            atkBase.SetActive(false);
            UpdatePlrHP();
            CheckMonsterTurn();
            isScAtkCmd = false;
        }
    }

    IEnumerator PowerUpMotion()
    {
        btlManager.CheckMstDead();
        if(btlManager.status != 6 && btlManager.status != 7 && btlManager.status != 8)
        {
            baseTxt.text = "근성업";
            atkBase.SetActive(true);

            powerUpParticle[btlManager.curMstTurn].SetActive(true);

            monsterAudio.clip = powerUp;
            monsterAudio.Play();

            yield return new WaitForSeconds(2.0f);

            powerUpParticle[btlManager.curMstTurn].SetActive(false);

            btlManager.status = 1; // Turn to Show Command Menu
            btlManager.curTurn = 0; // Turn to Playable
            isAttack = false;
            atkBase.SetActive(false);
            CheckMonsterTurn();
        }
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
                monsterAudio.clip = defense;
                monsterAudio.Play();

                combo.SetGauge10();
                GameManager.instance.gauge += 7; // defense Action Command Update
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
            yield return new WaitForSeconds(0.01f);
        }

        yield break;
    }
}
