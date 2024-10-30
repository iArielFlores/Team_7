using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jump = 16f;
   // private bool isFacingRight = true;

    //SpeedVariables
    public float initialSpeed = 8f;
    public float runMultiplier;



    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        speed = Input.GetKey(KeyCode.LeftShift) ? initialSpeed * runMultiplier : initialSpeed;

        //Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);   
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //This was messing up the player turn

   // private void Flip()
   // {
    //    if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
      //  {
        //    isFacingRight = !isFacingRight;
      //      Vector3 localScale = transform.localScale;
        //    localScale.x *= -1f;
       //     transform.localScale = localScale;
       // }
   // }
}