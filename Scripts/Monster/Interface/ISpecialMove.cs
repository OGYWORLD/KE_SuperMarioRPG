using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialMove
{
    public float walkSpeed {get; set;}
    public float runSpeed {get; set;}
    public float rotationY {get; set;}

    public void specificMove();
}

