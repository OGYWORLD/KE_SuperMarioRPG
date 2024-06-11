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

        dialog[0].text = "���⸦ �������� �ʹٰ�?";
        dialog[1].text = "�����տ� �� �ظ� ������?";
        dialog[2].text = "���� ��������� ���� �Ŵ�!";
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

                dialog[0].text = "������ ��!";
                dialog[1].text = "�޼տ� ���� ���� ���̽���?";
                dialog[2].text = "�� �༮, ���� ���ؿ�...";
            }
            else if (status == 1)
            {
                diaName.SetActive(false);

                dialog[0].text = "���� ���� �Ϸ���?";
                dialog[1].text = "����� �ָ�!!";
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

            dialog[0].text = "�����մϴ�~";
            dialog[1].text = "���� �� ���� ����� ���� ������";
            dialog[2].text = "�ʹ� ���ѷ��� ����...";
        }
        else if (status == 1)
        {
            dialog[0].text = "��, ������ ��!";
            dialog[1].text = "���ݸ� �� ���� ���� ���̿���.";
            dialog[2].text = "";
        }
        else if (status == 2)
        {
            dialog[0].text = "��?";
            dialog[1].text = "�� ���� ���� �ظӴ�?!";
            dialog[2].text = "";
        }
        else if (status == 3)
        {
            dialog[0].text = "��, ������ ��!";
            dialog[1].text = "�̰� �귯��ǥ �ظӿ���!";
            dialog[2].text = "";
        }
        else if (status == 4)
        {
            dialog[0].text = "�̰͸� ������";
            dialog[1].text = "� �༮�̶� ��ġ�� �� �־��!";
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
