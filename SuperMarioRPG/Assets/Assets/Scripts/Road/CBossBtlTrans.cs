using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CBossBtlTrans : MonoBehaviour
{
    // About Control animator State
    private Animator animator;

    #region public º¯¼ö
    public GameObject mario = null;
    public CMarioMove marioMove = null;
    public Camera marioCamera = null;
    public GameObject battleTrans = null;
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

    public void BtlTrans(string _n, string _s)
    {
        GameManager.instance.isToucehd = true;

        marioMove.isTouched = true;

        Scene curScene = SceneManager.GetActiveScene();

        GameManager.instance.beforeSceneName = curScene.name;

        GameManager.instance.curMarioPos = new Vector3(mario.transform.position.x, mario.transform.position.y, mario.transform.position.z);

        GameManager.instance.btlMoster = _n;

        GameManager.instance.isNowBattle = true;

        GameManager.instance.isBossBtl = true;

        StartCoroutine(ChangeScene(_s));
    }

    void CameraMoving()
    {
        marioCamera.orthographicSize = Mathf.Lerp(marioCamera.orthographicSize, marioCamera.orthographicSize - 0.1f, 0.03f);
    }

    IEnumerator ChangeScene(string _s)
    {
        yield return new WaitForSeconds(0.2f);
        battleTrans.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        SceneManager.LoadScene(_s);
    }
}
