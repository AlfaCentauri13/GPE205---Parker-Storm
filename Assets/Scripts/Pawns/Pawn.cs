using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour // this is abstract because we don't wanna make an instance of pawn at any point in time
{

    // float so they can be tiny decimals
    public float moveSpeed = 50; // this will be forward and backwards
    public float turnSpeed = 360; // this will be how fast the vehicle turns

    // variable to hold our mover
    public Mover mover;
    public TankMover tMover; 

    // Start is called before the first frame update
    public virtual void Start() // public virtual allows us to override the functionality of these functions. 
    {
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // leave the functionality to the child classes.

    public abstract void MoveForward(); 

    public abstract void MoveBackward();

    public abstract void RotateClockwise();

    public abstract void RotateCounterClockwise(); 
}
