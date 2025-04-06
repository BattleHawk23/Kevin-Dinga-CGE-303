using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    public AudioClip jumpSound;
    private AudioSource playerAudio;
    public AudioClip coinSound;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }

        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            playerAudio.PlayOneShot(jumpSound, 1.0f);

        }
    
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        animator.SetBool("onGround", isGrounded);

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void PlayCoinSound()
    {
        playerAudio.PlayOneShot(coinSound, 1.0f);
    }

}
