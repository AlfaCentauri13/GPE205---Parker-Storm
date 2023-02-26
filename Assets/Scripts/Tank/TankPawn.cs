using UnityEngine;

public class TankPawn : Pawn
{
    public KeyCode fireKey = KeyCode.Space;

    private GameObject GunObj;
    private Gun gun;
    public float collisionSphereRadius = 20f;
    bool tankCollisionCheck;

    public override void Start()
    {
        base.Start();
        GunObj = GameObject.Find("TankObj");
        gun = GunObj.GetComponent<Gun>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(fireKey)) gun.ShootBullet(); // check if we are pressing fire key, if so, shoot


        /*tankCollisionCheck = Physics.CheckSphere(transform.position, collisionSphereRadius);
        if (tankCollisionCheck)
        {
            CollisionWithObject();
        } else
        {
            var movementComponent = gameObject.GetComponent<TankMovement>();
            movementComponent.modRotationSpeed = movementComponent.defaultTankRotationSpeed;
        }*/
    }

   /* void CollisionWithObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionSphereRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Obstacle")
            {
                var movementComponent = gameObject.GetComponent<TankMovement>();

                float distanceToCollidedObject = Vector3.Distance(gameObject.transform.position, collider.gameObject.transform.position);
                Debug.Log("WHYYYYYYYYYYY");

                if (distanceToCollidedObject <= 2)
                    movementComponent.modRotationSpeed -= 0.05f * Time.deltaTime;
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, collisionSphereRadius);
    }*/

    //void OnCollisionEnter (Collision collision) 
    //{



    //    Debug.Log(movementComponent.modRotationSpeed.ToString()); // nightmare nightmare nightmare nightmare nightmare


    //}


    //void OnCollisionExit (Collision collision) 
    //{
    //    var movementComponent = gameObject.GetComponent<TankMovement>();

    //    float distanceToCollidedObject = Vector3.Distance(gameObject.transform.position, collision.gameObject.transform.position);

    //    if (collision.gameObject.layer.ToString() != "whatIsGround")
    //    {
    //        Debug.Log("WHYYYYYYYYYYY");
    //        movementComponent.modRotationSpeed = movementComponent.defaultTankRotationSpeed;
    //    }

    //    Debug.Log(movementComponent.modRotationSpeed.ToString()); // nightmare nightmare nightmare nightmare nightmare


    //}


}
