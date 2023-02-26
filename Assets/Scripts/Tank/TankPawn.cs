using UnityEngine;

public class TankPawn : Pawn
{
    public KeyCode fireKey = KeyCode.Space;

    private GameObject GunObj;
    private Gun gun; 

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
    }
}
