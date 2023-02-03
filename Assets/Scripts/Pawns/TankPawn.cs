using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TankPawn : Pawn
{

    private Camera mainCamera; 

    // Start is called before the first frame update
    public override void Start() 
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // this just works. It grabs the component by the tag!

        tMover = GetComponent<TankMover>();

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {

        mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 20, this.transform.position.z); // make the camera follow the tank

        base.Update(); // whatever happens in Pawn.Update() will happen in TankPawn.Update(); 

    }

    public override void MoveForward() 
    {
        
        // mover = GetComponent<TankMover>();
        if (tMover != null)
        {
            Debug.Log("Moving Forward.");
            tMover.Move(transform.forward, moveSpeed);
        } 
        else Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
    }

    public override void MoveBackward() 
    {
        if (tMover != null)
        {
            Debug.Log("Moving Backwards.");
            tMover.Move(transform.forward, -moveSpeed);
        } 
        else Debug.LogWarning("Warning: No Mover in TankPawn.MoveBackward()!");

    }

    public override void RotateClockwise() 
    {
        if (tMover != null)
        {
            Debug.Log("Rotating Clockwise.");
            tMover.Rotate(turnSpeed);
        }
        else Debug.LogWarning("Warning: No Mover in TankPawn.RotateClockwise()!");
        
    }

    public override void RotateCounterClockwise() 
    {
        if (tMover != null)
        {
            Debug.Log("Rotating Counter Clockwise.");
            tMover.Rotate(-turnSpeed);
        }
        else Debug.LogWarning("Warning: No Mover in TankPawn.RotateCounterClockwise()!");
        
    }

}
