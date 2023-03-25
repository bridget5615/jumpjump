using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class ArduinoScript : MonoBehaviour
{
    public float moveSpeed; 
    private float amountToMove;

    SerialPort sp = new SerialPort("/dev/tty.usbmodem1301", 9600);

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
        sp.Open();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        amountToMove = moveSpeed * Time.deltaTime;

        if (sp.IsOpen)
        {
            try
            {
                MoveObject(sp.ReadByte());
                print(sp.ReadByte());
            }
            catch(System.Exception)
            {
            }
        }
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        // myAnimator.SetBool("Grounded", grounded);
    }

    void MoveObject(int Direction)
    {

        if (Direction == 14)
        {
            myAnimator.SetBool("isJumping", true);
            transform.Translate(Vector3.left * amountToMove, Space.World);

        }
        if (Direction == 0)
        {
            myAnimator.SetBool("isJumping", false);
        }
    }
}