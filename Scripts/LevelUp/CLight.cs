using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLight : MonoBehaviour
{
    public CLevelUpManager luManager = null;

    private Vector3[] lightPos = new Vector3[2];

    private Animator animator = null;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        lightPos[0] = new Vector3(0f, 0f, 0f);
        lightPos[1] = new Vector3(1.16f, 0f, 1.16f);
    }

    void Update()
    {
        SetLight();
    }

    void SetLight()
    {
        // Set Light to Current LevelUp Character
        gameObject.GetComponent<Transform>().position = lightPos[luManager.curLevelUp];
    }
}
