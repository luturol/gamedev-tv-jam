using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAgentScript : MonoBehaviour
{
    // velocidade do inimigo
    private float movement = 2;

    // variável de virada do sprite
    private bool facingRight = true;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(movement, 0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            Flip();
        }
        else {
            Invoke("TurnOffGameObject", 0.8f);

            anim.Play("Enemy_Die");
        }
    }

    // inverter o sprite baseado na scale 
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;

        movement *= -1;
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
