using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    #region Variables
    public Transform orientation, player, playerObj;
    public Rigidbody rigidBody;
    public float rotationSpeed;
    #endregion Variables

    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
