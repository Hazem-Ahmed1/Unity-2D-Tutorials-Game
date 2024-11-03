using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float detectionRange = 5f;        // Range within which the enemy detects the player
    public GameObject arrowPrefab;           // Prefab for the arrow
    public Transform firePoint;              // Position from which the arrow is shot
    public float arrowSpeed = 10f;           // Speed of the arrow

    private Transform player;
    private Animator animator;
    private Vector2 lastKnownPlayerPosition;
    private bool playerInRange = false;
    private bool facingRight = true;         // Track the current facing direction

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Calculate distance between enemy and player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
            lastKnownPlayerPosition = player.position;  // Update last known position of player
            animator.SetBool("isAttacking", true);      // Start looping the attack animation
            Flip();                                     // Check if the enemy should flip to face the player
        }
        else
        {
            playerInRange = false;
            animator.SetBool("isAttacking", false);     // Stop attack animation, back to idle
        }
    }

    // Animation event method to shoot arrow
    public void ShootArrow()
    {
        // Check if player is still in range before shooting
        if (playerInRange)
        {
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = (lastKnownPlayerPosition - (Vector2)firePoint.position).normalized;
            arrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;
        }
    }
    // Flip the enemy to face the player
    void Flip()
    {
        // Check if player is on the left or right of the enemy
        if (player.position.x > transform.position.x && !facingRight)
        {
            // Player is on the right, so flip to face right
            facingRight = true;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            // Player is on the left, so flip to face left
            facingRight = false;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
