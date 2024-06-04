using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ActiveStartCutScene : MonoBehaviour
{
    #region public º¯¼ö

    public GameObject CutScene = null;
    public GameObject BackgroundCutScene = null;

    public VideoPlayer CutSceneVideo = null;

    #endregion

    private int curPage = 0; // 0: CutScene, 1: Background

    void Start()
    {
        StartCoroutine(PlayBackgroundCutScnce());
        BackgroundCutScene.SetActive(false);
    }

    void Update()
    {
        if(Input.anyKeyDown && curPage == 0)
        {
            CutSceneVideo.Pause();
            ActiveBackgroundCutScene();
            curPage = 1;
        }
        else if(Input.anyKeyDown && curPage == 1)
        {
            SceneManager.LoadScene(+1); // d02out
        }
    }

    IEnumerator PlayBackgroundCutScnce()
    {
        if(curPage == 0)
        {
            yield return new WaitForSeconds(40.0f);
            ActiveBackgroundCutScene();
        }
    }

    void ActiveBackgroundCutScene()
    {
        CutScene.SetActive(false);
        BackgroundCutScene.SetActive(true);
    }
}
