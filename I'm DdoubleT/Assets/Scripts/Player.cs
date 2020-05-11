using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Animator animator;
    // variável de virada do sprite
    private bool facingRight = true;

    // Variáveis correspondentes a gravidade quanto ao chão do personagem 
    public bool isOnGround;

    //pulo
    private bool jump = false;
    private bool doubleJump = false;
    public float jumpForce;

    private bool canMove = false;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (canMove && isOnGround)
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    // inverter o sprite baseado na scale 
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }

    void Move()
    {
        // Vector 3 aceita a motion gradativa de x
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        // essa combinação transforma o movimento no que chamam de movimento smooth
        transform.position += movement * Time.deltaTime * speed;

        if (movement.x != 0 && canMove && isOnGround)
        {
            canMove = true;
            animator.SetBool("canRun", canMove);
            if (movement.x > 0 && !facingRight)
                Flip();
            else if (movement.x < 0 && facingRight)
                Flip();
        }
        else
        {
            canMove = false;
            animator.SetBool("canRun", canMove);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            isOnGround = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.gameObject.tag == "Ground")     
        {
            isOnGround = true;
            canMove = true;
        } 
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    // void OnCollisionExit2D(Collision2D other)
    // {        
    //     if (other.gameObject.tag == "Ground")
    //         isOnGround = false;
    // }
}
