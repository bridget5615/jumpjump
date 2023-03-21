using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; 
    public float jumpForce;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;

    private Collider2D myCollider;
    private Animator myAnimator;
    
    // Start is called before the first frame update
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();    
        myCollider = GetComponent<Collider2D>();  
        myAnimator = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // can only jump when touching the ground
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        // myAnimator.SetBool("Grounded", grounded);
        
        if (grounded)
        {
            myAnimator.SetBool("isJumping", false);
        }
        else
        {
            myAnimator.SetBool("isJumping", true);
        }
    }
}
