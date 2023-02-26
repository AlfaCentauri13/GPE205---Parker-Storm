using UnityEngine;

public class TankMovement : MonoBehaviour
{

    #region ALL THE VARIABLES
    [Header("Movement")]
    public float moveSpeed;
    public float rotationSpeed;

    public float groundDrag;

    [Header("IsOnGround")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    // Vector3 moveDirection; unused variable, but I'm keeping it just in case I need it in the future.
    Rigidbody rigidBody;
    #endregion ALL THE VARIABLES

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, whatIsGround); // check to see if we are on the ground
        PlayerInput();

        rigidBody.drag = grounded ? groundDrag : 0;
        SpeedControl();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        rigidBody.AddForce(moveSpeed * 10f * (verticalInput * Time.deltaTime) * rigidBody.transform.forward, ForceMode.Force); // apply the calculate force
        rigidBody.transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime); // at rotation to the tank based on horizontal input
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(rigidBody.velocity.x, 0f, rigidBody.velocity.z); // default velocity of the tank

        // limit velocity if needed

        if(flatVel.magnitude > moveSpeed) // if the tanks velocity is greater than its set movement speed
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; // adjust tank speed to match move speed
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z); // limit the current velocity of the tank to the desired velocity set 
        }

    }
}
