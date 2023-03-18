using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 4f;


    [Header("Components")]
    public Rigidbody2D Rb; 
    public LayerMask groundLayer;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLine = 2;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check 
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLine, groundLayer);

        // Inputs 
        bool space = Input.GetKey(KeyCode.Space);
        bool rightKey = Input.GetKey(KeyCode.RightArrow); 
        bool leftKey = Input.GetKey(KeyCode.LeftArrow); 

        if (rightKey)
        {
            Debug.Log("Right Key Pressed");
            Rb.AddForce(Vector2.right, ForceMode2D.Impulse);
        }

        if (leftKey)
        {
            Debug.Log("Left Key Pressed");
            Rb.AddForce(Vector2.left, ForceMode2D.Impulse);
        }

        if (space && onGround)
        {
            Debug.Log("Space Key Pressed");
            Rb.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
        }

        // Animation
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (onGround)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLine);
    }
}
