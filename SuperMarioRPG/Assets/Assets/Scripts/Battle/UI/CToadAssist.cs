using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CToadAssist : MonoBehaviour
{
    public CBattleManager btlManager = null;
    public Image gaugeImage = null;

    public GameObject waitButton = null;
    private Animator animator = null;

    public GameObject Box = null;
    private Transform boxTrans = null;
    private Animator boxAnim = null;

    // Items (0: rock candy, 1: mushroom, 2: flower)
    public GameObject[] toadItems = new GameObject[3];
    public GameObject cloudParticle = null;
    public GameObject toad = null;
    private int toadItmIdx = 0;

    // item info box
    public GameObject itemInfo = null;
    public Text itemInfoTxt = null;
    private Dictionary<int, string> itemInfoString = new Dictionary<int, string>();

    // particle (0: rock candy, 1: mushroom, 2: flower)
    public GameObject[] particles = new GameObject[3];

    // To Coroutine
    private bool isJump = false;
    private bool isParticleEnd = false;

    // To Monster Atk Motion (rock candy)
    public CMstControl monApr = null;
    public CPrintNum printNum = null;

    // HPBar
    public Image[] HPBar = new Image[2];

    // Audio
    private AudioSource comboAudio = null;
    public AudioClip box = null;
    public AudioClip cloud = null;
    public AudioClip rockCandy = null;
    public AudioClip mushroom = null;
    public AudioClip honeySyrup = null;

    void Start()
    {
        comboAudio = gameObject.GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        boxTrans = Box.GetComponent<Transform>();
        boxAnim = Box.GetComponent<Animator>();

        // set item info text
        itemInfoString[0] = "º°»çÅÁ";
        itemInfoString[1] = "ÆÄ¿öÇ® ¹ö¼¸";
        itemInfoString[2] = "ÇÃ¶ó¿ö ¿¢±â½º";
    }

    // Update is called once per frame
    void Update()
    {
        FullEff();

        ShowBox();
        BoxAnimation();
        ShowParticle();

        // hide when monster attack, win, loose
        if (btlManager.status != 0 && btlManager.status != 6 && btlManager.status != 7)
        {
            ShowButton();
        }
        else
        {
            HideButton();
        }
    }

    void ShowButton()
    {
        animator.SetBool("state", true);
    }

    void HideButton()
    {
        animator.SetBool("state", false);
    }

    void FullEff()
    {
        if(GameManager.instance.gauge >= 100)
        {
            waitButton.SetActive(false);
            PressedButton();
        }
        else
        {
            waitButton.SetActive(true);
        }
    }

    void PressedButton()
    {
        // Can Use in Default Menu
        if (btlManager.status == 1 && Input.GetKeyDown(KeyCode.Minus))
        {
            GameManager.instance.gauge = 0;
            waitButton.SetActive(true);
            StartCoroutine(ResetGauge());
        }

    }

    void ShowBox()
    {
        if(btlManager.status == 10)
        {
            Box.SetActive(true);
            toad.SetActive(true);
            StartCoroutine(BoxMotion());
        }
        else // Init
        {
            boxTrans.position = new Vector3(0.2f, 4f, 3.5f);
            btlManager.jumpCount = 0;
        }
    }

    void BoxAnimation()
    {
        if(btlManager.jumpCount == 1 && !isJump)
        {
            isJump = true;
            boxAnim.SetInteger("JumpState", 1);
            StartCoroutine(ShowItmes());
        }
        else if(btlManager.jumpCount == 2 && isJump)
        {
            comboAudio.loop = false;
            comboAudio.Stop();

            isJump = false;
            boxAnim.SetInteger("JumpState", 2);
            StartCoroutine(WaitClosedBox());
        }
    }

    void ShowParticle()
    {
        if (btlManager.status == 12 && !isParticleEnd)
        {
            isParticleEnd = true;
            StartCoroutine(UpdateInfo());
        }
    }

    void EndParticle()
    {
        isParticleEnd = false;
        btlManager.status = 0; // hide Command Menu
        btlManager.curTurn = 1; // set Status to 0 Cuz next turn is Monster Turn
        if(btlManager.curPlrTurn == 0) // pass player turn
        {
            btlManager.curPlrTurn = 1;
        }
        else
        {
            btlManager.curPlrTurn = 0;
        }
    }

    void MonsterHPCheck(int _p)
    {
        int dam = btlManager.monsterHp[_p] - 99;
        if (dam < 0)
        {
            dam = 0;
        }
        btlManager.monsterHp[_p] = dam; // Upadate Monster Hp
    }

    void PlaySound()
    {
        if (toadItmIdx == 0) // rock candy
        {
            comboAudio.clip = rockCandy;
            comboAudio.Play();
        }
        else if (toadItmIdx == 1) // mushroom
        {
            comboAudio.clip = mushroom;
            comboAudio.Play();
        }
        else if (toadItmIdx == 2) // flower essence
        {
            comboAudio.clip = honeySyrup;
            comboAudio.Play();
        }
    }

    IEnumerator UpdateInfo()
    {
        PlaySound();

        particles[toadItmIdx].SetActive(true);
        if (GameManager.instance.memberIndex == EMEMBER.MELLOW)
        {
            particles[toadItmIdx].GetComponent<Transform>().transform.GetChild(1).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2.0f);

        if(toadItmIdx == 0) // rock candy
        {
            printNum.PrintRockCandy(); // Print Damage Num

            if(!btlManager.isLeftMstDead)
            {
                monApr.ShowRockCandyAttack(0); // Monster Attacked animation
                MonsterHPCheck(0);
            }
            if (!btlManager.isRightMstDead)
            {
                monApr.ShowRockCandyAttack(1); // Monster Attacked animation
                MonsterHPCheck(1);
            }

            yield return new WaitForSeconds(0.3f);
        }
        else if (toadItmIdx == 1) // mushroom
        {
            if (!btlManager.isMarioDead)
            {
                StartCoroutine(UpdateBarAnim((int)EMEMBER.MARIO));
                GameManager.instance.members[(int)EMEMBER.MARIO].m_curhp = GameManager.instance.members[(int)EMEMBER.MARIO].m_maxhp;
            }
            if(!btlManager.isMellowDead)
            {
                StartCoroutine(UpdateBarAnim((int)EMEMBER.MELLOW));
                GameManager.instance.members[(int)EMEMBER.MELLOW].m_curhp = GameManager.instance.members[(int)EMEMBER.MELLOW].m_maxhp;
            }
        }
        else if (toadItmIdx == 2) // flower essence
        {
            GameManager.instance.curFP = GameManager.instance.maxFP;
        }

        yield return new WaitForSeconds(0.8f);

        particles[toadItmIdx].SetActive(false);
        particles[toadItmIdx].GetComponent<Transform>().transform.GetChild(1).gameObject.SetActive(false);

        monApr.SetMonState();
        itemInfo.SetActive(false);
        EndParticle();

    }

    IEnumerator UpdateBarAnim(int _w)
    {
        for (int i = 0; i < GameManager.instance.members[_w].m_maxhp; i++)
        {
            HPBar[_w].fillAmount += 1f / (float)GameManager.instance.members[_w].m_maxhp;
            yield return new WaitForSeconds(0.005f);
        }

        yield break;
    }

    IEnumerator ShowItmes()
    {
        comboAudio.clip = box;
        comboAudio.loop = true;
        comboAudio.Play();

        while (btlManager.jumpCount == 1)
        {
            toadItmIdx++;

            if (toadItmIdx == 3)
            {
                toadItmIdx = 0;
            }

            toadItems[toadItmIdx].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            toadItems[toadItmIdx].SetActive(false);
        }
        toadItems[toadItmIdx].SetActive(true);

        yield break;
    }

    IEnumerator WaitClosedBox()
    {
        yield return new WaitForSeconds(1.5f);

        btlManager.jumpCount = 3;
        boxAnim.SetInteger("JumpState", 3);
        toadItems[toadItmIdx].SetActive(false);

        btlManager.status = 11; // change btl Status

        Box.SetActive(false);
        cloudParticle.SetActive(true);

        comboAudio.clip = cloud;
        comboAudio.Play();

        itemInfoTxt.text = itemInfoString[toadItmIdx];
        itemInfo.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        cloudParticle.SetActive(false);
        btlManager.status = 12;
        toad.SetActive(false);
    }

    IEnumerator ResetGauge()
    {
        for (int i = 0; i < 100; i++)
        {
            gaugeImage.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.005f);
        }

        btlManager.status = 10;

        yield break;
    }

    IEnumerator BoxMotion()
    {
        Transform trans = Box.GetComponent<Transform>();
        float sumTime = 0f;
        float TotalTime = 50.0f;

        while (sumTime <= TotalTime)
        {
            trans.position = Vector3.Lerp(boxTrans.position, new Vector3(0.2f, 2.5f, 3.5f), sumTime / TotalTime);

            sumTime += Time.deltaTime;


            yield return null;
        }

        yield break;
    }
}
