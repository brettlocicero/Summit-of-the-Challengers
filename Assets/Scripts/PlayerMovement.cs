using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerMovement : MonoBehaviour
{
    public bool adminMode = true;
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    bool doubleJumped = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
    public bool stunned;
    bool dashing;

    [SerializeField] Animator spriteAnim;
    [SerializeField] Transform playerSprites;
    [SerializeField] Transform gun;
    [SerializeField] ParticleSystem dust;
    [SerializeField] ParticleSystem dashParticles;
    [SerializeField] PhysicsMaterial2D walkingMat;
    [SerializeField] PhysicsMaterial2D fallingMat;
    [SerializeField] PhysicsMaterial2D dashingMat;
    [SerializeField] LayerMask groundLayer;

    [HideInInspector] public Vector2 rbVelocity;

    void Start ()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
    }

    void Update ()
    {
        // Movement controls
        Movement();

        // Change facing direction
        float mouseX = Input.mousePosition.x;
        if (mouseX > Screen.width / 2) 
        {
            playerSprites.localScale = Vector3.one;
            gun.localScale = new Vector3(1f, 1f, 1f);
        }

        else 
        {
            playerSprites.localScale = new Vector3(-1f, 1f, 1f);
            gun.localScale = new Vector3(1f, -1f, 1f);
        }

        // Jumping
        if (isGrounded && !dashing) 
        {
            doubleJumped = false;
            GetComponent<CapsuleCollider2D>().sharedMaterial = walkingMat;
        }

        else if (!dashing)
            GetComponent<CapsuleCollider2D>().sharedMaterial = fallingMat;

        if (isGrounded && moveDirection != 0) dust.Play();
        else dust.Stop();
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.Space) && !doubleJumped)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, 0f);
            r2d.velocity += new Vector2(0f, jumpHeight);

            doubleJumped = !doubleJumped;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing)
            StartCoroutine(Dash());
    }

    void Movement () 
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;

            if (isGrounded) 
            {
                spriteAnim.SetBool("Running", true);
                spriteAnim.SetBool("Idle", false);
                spriteAnim.SetBool("Falling", false);
            }

            else 
            {
                spriteAnim.SetBool("Running", false);
                spriteAnim.SetBool("Idle", false);
                spriteAnim.SetBool("Falling", true);
            }
        }
        
        else if (isGrounded)
        {
            moveDirection = 0;
            spriteAnim.SetBool("Running", false);
            spriteAnim.SetBool("Idle", true);
            spriteAnim.SetBool("Falling", false);
        }

        else 
        {
            moveDirection = 0;
            spriteAnim.SetBool("Running", false);
            spriteAnim.SetBool("Idle", false);
            spriteAnim.SetBool("Falling", true);
        }
    }

    void FixedUpdate ()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius, groundLayer);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        rbVelocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Apply movement velocity
        if (!stunned && !dashing)
            r2d.velocity = rbVelocity;

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    IEnumerator Dash () 
    {
        dashing = true;
        GetComponent<CapsuleCollider2D>().sharedMaterial = dashingMat;
        r2d.gravityScale = 0f;
        r2d.velocity = new Vector2(r2d.velocity.x, 0f);
        r2d.AddForce(new Vector2((moveDirection) * maxSpeed * 10f, 0f), ForceMode2D.Impulse);
        dashParticles.Play();
        
        yield return new WaitForSeconds(0.1f);

        dashParticles.Stop();
        r2d.gravityScale = gravityScale;
        dashing = false;
    }
}