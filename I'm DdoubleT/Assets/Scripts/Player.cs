using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    // variável de virada do sprite
    private bool facingRight = true;

    // Variáveis correspondentes a gravidade quanto ao chão do personagem 
    private bool onGround;
    public Transform groundCheck;

    //pulo
    private bool jump = false;
    private bool doubleJump;
    public float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {        
        Move();
        Jump();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Ground")
        {
            onGround = true;
        }
        else{
            onGround = false;
        }
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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        transform.position += movement * Time.deltaTime * speed;

        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            onGround = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
