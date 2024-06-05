using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevelUpManager : MonoBehaviour
{
    public Queue<int> whoLevelUp { get; set; } = new Queue<int>();
    public int curLevelUp { get; set; }
    public int status { get; set; } // 0: wait, 1: ing

    void Start()
    {
        // Check Who Level Up
        CalculWhoLevelUp();

        // Level up Count
        curLevelUp = whoLevelUp.Dequeue();
    }

    void Update()
    {
        
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

    
}
