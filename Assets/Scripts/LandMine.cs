using UnityEngine;

/*
Set a damage amount as a public float

grab the current objects health when something enters the collision sphere
subtract the damage amount from the object's health

destroy the mine
 */



public class LandMine : MonoBehaviour
{ 
    [SerializeField]
    public float damageAmt = 50;

    private void OnTriggerEnter(Collider other)
    {
        HealthManager objectHealth = other.GetComponent<HealthManager>();
        if (objectHealth)
        {
            objectHealth.health -= damageAmt;
            Destroy(gameObject); 
        }
    }
}
