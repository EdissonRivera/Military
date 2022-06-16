using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.AI;
public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField]
    private float walkPointRange;
    [SerializeField]
    private float health;
    [SerializeField]
    private Image imgHealth;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public GameObject projectile;
    public Transform shooting;
    public bool Soldier;

    //State
    public float sightRange, attackRange;
    public bool playerInsightRange, playerInAttackRange;

    //Rewarded
    public GameObject Coint;
    public Animator _animator;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInsightRange && !playerInAttackRange) Patroling();
        if (playerInsightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point int range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

        _animator.SetBool("Run", true);

        if (Soldier)
            _animator.SetBool("Shoot", false);


    }
    private void AttackPlayer()
    {
        if (Soldier)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            if (!alreadyAttacked)
            {
                //attack code here
                //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                Rigidbody rb = Instantiate(projectile, shooting.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
                rb.AddForce(transform.up * 5f, ForceMode.Impulse);
                _animator.SetBool("Shoot", true);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        else
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player);
        }
        
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            imgHealth.fillAmount = health / 100;

            if (health <= 0)
            {
                Instantiate(Coint, transform.position, Quaternion.identity);
                Invoke(nameof(DestroyEnemy), 0.5f);
            }
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



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            TakeDamage(10);
            Debug.Log("playerShoot");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "FireDamage")
        {
            TakeDamage(2);

        }
    }

}
