using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHammerDia : MonoBehaviour
{
    public CMarioMove marioMove = null;
    public Animator marioAnim = null;
    public CBossBtlTrans btlTrans = null;

    public Text[] dialog = new Text[3];

    public GameObject diaName = null;

    public GameObject msgWindow = null;
    public GameObject Info = null;
    public GameObject toad = null;
    public GameObject hammer = null;
    public GameObject cloud = null;

    public int status { get; set; } = 0;

    // Audio
    private AudioSource talkAudio = null;

    public GameObject getItem = null;
    public AudioClip cloudSound = null;

    void Start()
    {
        talkAudio = gameObject.GetComponent<AudioSource>();

        // set mario move to can't move
        marioMove.isTalk = true;

        // set mario anim
        marioAnim.SetBool("isWalk", false);
        marioAnim.SetBool("isJump", false);

        dialog[0].text = "여기를 지나가고 싶다고?";
        dialog[1].text = "오른손에 쥔 해머 보이지?";
        dialog[2].text = "곱게 지나가기는 힘들 거다!";
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isHammerBroDead)
        {
            GetInput();
        }
        else
        {
            ShowText();
            GetHammerInput();
        }

    }

    void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            talkAudio.Play();

            if (status == 0)
            {
                diaName.SetActive(true);

                dialog[0].text = "마리오 님!";
                dialog[1].text = "왼손에 잡힌 저도 보이시죠?";
                dialog[2].text = "이 녀석, 정말 강해요...";
            }
            else if (status == 1)
            {
                diaName.SetActive(false);

                dialog[0].text = "나랑 한판 하려고?";
                dialog[1].text = "상대해 주마!!";
                dialog[2].text = "";
            }
            else if (status == 2)
            {
                marioMove.isTouched = true;
                btlTrans.BtlTrans("hammerBro", "btld02a_Boss");
            }

            status++;
        }
    }

    void GetHammerInput()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            talkAudio.Play();
            status++;
        }
    }

    void ShowText()
    {
        if (status == 0)
        {
            diaName.SetActive(true);

            dialog[0].text = "감사합니다~";
            dialog[1].text = "빨리 이 숲을 벗어나고 싶은 마음에";
            dialog[2].text = "너무 서둘렀나 봐요...";
        }
        else if (status == 1)
        {
            dialog[0].text = "자, 마리오 님!";
            dialog[1].text = "조금만 더 가면 버섯 성이에요.";
            dialog[2].text = "";
        }
        else if (status == 2)
        {
            dialog[0].text = "어?";
            dialog[1].text = "이 낡아 빠진 해머는?!";
            dialog[2].text = "";
        }
        else if (status == 3)
        {
            dialog[0].text = "마, 마리오 님!";
            dialog[1].text = "이건 브러스표 해머예요!";
            dialog[2].text = "";
        }
        else if (status == 4)
        {
            dialog[0].text = "이것만 있으면";
            dialog[1].text = "어떤 녀석이라도 해치울 수 있어요!";
            dialog[2].text = "";
        }
        else if (status == 5)
        {
            GetHammerAnim();
        }
        else if (status == 6)
        {
            marioAnim.SetBool("isGetHammer", false);

            Info.SetActive(false);

            toad.SetActive(false);
            hammer.SetActive(false);
            cloud.SetActive(true);

            talkAudio.clip = cloudSound;
            talkAudio.Play();

            marioMove.isTalk = false;
            GameManager.instance.isHammerBroEvent = 2;

            status++;
        }
    }

    void GetHammerAnim()
    {
        marioAnim.SetBool("isGetHammer", true);

        msgWindow.SetActive(false);
        Info.SetActive(true);

        getItem.SetActive(true);

        GameManager.instance.validItem[(int)ECLOTHES.HAMMER][5] = 1;
    }
}
