using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class CbtlTrans : MonoBehaviour
{
    // About Control animator State
    private Animator animator;

    #region public ����
    public GameObject mario = null;
    public CMarioMove marioMove = null;
    public Camera marioCamera = null;
    public GameObject battleTrans = null;
    public CMonsterMove move = null;
    //public 
    #endregion

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();

        // battleTrans HAVE TO continue on Next Scene
        DontDestroyOnLoad(battleTrans);
    }

    void Update()
    {
        if (marioMove.isTouched)
        {
            CameraMoving();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        move.runSpeed = 0f;
        move.walkSpeed = 0f;
        animator.speed = 0;
        marioMove.isTouched = true;

        Scene curScene = SceneManager.GetActiveScene();

        GameManager.instance.beforeSceneName = curScene.name;

        GameManager.instance.curMarioPos = new Pos(mario.transform.position.x, mario.transform.position.y, mario.transform.position.z);

        GameManager.instance.monsterAtv.Add(gameObject.name);

        GameManager.instance.btlMoster = Regex.Replace(gameObject.name, @"[0-9]", "");

        GameManager.instance.isNowBattle = true;

        StartCoroutine(ChangeScene());
    }

    void CameraMoving()
    {
        marioCamera.orthographicSize = Mathf.Lerp(marioCamera.orthographicSize, marioCamera.orthographicSize - 0.2f, Time.deltaTime / 0.1f);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.2f);
        battleTrans.SetActive(true);

        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(2); // d02out btl
    }
}
