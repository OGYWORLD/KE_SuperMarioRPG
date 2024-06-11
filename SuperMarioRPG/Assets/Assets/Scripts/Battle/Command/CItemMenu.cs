using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CItemMenu : MonoBehaviour
{
    public CBattleManager btlManager = null;
    public GameObject[] items = new GameObject[2];

    public GameObject cursor = null;
    private Animator cursorAnim = null;
    private Animator animator = null;
    private Transform trans = null;

    // To get which item has
    private SortedSet<int> hasItem = new SortedSet<int>();
    private int firstItemIndex = 0;
    private int secondItemIndex = 0;

    // item index will use
    public int itemIndex { get; set; } = 0;
    private bool isEmpty = true;

    // info text
    public Text[] itemNames = new Text[2];
    public Text[] itemCnt = new Text[2];
    public Text itemInfo;

    // cursor to select character
    public GameObject[] marioFingerCursor = new GameObject[2];
    public GameObject mellowFingerCursor = null;

    // For item info text
    readonly public struct InfoText
    {
        public readonly int m_index;

        public readonly string m_name;

        public readonly int m_cnt;

        public readonly string m_s;

        public InfoText(int _i, string _n, int _c, string _s)
        {
            m_index = _i;
            m_name = _n;
            m_cnt = _c;
            m_s = _s;
        }

        public object this[int index]
        {
            get
            {
                if (index == 0)
                    return m_index;
                else if (index == 1)
                    return m_name;
                else if (index == 2)
                    return m_cnt;
                else if (index == 3)
                    return m_s;
                else
                    return null;
            }
        }
    }

    private Dictionary<int, InfoText> itemInfoText = new Dictionary<int, InfoText>();

    // Audio
    private AudioSource itemMenuAudio = null;
    public AudioClip pass = null;
    public AudioClip pressed = null;

    void Start()
    {
        itemMenuAudio = gameObject.GetComponent<AudioSource>();

        animator = gameObject.GetComponent<Animator>();
        cursorAnim = cursor.GetComponent<Animator>();
        trans = cursor.GetComponent<Transform>();

        // Set Item Info Text
        itemInfoText[(int)EITEMS.MUSHROOM] = new InfoText(0, "버섯", 0, "HP 30을 회복합니다.");
        itemInfoText[(int)EITEMS.HONEY_SYRUP] = new InfoText(0, "허니 시럽", 0, "FP 10을 회복합니다.");
    }

    void Update()
    {
        if(btlManager.status == 5)
        {
            SetList();
            if (!isEmpty)
            {
                SetHasItem();
                ShowSpecialMenu();
                GetInput();
                PressedItem();
            }
            else
            {
                NoItem();
            }
        }
        else if(btlManager.status == -50) // mushroom fingerCursor
        {
            ShowFingerCursor();
        }
        else if(btlManager.status != 50 && btlManager.status != 51)
        {
            InitMenu();
        }

        if (btlManager.status != 5)
        {
            animator.SetBool("state", false);
            cursorAnim.SetBool("MembersCursorStatus", false);
        }
    }

    void ShowSpecialMenu()
    {
        animator.SetBool("state", true); // show Special Menu
        cursorAnim.SetBool("MembersCursorStatus", true); // show cursor
    }

    void SetList()
    {
        int index = 0;
        for (int i = 0; i < GameManager.instance.m_items.Count; i++)
        {
            // Check be set on list item
            if(GameManager.instance.m_items[i] != 0)
            {
                itemNames[index].text = itemInfoText[i].m_name;
                itemCnt[index].text = "" + GameManager.instance.m_items[i];

                items[index].SetActive(true);
                
                hasItem.Add(i);
                isEmpty = false;
                index++;
            }
        }
    }

    void PressedItem()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            itemMenuAudio.clip = pressed;
            itemMenuAudio.Play();

            if (itemIndex == (int)EITEMS.MUSHROOM)
           {
                marioFingerCursor[(int)GameManager.instance.memberIndex].SetActive(true);
                btlManager.status = -(50 + itemIndex); // -50 is Mushroom index for fingerCursor
                GameManager.instance.m_items[(int)EITEMS.MUSHROOM]--;
           }
           else
           {
                btlManager.status = 50 + itemIndex;
                GameManager.instance.m_items[(int)EITEMS.HONEY_SYRUP]--;
           }
        }
    }

    void ShowFingerCursor()
    {
        // only Mario
        if(GameManager.instance.memberIndex == EMEMBER.MARIO)
        {
            marioFingerCursor[(int)GameManager.instance.memberIndex].SetActive(true);
            btlManager.whoUseMushroom = EMEMBER.MARIO;
        }
        else if (GameManager.instance.memberIndex == EMEMBER.MELLOW) // with Mellow
        {
            if(btlManager.isMarioDead)
            {
                marioFingerCursor[(int)GameManager.instance.memberIndex].SetActive(false);
                mellowFingerCursor.SetActive(true);
                btlManager.whoUseMushroom = EMEMBER.MELLOW;
            }
            else if(btlManager.isMellowDead)
            {
                marioFingerCursor[(int)GameManager.instance.memberIndex].SetActive(true);
                mellowFingerCursor.SetActive(false);
                btlManager.whoUseMushroom = EMEMBER.MARIO;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow) && !btlManager.isMellowDead)
            {
                itemMenuAudio.clip = pass;
                itemMenuAudio.Play();

                marioFingerCursor[(int)GameManager.instance.memberIndex].SetActive(false);
                mellowFingerCursor.SetActive(true);
                btlManager.whoUseMushroom = EMEMBER.MELLOW;
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow) && !btlManager.isMarioDead)
            {
                itemMenuAudio.clip = pass;
                itemMenuAudio.Play();

                marioFingerCursor[(int)GameManager.instance.memberIndex].SetActive(true);
                mellowFingerCursor.SetActive(false);
                btlManager.whoUseMushroom = EMEMBER.MARIO;
            }
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            itemMenuAudio.clip = pressed;
            itemMenuAudio.Play();

            btlManager.status = 50;
            marioFingerCursor[(int)GameManager.instance.memberIndex].SetActive(false);
            mellowFingerCursor.SetActive(false);
        }
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            itemMenuAudio.clip = pass;
            itemMenuAudio.Play();

            itemInfo.text = itemInfoText[firstItemIndex].m_s;

            float posY = trans.position.y + 60f;
            if (posY > 953f)
            {
                posY = 953f;
            }

            itemIndex = firstItemIndex;

            trans.position = new Vector3(trans.position.x, posY, trans.position.x);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            itemMenuAudio.clip = pass;
            itemMenuAudio.Play();

            float posY = trans.position.y - 60f;
            if (posY < 953f - (60f * (hasItem.Count-1)))
            {
                posY = 953f - (60f * (hasItem.Count - 1));

                itemInfo.text = itemInfoText[firstItemIndex].m_s; // set first info

                itemIndex = firstItemIndex;
            }
            else
            {
                itemInfo.text = itemInfoText[secondItemIndex].m_s;

                itemIndex = secondItemIndex;
            }
            trans.position = new Vector3(trans.position.x, posY, trans.position.x);
        }
    }

    void InitMenu()
    {
        for (int i = 0; i < GameManager.instance.m_items.Count; i++)
        {
            items[i].SetActive(false);
        }

        itemInfo.text = itemInfoText[0].m_s;

        trans.position = new Vector3(trans.position.x, 953f, trans.position.x);

        isEmpty = true;

        SetList();
        if(hasItem.Count == 2)
        {
            itemIndex = 0;
        }

        hasItem.Clear();
    }

    void SetHasItem()
    {
        int[] ar = new int[2];

        int index = 0;
        foreach (int i in hasItem)
        {
            ar[index] = i;

            index++;
        }

        if(hasItem.Count == 1)
        {
            itemIndex = ar[0];
            firstItemIndex = ar[0];
            secondItemIndex = ar[0];
            itemInfo.text = itemInfoText[firstItemIndex].m_s;
        }
        else
        {
            firstItemIndex = ar[0];
            secondItemIndex = ar[1];
        }
    }

    void NoItem()
    {
        animator.SetBool("state", true);
        itemInfo.text = "아이템이 없습니다.";
    }
}
