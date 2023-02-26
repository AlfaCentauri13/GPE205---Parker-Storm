using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables 

    public float bulletLife = 3f;
    public float bulletDamage = 5f;

    [HideInInspector]
    public Vector3 hitLocationofBullet;

    [HideInInspector]
    public Vector3 startLocationofBullet;

    #endregion Variables

    void Start()
    {

        // play the sound of the bullet being fired
        GameManager.instance.PlayTankShotSound(); 
        startLocationofBullet = transform.position;
        BulletSoundEnabled();
        Invoke("BulletSoundDisabled", .01f);

    }
    void Awake() => Destroy(gameObject, bulletLife); // destroy bullet 

    void OnCollisionEnter(Collision other) // when we hit something
    {
        DealDamage(other.gameObject);
        Debug.Log("Hit");
        BulletSoundEnabled();
        Invoke("BulletSoundDisabled", .1f);
        hitLocationofBullet = transform.position;
    }

    // make the bullet sound available 
    void BulletSoundEnabled()
    {
        GameObject bulletObj = gameObject;
        if (bulletObj) bulletObj.layer = LayerMask.NameToLayer("whatIsSound"); 
    }

    // make the bullet sound undetectable
    void BulletSoundDisabled()
    {
        GameObject bulletObj = gameObject;
        if (bulletObj) bulletObj.layer = LayerMask.NameToLayer("Default"); 
    }
    
    // we don't want the tank to trace the sound of the bullet as it is flying, we want the tank to only detect the sound once and then go towards it.

    public void DealDamage(GameObject target) // deals damage to a gameobject
    {
        if (!target.CompareTag("Bullet"))
        {
            HealthManager HealthManager = target.GetComponent<HealthManager>();
            if (HealthManager != null) // does the object have a health manager
            {
                HealthManager.TakeDamage(bulletDamage); // if it does subtract its health
                GameManager.instance.PlayTankImpactSound();
            }
            Destroy(gameObject);
        }
        if (target.CompareTag("Enemy"))
        {
            AIController enemyReference = target.GetComponent<AIController>(); // get a reference to the hit enemy

            if (enemyReference != null) // check if the enemy is valid
            {
                var enemyHealthManager = enemyReference.GetComponent<HealthManager>(); // get the health component off of the enemy
                if (enemyHealthManager != null) // make sure the enemy's health component is valid
                {
                    switch (enemyHealthManager.health - bulletDamage) // if the enemy is below 50 health
                    {
                        case <= 50f:
                            enemyReference.isMad = true; // the enemy is now aggressive
                            enemyReference.Aggressive();
                            break;
                        default:
                            enemyReference.isMad = false; // the enemy is now passive
                            enemyReference.Passive();
                            break;
                    }
                }
            }
        }
       
    }
}
