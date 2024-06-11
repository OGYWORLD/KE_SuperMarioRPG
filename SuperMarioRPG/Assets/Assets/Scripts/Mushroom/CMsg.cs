using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMsg : MonoBehaviour
{
    public Animator marioAnim = null;
    public GameObject mellow = null;
    public Animator mellowAnim = null;

    public GameObject cloud = null;

    public CMarioMove CMario = null;

    private int status = 0;

    public GameObject msgWindow = null;

    public Text text01 = null;
    public Text text02 = null;
    public Text text03 = null;

    public GameObject infoWindow = null;

    private AudioSource mellowAudio = null;

    public GameObject beFried = null;

    public AudioClip pressed = null;
    public AudioClip cloudSound = null;

    void Start()
    {
        mellowAudio = gameObject.GetComponent<AudioSource>();

        text01.text = "���� �������� ȣ������ ��";
        text02.text = "������ [���]��� �ؿ�.";
        text03.text = "���������� ������ ���� ���ؿ�.";
    }

    // Update is called once per frame
    void Update()
    {
        if (CMario.isDrived)
        {
            StartCoroutine(waitMarioWalk());
        }

        ShowText();
        ShowWindow();
        GetInput();
    }

    void ShowWindow()
    {
        if(status == 1 || status == 2)
        {
            mellowAudio.clip = pressed;

            msgWindow.SetActive(true);
        }
        else if(status == 3)
        {
            beFried.SetActive(true);

            msgWindow.SetActive(false);
            infoWindow.SetActive(true);

            marioAnim.SetInteger("status", 6);
            mellowAnim.SetInteger("status", 6);
        }
        else if(status == 4)
        {
            mellowAudio.clip = cloudSound;
            mellowAudio.Play();

            GameManager.instance.memberIndex = EMEMBER.MELLOW;
            marioAnim.SetInteger("status", 0);
            mellowAnim.SetInteger("status", 0);

            mellow.SetActive(false);
            cloud.SetActive(true);
            infoWindow.SetActive(false);
            CMario.isTouched = false;
            GameManager.instance.isWithMellow = true;

            status++;
        }
    }

    void ShowText()
    {
        if(status == 2)
        {
            text01.text = "������ ��, ���� ���� �Բ�";
            text02.text = "������ ���� �� �Ǿ ��� �ּ���!";
            text03.text = "�̷��� ��Ź�帱�Կ�!!";
        }
    }

    void GetInput()
    {
        if (status <= 3 && Input.GetKeyDown(KeyCode.K) && !GameManager.instance.isWithMellow && !GameManager.instance.isMenu)
        {
            mellowAudio.Play();

            status++;
        }
    }

    IEnumerator waitMarioWalk()
    {
        yield return new WaitForSeconds(1.0f);

        status = 1;
    }
}
