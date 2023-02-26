using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{

    #region ALL THE VARIABLES
    public NavMeshAgent agent;

    public GameObject player, GunObj;
    public Gun gun; // g u n 
    Vector3 bulletSoundLocation = Vector3.zero;

    public LayerMask whatIsground, whatIsPlayer, whatIsSound;

    public Vector3 walkPoint;

    #region Attacking Variables 
    private float timeBetweenAttacks, passiveAttackTime, aggressiveAttackTime, timeBeingAggressive;
    bool alreadyAttacked;
    #endregion Attacking Variables

    #region State Variables
    public float sightRange, attackRange, hearingRange;
    public bool playerInSightRange, playerInAttackRange, soundInHearingRange, InvestigatingSound, isMad = false;
    #endregion State Variables

    #region patroling variables 
    bool walkPointSet;
    public float walkPointRange;
    public List<GameObject> walkpoints;
    public float walkSpeed = 5f;
    public bool usePatrolPath;
    int walkpointIndex = 0;
    public bool canLoop = true;
    #endregion patroling variables 
    #endregion ALL THE VARIABLES
    protected override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        base.Awake();
    }
    void RandomPatrol()
    {
        if (!walkPointSet) SearchWalkPoint(); // find a walkpoint
        if (walkPointSet) agent.SetDestination(walkPoint); // set location to move towards to a random walkpoint


        Vector3 distanceToWalkPoint = transform.position - walkPoint; // did we reach the walkpoint

        if (distanceToWalkPoint.magnitude <= 2f)
        {
            walkPointSet = false; // find another walkpoint
        }
    }
    void PathPatrol()
    {
        Vector3 destination = walkpoints[walkpointIndex].transform.position; // get the current index of the walkpoint current position
        agent.SetDestination(destination); // set destination to current index

        float distance = Vector3.Distance(transform.position, destination); // did we reach the current index position 
        if (distance <= 1f) // if we did and we have a next valid index than increment the index
        {
            if (walkpointIndex < walkpoints.Count - 1)
            {
                walkpointIndex++;
   
            }
            else
            {
                if (canLoop) walkpointIndex = 0; // if we reached the last index and can loop 
            }

        }
    }
    private void Patroling()
    {
        if (!usePatrolPath)
        {
            RandomPatrol(); // use random points to patrol
        }
        else
        {
            PathPatrol(); // patrol on a path
        }


    }
    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomRangeZ = Random.Range(-walkPointRange, walkPointRange); // z position 
        float randomRangeX = Random.Range(-walkPointRange, walkPointRange); // x position

        walkPoint = new Vector3(transform.position.x + randomRangeX, transform.position.y, transform.position.z + randomRangeZ); // set walk point to the result of the random generated position

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsground)) walkPointSet = true;

    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position); // set destination to player location
        }
        
        
    }

    private void AttackPlayer()
    {
        //enemy doesnt move
        agent.SetDestination(transform.position);
        InvestigatingSound = false;

        transform.LookAt(player.transform.position);
        if (!alreadyAttacked)
        {
           if (gun != null) gun.ShootBullet();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // call the reset attack function after some specified time
        }
    }

    private void ResetAttack() => alreadyAttacked = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        Invoke(nameof(UpdateComponents), 1f); // after 1 second, update the components
        passiveAttackTime = 1f;
        aggressiveAttackTime = 0.5f;
        timeBeingAggressive = 6f;
        timeBetweenAttacks = passiveAttackTime; // normal attack time
        InvestigatingSound = false; // not investigating sound
    }

    void UpdateComponents()
    {
        if (GameManager.instance != null)
        {

            GameManager.instance.enemies?.Add(this); // add ai controller to list
            player = GameManager.instance.newPawnObj;
            GunObj = GameObject.Find("TankAI");
            gun = GunObj.GetComponent<Gun>();

        }
    }

    public void Aggressive()
    {
        timeBetweenAttacks = aggressiveAttackTime; // shorten attack time
        Invoke(nameof(Passive), timeBeingAggressive); // aggression cooldown
    }
    public void Passive() => timeBetweenAttacks = passiveAttackTime;
    void HeardSound()
    {
        // if the player isn't in attack range and isn't in sight range, change Investigating sound to true, otherwise, don't investigate.
        if (!playerInAttackRange && !playerInSightRange)
        {
            InvestigatingSound = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, hearingRange); // get all colliders within sphere
            foreach (Collider collider in colliders) // for each collider in colliders
            {
                if (collider.gameObject.CompareTag("Bullet")) // is it the bullet?
                {
                    if (collider.gameObject.TryGetComponent<Bullet>(out var bulletObj)) // get the component of bullet, is it?
                    {
                        // get the bullet location and set the destination to that bullet location
                        bulletSoundLocation = bulletObj.startLocationofBullet; 
                        agent.SetDestination(bulletSoundLocation); 
                    }
                    break;
                }
            }
            var DistanceFromSound = Vector3.Distance(transform.position, bulletSoundLocation); // the distance from sound is the transform position and bullet sound location (vector range)

            if (DistanceFromSound <= 1) InvestigatingSound = false; // did we finish traveling to the bullet heard location? If so, then don't investigate.
        }
        else
        {
            InvestigatingSound = false;
        }
       
    }
    public void OnDestroy()
    {
        if (GameManager.instance != null && GameManager.instance.enemies != null)
        {
            GameManager.instance.enemies.Remove(this); // Remove this ai controller from the list
        }
    }


    // Update is called once per frame
    protected override void Update()
    {
        // booleans
        // sound = bullet passing through sphere
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // based on attack sphere
        soundInHearingRange = Physics.CheckSphere(transform.position, hearingRange, whatIsSound); // based on hearing sphere
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // based on sight sphere

        if (!InvestigatingSound && !playerInAttackRange && !playerInSightRange)
        {
            Patroling(); // Patrolling state
            Passive(); // passive state
        }

        // if the player is not within attacking range but is within sight, chase after the player

        if (!playerInAttackRange && playerInSightRange) ChasePlayer(); // Chasing state 

        // vice versa
        if (playerInAttackRange && playerInSightRange) AttackPlayer(); // attacking state

        // if the player is within hearing range or the bot is currently investigating the sound, go into investigating state
        if (soundInHearingRange || InvestigatingSound) HeardSound();  // investigating state
    }
    // draw spheres
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    
}
