using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RayCastValues", fileName = "RayCastValues")]
public class RayCastValues : ScriptableObject
{
    public float CatchedRange;
    public float CatchedWallRange;
    public float halfPosY;
    public float turnAngle;
    public float walkSpeed;
    public float runSpeed;
}
