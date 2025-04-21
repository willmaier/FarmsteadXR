using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public enum FSMStates
    {
        Idle, Attack, Dead
    }

    public FSMStates currentState;

    public float attackDistance = 10;
    public float shootRate = 3.0f;

    float distanceToPlayer;
    float elapsedTime = 0;

    public GameObject player;
    public GameObject enemyProjectile;

    public int health = 100;
    bool isDead;

    NavMeshAgent agent;

    public Transform enemyEyes;
    public float fieldOfView = 45f;
    public bool showGizmos = false;

    Vector3 startingPoint;
    Vector3 nextDestination;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isDead = false;
        agent = GetComponent<NavMeshAgent>();
        currentState = FSMStates.Idle;
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case FSMStates.Idle:
                UpdateIdleState();
                break;

            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        elapsedTime += Time.deltaTime;

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    void UpdateIdleState()
    {
        //agent.SetDestination(startingPoint);
        agent.stoppingDistance = 0;
        agent.speed = 1.5f;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
    }

    void UpdateAttackState()
    {
        nextDestination = player.transform.position;

        if(distanceToPlayer >= attackDistance)
        {
            currentState = FSMStates.Idle;
        }

        FaceTarget(nextDestination);
        EnemyAttack();
    }

    void UpdateDeadState()
    {

    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void EnemyAttack()
    {
        if (!isDead)
        {
            if (elapsedTime >= shootRate)
            {
                Invoke("ShootPumpkin", 3);
                elapsedTime = 0;
            }
        }
    }

    void ShootPumpkin()
    {
        Vector3 liftedPosition = transform.position;
        liftedPosition.y = 1;
        var proj = Instantiate(enemyProjectile, liftedPosition, transform.rotation);
        proj.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);

            Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * attackDistance);
            Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
            Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;

            Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
            Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
            Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
        }
    }
}
