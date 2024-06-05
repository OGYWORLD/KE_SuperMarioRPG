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

    private float speed = 7.0f;
    private float jumpPower = 11.0f;
    private float jumpPowerMax = 0f;
    private float jumpPowerHeight = 1.5f;

    private Vector3 prePos;
    private EKeyDirection preKey = EKeyDirection.ORIGIN;

    private bool isJumping = false;
    private Animator animator;

    public GameObject marioCamera = null;
    public GameObject mario = null;

    public bool isTouched = false;

    private Transform cameraTrans = null;

    private List<Vector3> MarioPosforRay;


    void Start()
    {
        Scene curScene = SceneManager.GetActiveScene();

        if (GameManager.instance.beforeSceneName != curScene.name)
        {
            // 씬 변경 내용 여기서 처리!!!!!!!!!!!!!!!!!!

            int flag = GameManager.instance.chgStage[curScene.name] - GameManager.instance.chgStage[GameManager.instance.beforeSceneName] <= 0 ? 0 : 1;
            Vector3 posSet = new Vector3(
                GameManager.instance.InOutPos[GameManager.instance.chgStage[curScene.name] + flag].m_x,
                GameManager.instance.InOutPos[GameManager.instance.chgStage[curScene.name] + flag].m_y,
                GameManager.instance.InOutPos[GameManager.instance.chgStage[curScene.name] + flag].m_z);
            gameObject.transform.position = posSet;
        }
        else
        {
            Vector3 posSet = new Vector3(GameManager.instance.curMarioPos.m_x, GameManager.instance.curMarioPos.m_y, GameManager.instance.curMarioPos.m_z);
            gameObject.transform.position = posSet;
        }
        animator = mario.GetComponent<Animator>();
        cameraTrans = marioCamera.GetComponent<Transform>();
        jumpPowerMax = mario.transform.position.y + jumpPowerHeight;
        prePos = mario.transform.forward;
    }

    void Update()
    {
        InputMove();
        Jump();
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

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            animator.SetBool("isJump", true);
        }

        if (!Input.anyKey)
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
            if(mario.transform.position.y > jumpPowerMax)
            {
                mario.transform.position = new Vector3(mario.transform.position.x, jumpPowerMax, mario.transform.position.z);
                isJumping = false;
            }
        }
        else if (!isJumping)
        {
            mario.transform.position -= new Vector3(0.0f, jumpPower * Time.deltaTime, 0.0f);
            if (mario.transform.position.y < 0.0f)
            {
                animator.SetBool("isJump", isJumping);
                mario.transform.position = new Vector3(mario.transform.position.x, 0.0f, mario.transform.position.z);
            }
        }
    }

    void CameraPosUpdate(EKeyDirection _keyd)
    {
        if(!isBeDetectedWall())
        {
            if (_keyd == preKey)
            {
                prePos = mario.transform.forward;
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

}
