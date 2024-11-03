using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool FacingRight = true;
    [HideInInspector] public bool ShouldBeDisplayedHere;
    private Animator animator;
    public float speed = 5f;
    private Rigidbody2D rb;
    private BoxCollider2D Bcollider;
    public LayerMask GroundLayer;
    [SerializeField] private float JumpValue = 6f;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Bcollider = rb.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (xInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpValue);
        }
        if(xInput > 0 && !FacingRight)
        {
            Flip();
        }
        if(xInput < 0 && FacingRight)
        {
            Flip();
        }

        if(xInput != 0)
        {
            animator.SetBool("isRunning",true);
        }
        else
        {
            animator.SetBool("isRunning",false);
        }

        JumpAnimation();
    }

    private bool isGround()
    {
        return Physics2D.BoxCast(Bcollider.bounds.center, Bcollider.bounds.size, 0f, Vector2.down, 0.1f, GroundLayer);
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        FacingRight = !FacingRight;
    }

    private void JumpAnimation()
    {
        if (!isGround())
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false );
        }
    }
}
