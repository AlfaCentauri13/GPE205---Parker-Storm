using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TankMover : Mover
{
    // variable to store our Rigidbody component.
    private Rigidbody rb;

    public override void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }
    public override void Rotate(float rotateSpeed) => rb.transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f); // use Rotate and rotateSpeed * Time.deltaTime as Y

}
