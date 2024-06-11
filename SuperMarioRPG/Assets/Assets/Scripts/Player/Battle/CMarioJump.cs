using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMarioJump : MonoBehaviour
{
    public CBattleManager btlManager = null;

    private Transform trans = null;
    private Animator animator = null;

    public AudioClip jump = null;
    private AudioSource playerAudio = null;

    void Start()
    {
        playerAudio = gameObject.GetComponent<AudioSource>();
        trans = gameObject.GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Toad Assist
        if (btlManager.status == 10)
        {
            AttackTurn();
            Jump();
        }
    }

    void AttackTurn()
    {
        trans.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && btlManager.jumpCount <= 2)
        {
            playerAudio.clip = jump;
            playerAudio.Play();

            btlManager.jumpCount++;
            animator.SetBool("isJump", true);
            StartCoroutine(JumpMotion());
        }
    }

    IEnumerator JumpMotion()
    {
        float sumTime = 0f;
        float TotalTime = 0.2f;

        while (sumTime <= TotalTime)
        {
            trans.position = Vector3.Lerp(trans.position, new Vector3(trans.position.x, 1f, trans.position.z), sumTime / TotalTime);

            sumTime += Time.deltaTime;


            yield return null;
        }

        animator.SetBool("isJump", false);

        sumTime = 0f;
        while (sumTime <= TotalTime)
        {
            trans.position = Vector3.Lerp(trans.position, new Vector3(trans.position.x, 0f, trans.position.z), sumTime / TotalTime);

            sumTime += Time.deltaTime;


            yield return null;
        }

        yield break;
    }
}
