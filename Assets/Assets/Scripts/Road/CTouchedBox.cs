using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTouchedBox : MonoBehaviour
{
    [SerializeField]
    private int LeavesPuchBoxNum = 3;
    private Animator animator;

    public GameObject CoinPrefab;
    private GameObject[] Coins;
    private int coinNum = 3;
    private int coinIndex = 0;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("Next", LeavesPuchBoxNum);

        Coins = new GameObject[coinNum];
        for(int i = 0; i < coinNum; i++)
        {
            Coins[i] = Instantiate(CoinPrefab, transform.position, Quaternion.identity);
            Coins[i].SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(LeavesPuchBoxNum > 0)
        {
            StartCoroutine(ShowCoin());

            GameManager.instance.coin++;
            animator.SetTrigger("Touched");
            LeavesPuchBoxNum--;
            if (LeavesPuchBoxNum < 0)
            {
                LeavesPuchBoxNum = 0;
            }
            animator.SetInteger("Next", LeavesPuchBoxNum);
        }
    }

    IEnumerator ShowCoin()
    {
        yield return new WaitForSeconds(0.4f);
        Coins[coinIndex].SetActive(true);

        yield return new WaitForSeconds(0.3f); // 실행 주기
        Coins[coinIndex].SetActive(false);
        coinIndex++;

        if(coinIndex > 3)
        {
            coinIndex = 0;
        }
    }
}
