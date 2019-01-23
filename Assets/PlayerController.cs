using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    public float speed = 10.0f;
    public float FlySpeed = 12.0f;
    public float JumpForce = 900.0f;
    public int JumpHeight;

    //other variables
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 fly;
    private Vector2 jump;
    private Rigidbody2D controller;
    private bool canJump;



    [Header("Checks")]
    public Transform GroundCheck;

    void Start()
    {
        //gets the controller on start
        controller = GetComponent<Rigidbody2D>();

        // Gravity of Game object
        gameObject.transform.position = new Vector3(-18, -8, 0);

        //Declares Fly as Vector2
        fly = new Vector2(0, 19.8f * FlySpeed);

        //Declares jump force
        jump = Vector2.up * JumpForce;
        canJump = false;
    }

    void Fly()
    {
        //flys character
        GetComponent<Rigidbody2D>().AddForce(fly);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    void Jump()
    {
        //Jumps character

        canJump = true;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 900f));
    }
   
        

    void Update()
    {
        
        //checks if the player is on grounds
        if (Physics2D.OverlapCircle(GroundCheck.position, 0.2f))
        {
            //moves character on x and y axis
            moveDirection = new Vector2(Input.GetAxis("Horizontal"), controller.velocity.y);
            //changes direction character faces to last key
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            //Sets jump on space and fly on double space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }



            // moves the player
            controller.velocity = (moveDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
       {
            canJump = false;
        }
   }
}

