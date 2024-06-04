using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterMove : MonoBehaviour, ISpecialMove
{

    // About move
    public float walkSpeed { get; set; } = 1.0f;
    public float runSpeed { get; set; } = 5.0f;
    public float rotationY { get; set; } = 90f;

    // About Control animator State
    private Animator animator;

    // CatchedMario
    private CCatchedMario catched = null;

    void Awake()
    {
        catched = gameObject.GetComponent<CCatchedMario>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
       if(GameManager.instance.monsterAtv.Contains(gameObject.name))
       {
           gameObject.SetActive(false);
       }
       else
       {
           gameObject.SetActive(true);
       }
    }

    void Update()
    {
        GoombarMove();
    }

    void OnEnable()
    {
        animator.SetBool("Catched", false);
    }

    void GoombarMove()
    {
        if (catched.isBeCatched() == ECatched.NOTHING)
        {
            animator.SetBool("Catched", false);
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }
        else if (catched.isBeCatched() == ECatched.WALL)
        {
            animator.SetBool("Catched", false);
            Quaternion TurnQuater = Quaternion.Euler(0.0f, rotationY, 0.0f);

            transform.rotation *= TurnQuater;
        }
        else if (catched.isBeCatched() == ECatched.MARIO)
        {
            animator.SetBool("Catched", true);
            specificMove();
        }

    }

    public void specificMove()
    {
        transform.position += transform.forward * runSpeed * Time.deltaTime;
    }
}
