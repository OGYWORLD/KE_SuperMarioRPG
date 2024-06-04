using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECatched
{
    NOTHING,
    MARIO,
    WALL
}
public class CCatchedMario : MonoBehaviour
{
    #region public
    public RayCastValues rcValues = null;
    #endregion

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
