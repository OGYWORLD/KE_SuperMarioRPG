using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerValues", fileName = "PlayerValues")]
public class CPlayerValue : ScriptableObject
{
    public int Me;
    public int You;
    public Vector3[] monPos;
}
