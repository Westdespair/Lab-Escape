using UnityEngine;
using UnityEngine.AI;

// Class to see how to implement a smarter AI, 
// Tutorial fetched from: https://www.youtube.com/watch?v=UjkSFoLxesw&ab_channel=Dave%2FGameDevelopment

// Will comment the code more, but as for now only the main parts has comments

public class SmarterAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private Animator animator;

    public GameObject weaponSlot;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponSlot.GetComponent<Weapon>().SetMode(Weapon.WeaponMode.InEnemyHand);
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if (health <= 0)
        {
            die();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    public void die()
    {
        animator.SetBool("isDead", true);
        agent.enabled = false;
        weaponSlot.GetComponent<Weapon>().SetMode(Weapon.WeaponMode.Dropped);
        weaponSlot = null;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<LifeTIme>().enabled = true;
        gameObject.GetComponent<SmarterAI>().enabled = false;
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        //transform.rotation.SetEulerAngles(0, transform.LookAt(player).y,  ;
        //transform.LookAt(player);
        Vector3 targetPostition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPostition);
        if (!alreadyAttacked){
            ///Attack code here
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            if(weaponSlot != null)
            {
                weaponSlot.GetComponent<Weapon>().FireWeapon();
                weaponSlot.GetComponent<Weapon>().SetMode(Weapon.WeaponMode.InEnemyHand);
                weaponSlot.transform.localPosition = weaponSlot.GetComponent<Weapon>().basePosition;
                weaponSlot.transform.localEulerAngles = new Vector3(-90, 180, -2);
                weaponSlot.GetComponent<Weapon>().resetBasePosition();
            }

            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
