using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWarriorAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Points to patrol between
    public float speed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public float waitTimeAtPatrolPoint = 1f; // Time to wait at each patrol point

    private Transform player;
    private int currentPatrolIndex;
    private Animator animator;
    private float lastAttackTime;
    private bool isWaiting = false;

    private CircleCollider2D attackHitBox;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        currentPatrolIndex = 0;
        animator.SetBool("isWalking", true);
        attackHitBox = GetComponentInChildren<CircleCollider2D>();
        attackHitBox.enabled = false;
    }   

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // Player detected
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            // Patrol only if not waiting
            if (!isWaiting)
            {
                Patrol();
            }
        }
    }

    void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        MoveTowards(targetPoint);

        // Check if the enemy is close enough to the target point
        if (Vector2.Distance(transform.position, targetPoint.position) < 1f)
        {
            StartCoroutine(WaitAtPatrolPoint());
        }
    }

    void ChasePlayer(float distanceToPlayer)
    {
        if (distanceToPlayer < attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer >= attackRange)
        {
            // Reset attacking state if out of attack range
            animator.SetBool("isAttacking", false); 
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);

            MoveTowards(player);
        }
    }

    void MoveTowards(Transform target)
    {
        // Move towards the target
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Flip the enemy sprite based on direction
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }
    }

    private IEnumerator WaitAtPatrolPoint()
    {
        isWaiting = true;
        animator.SetBool("isWalking", false); // Stop walking animation
        animator.SetBool("isRunning", false); // Stop running animation

        yield return new WaitForSeconds(waitTimeAtPatrolPoint); // Wait for a specified time

        // Move to the next patrol point
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        animator.SetBool("isWalking", true); // Start walking again
        isWaiting = false;
    }

    void AttackPlayer()
    {
        lastAttackTime = Time.time;
        animator.SetBool("isRunning", false); // Stop running animation
        animator.SetBool("isAttacking", true); // Trigger attack animation
    }

    // Draw Gizmos for visualization
    private void OnDrawGizmos()
    {
        // Draw patrol points
        if (patrolPoints != null)
        {
            Gizmos.color = Color.blue; // Color for patrol points
            foreach (Transform point in patrolPoints)
            {
                Gizmos.DrawSphere(point.position, 0.2f); // Draw a small sphere at each patrol point
            }
        }

        // Draw detection range
        Gizmos.color = Color.yellow; // Color for detection range
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Draw a wire sphere for detection range

        // Draw attack range
        Gizmos.color = Color.red; // Color for attack range
        Gizmos.DrawWireSphere(transform.position, attackRange); // Draw a wire sphere for attack range
    }


    public void Attack()
    {
        attackHitBox.enabled = true;
    }

    public void DisableAttack()
    {
        attackHitBox.enabled = false;
    }

}
