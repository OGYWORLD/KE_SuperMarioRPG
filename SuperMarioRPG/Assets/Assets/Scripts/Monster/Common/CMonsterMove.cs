using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterMove : MonoBehaviour
{
    public enum ECatched
    {
        NOTHING,
        WALL,
        MARIO
    }

    public RayCastValues rcValues = null;

    // About Control animator State
    private Animator animator;

    void Awake()
    {
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
        if (isBeCatched() == ECatched.NOTHING)
        {
            animator.SetBool("Catched", false);
            transform.position += transform.forward * rcValues.walkSpeed * Time.deltaTime;
        }
        else if (isBeCatched() == ECatched.WALL)
        {
            animator.SetBool("Catched", false);
            Quaternion TurnQuater = Quaternion.Euler(0.0f, rcValues.turnAngle, 0.0f);

            transform.rotation *= TurnQuater;
        }
        else if (isBeCatched() == ECatched.MARIO)
        {
            animator.SetBool("Catched", true);
            specificMove();
        }

    }

    public void specificMove()
    {
        transform.position += transform.forward * rcValues.runSpeed * Time.deltaTime;
    }

    public ECatched isBeCatched()
    {
        Vector3 HalfPos = transform.position + new Vector3(0.0f, rcValues.halfPosY, 0.0f);

        if (Physics.Raycast(HalfPos, transform.forward, out RaycastHit hit, rcValues.CatchedWallRange))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return ECatched.WALL;
            }
        }
        else if (Physics.Raycast(HalfPos, transform.forward, out RaycastHit hitM, rcValues.CatchedRange))
        {
            if (hitM.collider.CompareTag("Player"))
            {
                return ECatched.MARIO;
            }
        }

        return ECatched.NOTHING;
    }
}
