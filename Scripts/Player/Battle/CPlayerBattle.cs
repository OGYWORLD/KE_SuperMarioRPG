using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPlayerBattle : MonoBehaviour
{
    #region public
    public CBattleManager btlManager = null;
    public CMstControl monApr = null;
    public CPlayerValue playerValues = null;
    public GameObject timingIcon = null;
    public CPrintNum printNum = null;

    // HPBar
    public Image[] HPBar = new Image[2];
    #endregion

    // timing Icon
    public bool isTiming { get; set; } = false;

    // Playable Character
    private Animator animator = null;
    private Transform trans = null;

    public bool isScAtkCmd { get; set; } = false;
    private float fowardSpeed = 7f;


    // Particle
    private GameObject hitParticle = null;
    public GameObject defenseParticle { get; set; } = null;
    public GameObject[] thunderParticle = new GameObject[2];
    public GameObject[] itemParticle = new GameObject[5]; // cloud, mushroom, potion, blue, red

    private Quaternion plrWatchPlayer = Quaternion.Euler(-9f, 315f, 0f);
    private Quaternion plrWatchMon = Quaternion.Euler(0f, 180f, 0f);
    private Vector3 originPos;
    private Vector3[] monPos;

    // Combo Gauge
    public CComboGauge combo = null;

    void Start()
    {
        monPos = new Vector3[2];
        monPos[0] = playerValues.monPos[0];
        monPos[1] = playerValues.monPos[1];

        hitParticle = transform.GetChild(3).gameObject;
        defenseParticle = transform.GetChild(4).gameObject;

        trans = gameObject.GetComponent<Transform>();

        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isBtl", GameManager.instance.isNowBattle);
    }

    void Update()
    {
        CheckDead();

        if (btlManager.status != 0 && btlManager.curTurn == 0 && btlManager.curPlrTurn == playerValues.Me && btlManager.status != 7)
        {
            MainMenuTurn();
        }
        if (btlManager.status >= 20 && btlManager.status < 30 && btlManager.curTurn == 0 && btlManager.curPlrTurn == playerValues.Me) // Default Attack
        {
            AttackTurn();
            if (!btlManager.isAttacked)
            {
                Attack();
            }
        }
        if (btlManager.status >= 40 && btlManager.status < 50 && btlManager.curTurn == 0 && btlManager.curPlrTurn == playerValues.Me) // magic Attack
        {
            AttackTurn();
            if (!btlManager.isAttacked)
            {
                MagicAttack();
            }
        }
        if (btlManager.status >= 50 && btlManager.status < 60 && btlManager.curPlrTurn == playerValues.Me) // item
        {
            AttackTurn();
            if (!btlManager.isAttacked)
            {
                UseItem();
            }
        }
        if (btlManager.status == 10) // Toad Assist
        {
            AttackTurn();
        }

        // Win!
        if (btlManager.status == 6)
        {
            // Reset HP if Die Someone in Battle
            if (btlManager.isMarioDead && playerValues.Me == 0)
            {
                GameManager.instance.members[0].m_curhp = GameManager.instance.members[0].m_maxhp;
            }
            if (btlManager.isMellowDead && playerValues.Me == 1)
            {
                GameManager.instance.members[1].m_curhp = GameManager.instance.members[1].m_maxhp;
            }

            MainMenuTurn();
            animator.SetBool("isDead", false);
            animator.SetInteger("status", btlManager.status);
        }

        CheckActionCommand();

    }

    void MainMenuTurn()
    {
        if (btlManager.status < 10)
        {
            trans.rotation = Quaternion.Lerp(trans.rotation, plrWatchPlayer, 0.4f);
        }
    }

    IEnumerator TurnBack()
    {
        originPos = btlManager.plrPos[playerValues.Me];

        float sumTime = 0f;

        while (sumTime <= 0.4f)
        {
            trans.position = Vector3.Lerp(trans.position, originPos, Time.deltaTime * fowardSpeed);

            sumTime += Time.deltaTime;

            yield return null;
        }
        yield break;
    }

    IEnumerator AttackMove()
    {
        float sumTime = 0f;

        while (sumTime <= 0.5f)
        {
            trans.position = Vector3.Lerp(trans.position, monPos[btlManager.curAttack], sumTime / 0.5f);

            sumTime += Time.deltaTime;

            yield return null;
        }
        yield break;
    }

    void AttackTurn()
    {
        trans.rotation = Quaternion.Lerp(trans.rotation, plrWatchMon, 0.2f);
    }

    void Attack()
    {
        btlManager.isAttacked = true;
        isTiming = true;

        StartCoroutine(AttackMotion());
    }

    IEnumerator AttackMotion()
    {
        StartCoroutine(AttackMove());

        yield return new WaitForSeconds(0.5f); // run to monster

        animator.SetInteger("status", btlManager.status); // set attack animation

        yield return new WaitForSeconds(0.3f); // show attack animation

        monApr.ShowAttackedMon(); // Monster Attacked animation

        timingIcon.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        // check action command

        yield return new WaitForSeconds(0.2f);
        isTiming = false;

        if (isScAtkCmd)
        {
            printNum.PrintNum(GameManager.instance.members[btlManager.curPlrTurn].m_stat.m_attak * 2, btlManager.curAttack); // print Damage Num
            SuccessActionCommand();
            monApr.ShowACParticle(); // Show Monster ActionCommand Particle
            yield return new WaitForSeconds(1.0f);
            monApr.HideACParticle(); // Hide Monster ActionCommand Particle
        }
        else
        {
            printNum.PrintNum(GameManager.instance.members[btlManager.curPlrTurn].m_stat.m_attak, btlManager.curAttack); // print Damage Num

            yield return new WaitForSeconds(0.3f);
        }

        StartCoroutine(TurnBack());

        yield return new WaitForSeconds(0.5f);

        timingIcon.SetActive(false);
        updateMonsterHP();
        EndAttck();
        animator.SetInteger("status", btlManager.status);
        animator.SetBool("isAtk", false);
        monApr.SetMonState(); // End Monster Attacked animation
    }
    public void CheckActionCommand()
    {
        if (isTiming)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                combo.SetGauge10();
                GameManager.instance.gauge += 3; // attack Action Command Update
                combo.updateGaugeImage(3);

                isScAtkCmd = true;
                isTiming = false;
            }
        }
    }

    public void SuccessActionCommand()
    {
        animator.SetBool("isAtk", isScAtkCmd);
    }

    public void SuccessDfActionCommand()
    {
        animator.SetBool("isDefense", true);
    }

    public void StopDFActionCommand()
    {
        animator.SetBool("isDefense", false);
    }

    void EndAttck()
    {
        btlManager.status = 0; // hide Command Menu
        btlManager.curTurn = 1; // set Status to 0 Cuz next turn is Monster Turn
        btlManager.curPlrTurn = playerValues.You; // Pass Turn To another playable
        isScAtkCmd = false;
        btlManager.isAttacked = false;
        isTiming = false;
    }

    void updateMonsterHP()
    {
        int _mult;
        _mult = isScAtkCmd ? 2 : 1;

        // Default Attack
        if (btlManager.status / 10 == 2)
        {
            btlManager.monsterHp[btlManager.curAttack] -= GameManager.instance.members[btlManager.curPlrTurn].m_stat.m_attak * _mult; // Upadate Monster Hp
        }
        else if (btlManager.status / 10 == 3) // Magic Attack
        {
            btlManager.monsterHp[btlManager.curAttack] -= GameManager.instance.members[btlManager.curPlrTurn].m_magicAttack[btlManager.status % 10].m_attack * _mult; // Upadate Monster Hp
        }
    }

    // Call From CMstControl
    public void ShowHitAnim()
    {
        animator.SetBool("isAttacked", true);
        hitParticle.SetActive(true);
    }
    // Call From CMstControl
    public void StopHitAnim()
    {
        animator.SetBool("isAttacked", false);
        hitParticle.SetActive(false);
    }

    void CheckDead()
    {
        if (playerValues.Me == 0 && btlManager.isMarioDead)
        {
            animator.SetBool("isDead", true);
        }
        else if (playerValues.Me == 1 && btlManager.isMellowDead)
        {
            animator.SetBool("isDead", true);
        }
    }

    /* For Magic Attack */
    void MagicAttack()
    {
        btlManager.isAttacked = true;
        isTiming = true;

        StartCoroutine(MagicAttackMotion(playerValues.Me));
    }

    // Magic Attack Intro Motion
    void MagicAttackIntro()
    {
        // Mario's Jump has to move to Monster
        if (playerValues.Me == (int)EMEMBER.MARIO && btlManager.status == 40)
        {
            StartCoroutine(MarioJump());
        }
        if (playerValues.Me == (int)EMEMBER.MELLOW)
        {
            // mellow magic attack
            StartCoroutine(MellowThunder());
        }
    }

    IEnumerator MagicAttackMotion(float _t)
    {
        animator.SetInteger("status", btlManager.status);

        MagicAttackIntro();

        yield return new WaitForSeconds(0.2f + (_t * 2)); // for padding time to mellow's magic attack

        timingIcon.SetActive(true);

        // check action command

        yield return new WaitForSeconds(0.2f);
        isTiming = false;
        animator.SetInteger("status", 0);
        if (isScAtkCmd)
        {
            monApr.ShowAttackedMon();
            printNum.PrintNum(GameManager.instance.members[btlManager.curPlrTurn].m_magicAttack[btlManager.status % 10].m_attack, btlManager.curAttack); // print Damage Num
            monApr.ShowACParticle(); // Show Monster ActionCommand Particle
            yield return new WaitForSeconds(1.0f);
            monApr.HideACParticle(); // Hide Monster ActionCommand Particle
        }
        else
        {
            monApr.ShowAttackedMon();
            printNum.PrintNum(GameManager.instance.members[btlManager.curPlrTurn].m_stat.m_attak, btlManager.curAttack); // print Damage Num

            yield return new WaitForSeconds(0.3f);
        }

        StartCoroutine(TurnBack());

        yield return new WaitForSeconds(0.5f);

        timingIcon.SetActive(false);

        updateMonsterHP();
        EndAttck();
        thunderParticle[0].SetActive(false);
        thunderParticle[1].SetActive(false);
        animator.SetInteger("status", btlManager.status);
        animator.SetBool("isAtk", false);
        monApr.SetMonState(); // End Monster Attacked animation
    }

    IEnumerator MarioJump()
    {
        string monster = GameManager.instance.btlMoster;
        Vector3 startPos = new Vector3(monPos[btlManager.curAttack].x, GameManager.instance.curMoster[monster].m_monsterPosY + 5f, monPos[btlManager.curAttack].z - 1f);
        Vector3 targetPos = new Vector3(monPos[btlManager.curAttack].x, GameManager.instance.curMoster[monster].m_monsterPosY + 1f, monPos[btlManager.curAttack].z - 1f);
        float sumTime = 0f;

        while (sumTime <= 0.4f)
        {
            trans.position = Vector3.Lerp(startPos, targetPos, sumTime / 0.4f);

            sumTime += Time.deltaTime;

            yield return null;
        }
        yield break;
    }

    IEnumerator MellowThunder()
    {
        thunderParticle[0].SetActive(true);
        yield return new WaitForSeconds(1.0f);
        thunderParticle[1].SetActive(true);
    }

    void UseItem()
    {
        btlManager.isAttacked = true;

        StartCoroutine(ShowItemUse());
    }

    IEnumerator ShowItemUse()
    {
        int curItem = btlManager.status % 10 + 1;

        // mushroom or honey syrup
        itemParticle[curItem].GetComponent<Transform>().position = btlManager.plrPos[btlManager.curPlrTurn] + new Vector3(0f, 2f, 0f);
        itemParticle[curItem].SetActive(true);

        yield return new WaitForSeconds(0.5f);
        itemParticle[curItem].SetActive(false);

        // cloud
        itemParticle[0].GetComponent<Transform>().position = btlManager.plrPos[btlManager.curPlrTurn] + new Vector3(0f, 2f, 0f);
        itemParticle[0].SetActive(true);

        yield return new WaitForSeconds(1.0f);

        // twinkle effect
        if((btlManager.status % 10) == (int)EITEMS.MUSHROOM)
        {
            itemParticle[curItem + 2].GetComponent<Transform>().position = btlManager.plrPos[(int)btlManager.whoUseMushroom] + new Vector3(0f, 0.5f, 0f);
        }
        else
        {
            itemParticle[curItem + 2].GetComponent<Transform>().position = btlManager.plrPos[btlManager.curPlrTurn] + new Vector3(0f, 0.5f, 0f);
        }
        itemParticle[curItem + 2].SetActive(true);

        yield return new WaitForSeconds(1.0f);

        useItemUpdate();

        yield return new WaitForSeconds(0.5f);

        EndAttck();
        animator.SetInteger("status", btlManager.status);
        itemParticle[0].SetActive(false);
        itemParticle[curItem + 1].SetActive(false);
        itemParticle[curItem + 2].SetActive(false);
    }

    void useItemUpdate()
    {
        if(btlManager.status == 50) // Mushroom
        {
            StartCoroutine(UpdateBarAnim(30));

            GameManager.instance.members[(int)btlManager.whoUseMushroom].m_curhp += 30;
            if(GameManager.instance.members[(int)btlManager.whoUseMushroom].m_curhp > GameManager.instance.members[(int)btlManager.whoUseMushroom].m_maxhp)
            {
                GameManager.instance.members[(int)btlManager.whoUseMushroom].m_curhp = GameManager.instance.members[(int)btlManager.whoUseMushroom].m_maxhp;
            }
        }
        else if(btlManager.status == 51) // Honey Syrup
        {
            GameManager.instance.curFP += 10;
            if (GameManager.instance.curFP > GameManager.instance.maxFP)
            {
                GameManager.instance.curFP = GameManager.instance.maxFP;
            }
        }
    }

    IEnumerator UpdateBarAnim(int _n)
    {
        for (int i = 0; i < _n; i++)
        {
            HPBar[(int)btlManager.whoUseMushroom].fillAmount += 1f / (float)GameManager.instance.members[(int)btlManager.whoUseMushroom].m_maxhp;
            yield return new WaitForSeconds(0.005f);
        }

        btlManager.whoUseMushroom = EMEMBER.MARIO;

        yield break;
    }
}
