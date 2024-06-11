using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class CbtlTrans : MonoBehaviour
{
    // About Control animator State
    private Animator animator;

    #region public º¯¼ö
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
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.isToucehd = true;

            GameManager.instance.isNowBattle = true;

            move.rcValues.runSpeed = 0f;
            move.rcValues.walkSpeed = 0f;
            animator.speed = 0;
            marioMove.isTouched = true;

            Scene curScene = SceneManager.GetActiveScene();

            GameManager.instance.beforeSceneName = curScene.name;

            GameManager.instance.curMarioPos = new Vector3(mario.transform.position.x, mario.transform.position.y, mario.transform.position.z);

            GameManager.instance.monsterAtv.Add(gameObject.name);

            GameManager.instance.btlMoster = Regex.Replace(gameObject.name, @"[0-9]", "");

            StartCoroutine(ChangeScene());
        }
    }

    void CameraMoving()
    {
        marioCamera.orthographicSize = Mathf.Lerp(marioCamera.orthographicSize, marioCamera.orthographicSize - 0.1f, 0.03f);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.2f);
        battleTrans.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        if(SceneManager.GetActiveScene().name == "d02out2")
        {
            SceneManager.LoadScene("btld02b"); // d02out2 btl
        }
        else
        {
            SceneManager.LoadScene("btld02a");
        }
    }
}
