using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSpecialMenu : MonoBehaviour
{
    public CBattleManager btlManager = null;
    public GameObject[] specials = new GameObject[2];

    public GameObject cursor = null;
    private Animator cursorAnim = null;

    // For special info text
    public Text[] info = new Text[3];
    public Text neededFP = null;
    public Text curFP = null;

    // Set Text
    public Text[] names = new Text[2];
    public Text[] costs = new Text[2];

    private Animator animator = null;
    private int specialNum = 2;
    private Transform trans = null;

    // cur Magic Attack Index
    private int magicAttackIndex = 0;

    // For special info text
    readonly public struct InfoText
    {
        public readonly string m_s1;
        public readonly string m_s2;
        public readonly string m_s3;

        public InfoText(string _s1, string _s2, string _s3)
        {
            m_s1 = _s1;
            m_s2 = _s2;
            m_s3 = _s3;
        }

        public string this[int index]
        {
            get
            {
                if (index == 0)
                    return m_s1;
                else if (index == 1)
                    return m_s2;
                else if (index == 2)
                    return m_s3;
                else
                    return null;
            }
        }
    }

    private Dictionary<int, InfoText> fstInfoText = new Dictionary<int, InfoText>();
    private Dictionary<int, InfoText> scdInfoText = new Dictionary<int, InfoText>();

    // Audio
    private AudioSource specialMenuAudio = null;
    public AudioClip pass = null;
    public AudioClip pressed = null;

    // hide Right Name
    public GameObject rightName = null;

    void Start()
    {
        // For special info text
        fstInfoText[0] = new InfoText("적을 밟을 수 있다.", "밟기 직전에 K를 누르면", "추가 대미지를 줄 수 있다."); // mario fst Info Text
        fstInfoText[1] = new InfoText("적 전체에게 번개로 공격해.", "공격이 끝나기 직전에", "K를 눌러 봐."); // mellow fst Info Text

        scdInfoText[0] = new InfoText("불덩어리를 던진다.", "K를 연타해서 불덩어리를 던져라!", ""); // mario scd Info Text
        scdInfoText[1] = new InfoText("몬스터의 남은 HP를 알 수 있어.", "K를 누르면 적의 생각까지...", ""); // mellow scd Info Text

        animator = gameObject.GetComponent<Animator>();
        cursorAnim = cursor.GetComponent<Animator>();
        trans = cursor.GetComponent<Transform>();

        specialMenuAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(btlManager.status == 4)
        {
            SetList();

            GetInput();
            ShowSpecialMenu();
            PressedSpecial();
        }
        else
        {
            InitMenu();
        }
    }

    void ShowSpecialMenu()
    {
        animator.SetBool("state", true); // show Special Menu
        cursorAnim.SetBool("MembersCursorStatus", true); // show cursor
    }

    void SetList()
    {
        for (int i = 0; i < specialNum; i++)
        {
            names[i].text = GameManager.instance.members[btlManager.curPlrTurn].m_magicAttack[i].m_name;
            costs[i].text = "" + GameManager.instance.members[btlManager.curPlrTurn].m_magicAttack[i].m_cost;
        }

        // Set Text
        for (int i = 0; i < GameManager.instance.members[btlManager.curPlrTurn].m_maIndex + 1; i++)
        {
            specials[i].SetActive(true);
        }
    }

    void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            specialMenuAudio.clip = pass;
            specialMenuAudio.Play();

            ShowInfoText(0);
            float posY = trans.position.y + 60f;
            if(posY > 953f)
            {
                posY = 953f;
            }

            magicAttackIndex = 0;

            trans.position = new Vector3(trans.position.x, posY, trans.position.x);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            specialMenuAudio.clip = pass;
            specialMenuAudio.Play();

            float posY = trans.position.y - 60f;
            if (posY < 953f - (60f * GameManager.instance.members[btlManager.curPlrTurn].m_maIndex) )
            {
                posY = 953f - (60f * GameManager.instance.members[btlManager.curPlrTurn].m_maIndex);

                ShowInfoText(0); // set first info

                magicAttackIndex = 0;
            }
            else
            {
                ShowInfoText(1);

                magicAttackIndex = 1;
            }
            trans.position = new Vector3(trans.position.x, posY, trans.position.x);
        }
    }

    void ShowInfoText(int _p)
    {
        if(_p == 0)
        {
            for (int i = 0; i < info.Length; i++)
            {
                info[i].text = fstInfoText[btlManager.curPlrTurn][i];
            }

            neededFP.text = "" + GameManager.instance.members[btlManager.curPlrTurn].m_magicAttack[0].m_cost;
        }
        else
        {
            for (int i = 0; i < info.Length; i++)
            {
                info[i].text = scdInfoText[btlManager.curPlrTurn][i];
            }

            neededFP.text = "" + GameManager.instance.members[btlManager.curPlrTurn].m_magicAttack[1].m_cost;
        }

        curFP.text = "" + GameManager.instance.curFP;
    }

    void PressedSpecial()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(CheckFP())
            {
                rightName.SetActive(false);
                btlManager.status = 400 + magicAttackIndex;
            }
        }
    }

    // Check Current FP and Needed FP, Update FP
    bool CheckFP()
    {
        if(GameManager.instance.curFP < GameManager.instance.members[btlManager.curPlrTurn].m_magicAttack[magicAttackIndex].m_cost)
        {
            return false;
        }
        else
        {
            specialMenuAudio.clip = pressed;
            specialMenuAudio.Play();

            GameManager.instance.curFP -= GameManager.instance.members[btlManager.curPlrTurn].m_magicAttack[magicAttackIndex].m_cost;
            return true;
        }
    }

    void InitMenu()
    {
        animator.SetBool("state", false);
        cursorAnim.SetBool("MembersCursorStatus", false);

        // Set Hide List
        for (int i = 0; i < GameManager.instance.members[btlManager.curPlrTurn].m_maIndex + 1; i++)
        {
            specials[i].SetActive(false);
        }

        // Init info List
        ShowInfoText(0);

        // Set Cursor Position
        trans.position = new Vector3(trans.position.x, 953f, trans.position.x);

        // Init Magic Attack Index
        magicAttackIndex = 0;
    }
}
