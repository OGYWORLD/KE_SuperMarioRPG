using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CLevelUpManager : MonoBehaviour
{
    public static CLevelUpManager lu; // LUGameManager

    public Queue<int> whoLevelUp { get; set; } = new Queue<int>();
    public int curLevelUp { get; set; }

    // 0: wait, 1: message01, 2: message02, 3: message03, 4: Reset
    public int status { get; set; } = 0;

    // 0: Hammer, 1: Mushroom, 2: Flower
    public int whichSelect { get; set; } = 0;

    public CLUPlayer mario = null;
    public CLUPlayer mellow = null;

    public GameObject mellowObj = null;

    private bool isUpdate = false;

    public Image fadeImage = null;

    void Awake()
    {
        if (lu == null)
        {
            lu = this;
        }
        else // !Warning: Two GameManager Exist
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Check Who Level Up
        CalculWhoLevelUp();

        // Level up Count
        curLevelUp = whoLevelUp.Dequeue();
    }

    void Update()
    {
        Reset();

        SetCharacter();

        if (!isUpdate && status == 3)
        {
            isUpdate = true;
            // Update Info
            UpdateLevelUpInfo();
        }
    }

    void CalculWhoLevelUp()
    {

        if(GameManager.instance.members[(int)EMEMBER.MARIO].m_exp >= GameManager.instance.members[(int)EMEMBER.MARIO].m_leftExp)
        {
            whoLevelUp.Enqueue((int)EMEMBER.MARIO);
        }

        if(GameManager.instance.members[(int)EMEMBER.MELLOW].m_exp >= GameManager.instance.members[(int)EMEMBER.MELLOW].m_leftExp)
        {
            whoLevelUp.Enqueue((int)EMEMBER.MELLOW);
        }

    }

    void Reset()
    {
        if (status == 4)
        {
            status = 5;

            // Update Bonus Info
            UpdateBonus();

            // check still levelUp player exist
            if (whoLevelUp.Count == 1)
            {
                isUpdate = false;
                whichSelect = 0;
                curLevelUp = whoLevelUp.Dequeue();
                status = 0;
                mario.isSrt = false;
                mellow.isSrt = false;
            }
            else
            {
                // return road
                StartCoroutine(Fadeout());
            }
        }
    }

    void UpdateLevelUpInfo()
    {
        GameManager.instance.members[lu.curLevelUp].m_level += 1;
        GameManager.instance.members[lu.curLevelUp].m_maxhp += 5;
        GameManager.instance.members[lu.curLevelUp].m_stat = new Stats(GameManager.instance.members[lu.curLevelUp].m_stat.m_attak + 3,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_defense + 2,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_magicAttack + 2,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_magicDefense + 2);
    }

    void UpdateBonus()
    {
        if(whichSelect == 0)
        {
            GameManager.instance.members[curLevelUp].m_stat = new Stats(GameManager.instance.members[lu.curLevelUp].m_stat.m_attak + 2,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_defense + 1,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_magicAttack,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_magicDefense);
        }
        else if(whichSelect == 1)
        {
            GameManager.instance.members[curLevelUp].m_maxhp += 3;
        }
        else if(whichSelect == 2)
        {
            GameManager.instance.members[curLevelUp].m_stat = new Stats(GameManager.instance.members[lu.curLevelUp].m_stat.m_attak,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_defense,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_magicAttack + 1,
            GameManager.instance.members[lu.curLevelUp].m_stat.m_magicDefense + 1);
        }

        // Fill Full
        GameManager.instance.members[lu.curLevelUp].m_curhp = GameManager.instance.members[lu.curLevelUp].m_maxhp;
        GameManager.instance.members[lu.curLevelUp].m_exp = 0;
        GameManager.instance.curFP = GameManager.instance.maxFP;
        GameManager.instance.members[lu.curLevelUp].m_leftExp = GameManager.instance.EXPNeeded[GameManager.instance.members[lu.curLevelUp].m_level - 1];
    }

    IEnumerator Fadeout()
    {
        float sumTime = 0f;
        float totalTime = 0.5f;

        while (sumTime <= totalTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0, 1, sumTime / totalTime);
            fadeImage.color = color;

            sumTime += Time.deltaTime;

            yield return null;
        }

        GameManager.instance.isWin = false;

        SceneManager.LoadScene(GameManager.instance.beforeSceneName);

        yield break;
    }

    void SetCharacter()
    {
        if(GameManager.instance.memberIndex == EMEMBER.MELLOW)
        {
            mellowObj.SetActive(true);
        }
    }
}
