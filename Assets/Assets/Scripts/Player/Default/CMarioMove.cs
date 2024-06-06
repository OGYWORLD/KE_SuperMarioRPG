using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMarioMove : MonoBehaviour
{
    enum EKeyDirection
    {
        ORIGIN = -1,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    private float curMarioY = 0f;
    private float speed = 7.0f;
    private float jumpPower = 11.0f;
    private float jumpPowerHeight = 2.0f;

    private EKeyDirection preKey = EKeyDirection.ORIGIN;

    private bool isJumping = false;
    private Animator animator;

    public GameObject marioCamera = null;
    public GameObject mario = null;

    public bool isTouched { get; set; } = false;
    
    private List<Vector3> MarioPosforRay;

    private Vector3 preVector;

    private Vector3 mellowPos = new Vector3(-20.1f, 12f, 12.4f);

    public bool isDrived { get; set; } = false;
    void Start()
    {
        Scene curScene = SceneManager.GetActiveScene();

        if (GameManager.instance.beforeSceneName != curScene.name)
        {
            // 씬 변경 내용 여기서 처리!!!!!!!!!!!!!!!!!!

            // ex) 2 - 0 (d2out3 - d2out) => 양수 이므로 다음씬으로 갔기 때문에 flag는 0
            int flag;
            if(GameManager.instance.chgStage[curScene.name] - GameManager.instance.chgStage[GameManager.instance.beforeSceneName] <= 0)
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            Vector3 posSet = new Vector3(
                GameManager.instance.InOutPos[GameManager.instance.chgStage[curScene.name] + flag].x,
                GameManager.instance.InOutPos[GameManager.instance.chgStage[curScene.name] + flag].y,
                GameManager.instance.InOutPos[GameManager.instance.chgStage[curScene.name] + flag].z);
            gameObject.transform.position = posSet;

        }
        else
        {
            Vector3 posSet = new Vector3(GameManager.instance.curMarioPos.x, GameManager.instance.curMarioPos.y, GameManager.instance.curMarioPos.z);
            gameObject.transform.position = posSet;
        }

        animator = mario.GetComponent<Animator>();
        curMarioY = 0f;
    }

    void Update()
    {
        preVector = mario.transform.position;

        isBeDetectedBottom();
        InputMove();
        Jump();
        SetCameraYPos();

        GoToMellow();
    }

    void InputMove()
    {

        if (Input.GetKey(KeyCode.UpArrow) && !isTouched)
        {

            animator.SetBool("isWalk", true);

            Quaternion ToNorth = Quaternion.Euler(0.0f, -180.0f, 0.0f);

            mario.transform.rotation = ToNorth;

            CameraPosUpdate(EKeyDirection.UP);

            preKey = EKeyDirection.UP;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !isTouched)
        {
            animator.SetBool("isWalk", true);

            Quaternion ToWest = Quaternion.Euler(0.0f, -270.0f, 0.0f);

            mario.transform.rotation = ToWest;

            CameraPosUpdate(EKeyDirection.LEFT);

            preKey = EKeyDirection.LEFT;
        }

        if (Input.GetKey(KeyCode.DownArrow) && !isTouched)
        {
            animator.SetBool("isWalk", true);

            Quaternion ToSouth = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            mario.transform.rotation = ToSouth;

            CameraPosUpdate(EKeyDirection.DOWN);

            preKey = EKeyDirection.DOWN;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !isTouched)
        {
            animator.SetBool("isWalk", true);

            Quaternion ToEast = Quaternion.Euler(0.0f, -90.0f, 0.0f);

            mario.transform.rotation = ToEast;

            CameraPosUpdate(EKeyDirection.RIGHT);

            preKey = EKeyDirection.RIGHT;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isTouched)
        {
            isJumping = true;
            animator.SetBool("isJump", true);
        }

        if (!Input.anyKey && !isDrived)
        {
            preKey = EKeyDirection.ORIGIN;

            animator.SetBool("isWalk", false);
        }
    }

    void Jump()
    {
        if (isJumping)
        {
            mario.transform.position += new Vector3(0.0f, jumpPower * Time.deltaTime, 0.0f);

            if (mario.transform.position.y > curMarioY + jumpPowerHeight)
            {
                mario.transform.position = new Vector3(mario.transform.position.x, curMarioY + jumpPowerHeight, mario.transform.position.z);
                isJumping = false;
                animator.SetBool("isJump", isJumping);
            }
        }
        else if (!isJumping)
        {
            mario.transform.position -= new Vector3(0.0f, jumpPower * Time.deltaTime, 0.0f);

            if (mario.transform.position.y <= curMarioY)
            {
                mario.transform.position = new Vector3(mario.transform.position.x, curMarioY, mario.transform.position.z);
            }
        }
    }

    void CameraPosUpdate(EKeyDirection _keyd)
    {
        if(!isBeDetectedWall())
        {
            if (_keyd == preKey)
            {
                transform.position += mario.transform.forward * speed * Time.deltaTime;
            }
        }
    }

    bool isBeDetectedWall()
    {
        MarioPosforRay = new List<Vector3>();

        MarioPosforRay.Add(mario.transform.position);
        MarioPosforRay.Add(mario.transform.position + new Vector3(0.0f, 0.8f, 0.0f));
        MarioPosforRay.Add(mario.transform.position + new Vector3(0.0f, 1.2f, 0.0f));

        foreach (Vector3 pos in MarioPosforRay)
        {
            if (Physics.Raycast(pos, mario.transform.forward, out RaycastHit hit, 0.5f))
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Monster"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void isBeDetectedBottom()
    {
        if (Physics.Raycast(mario.transform.position, -mario.transform.up, out RaycastHit hit, 0.1f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                if(curMarioY < mario.transform.position.y)
                {
                    curMarioY = mario.transform.position.y;
                    jumpPowerHeight = 2.0f + curMarioY;
                }
            }
        }
        else
        {
            curMarioY = 0f;
        }
    }

    void SetCameraYPos()
    {
        if(!isTouched)
        {
            if (preVector.y > mario.transform.position.y)
            {
                transform.position += -mario.transform.up * 2f * Time.deltaTime;
            }
            else if (preVector.y < mario.transform.position.y)
            {
                transform.position += mario.transform.up * 2f * Time.deltaTime;
            }
        }
    }
    void GoToMellow()
    {
        if (isDrived)
        {
            transform.position = Vector3.Lerp(transform.position, mellowPos, Time.deltaTime);

            Quaternion toMellow = Quaternion.Euler(0f, 180f, 0f);
            mario.transform.rotation = Quaternion.Lerp(mario.transform.rotation, toMellow, Time.deltaTime);

            StartCoroutine(TalkToMellow());
        }
    }

    IEnumerator TalkToMellow()
    {
        yield return new WaitForSeconds(1.0f);
        isDrived = false;
    }
}
