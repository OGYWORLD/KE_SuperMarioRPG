using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CBGMControl : MonoBehaviour
{
    public static CBGMControl bgm;

    private AudioSource bgmAudio = null;

    public AudioClip mushRoad = null;
    public AudioClip monsterBGM = null;
    public AudioClip bossBGM = null;
    public AudioClip winBGM = null;
    public AudioClip mushroomBGM = null;
    public AudioClip siffRoadBGM = null;

    void Awake()
    {
        if (bgm == null)
        {
            bgm = this;
        }
        else // !Warning: Two GameManager Exist
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        bgmAudio = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // when start the battle, stop the music
        if(GameManager.instance.isToucehd)
        {
            bgmAudio.Stop();
        }
        else
        {
            setMusic();
        }


    }

    void setMusic()
    {
        Scene curScene = SceneManager.GetActiveScene();

        if(GameManager.instance.isLoose)
        {
            StartCoroutine(LooseBGM());
        }
        else if (GameManager.instance.isWin)
        {
            bgmAudio.clip = winBGM;

            if (!bgmAudio.isPlaying)
            {
                bgmAudio.Play();
            }
        }
        else if(curScene.name == "d02out")
        {
            bgmAudio.clip = mushRoad;

            if (!bgmAudio.isPlaying)
            {
                bgmAudio.Play();
            }
        }
        else if (curScene.name == "d02out3")
        {
            bgmAudio.clip = mushRoad;

            if (!bgmAudio.isPlaying)
            {
                bgmAudio.Play();
            }
        }
        else if (curScene.name == "btld02a")
        {
            bgmAudio.clip = monsterBGM;

            if (!bgmAudio.isPlaying)
            {
                bgmAudio.Play();
            }
        }
        else if (curScene.name == "btld02b")
        {
            bgmAudio.clip = monsterBGM;

            if (!bgmAudio.isPlaying)
            {
                bgmAudio.Play();
            }
        }
        else if (curScene.name == "btld02a_Boss")
        {
            bgmAudio.clip = bossBGM;

            if (!bgmAudio.isPlaying)
            {
                bgmAudio.Play();
            }
        }
        else if (curScene.name == "m01out")
        {
            bgmAudio.clip = mushroomBGM;

            if (!bgmAudio.isPlaying)
            {
                bgmAudio.Play();
            }
        }
        else if (curScene.name == "d02out2")
        {
            bgmAudio.clip = siffRoadBGM;

            if (!bgmAudio.isPlaying)
            {
                bgmAudio.Play();
            }
        }
    }

    IEnumerator LooseBGM()
    {
        float sumTime = 1f;

        while (sumTime > 0f)
        {
            bgmAudio.pitch = sumTime;

            sumTime -= Time.deltaTime;

            yield return null;
        }

        bgmAudio.Stop();

        yield break;
    }
}
