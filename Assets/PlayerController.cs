using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    public float speed = 10.0f;
    public float FlySpeed = 12.0f;
    public float JumpForce = 900.0f;
    public float flyingTime = 2f;

    //other variables
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 fly;
    private Vector2 jump;
    private Rigidbody2D controller;
    private bool canJump;
    private Vector3 flyingposition;
    private bool canFly;
    private float flyingTimer;
    



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
        canFly = false;
    }

    void Fly()
    {
        //flys character
        flyingposition = transform.position;
        flyingposition.y += 2;
        canFly = true;
        flyingTimer = 0;
        controller.isKinematic = true;
        
    }

    void Jump()
    {
        //Jumps character
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400f));
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

            //Sets jump on space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canJump)
                {
                    Jump();
                }
            }

            //sets fly on hold m
            if (Input.GetKey(KeyCode.M) && !canFly)
            { 
                Fly();
            }
            if (canFly)
            {
                transform.position = Vector3.Lerp(transform.position, flyingposition, Time.deltaTime * 5);
                flyingTimer += Time.deltaTime;

                if (flyingTimer >= flyingTime)
                {
                    canFly = false;
                    controller.isKinematic = false;
                }
            }

            // moves the player
            controller.velocity = (moveDirection);
        }
    }

    //Checks if player is grounded as to jump
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
       {
            canJump = true;
       }
   }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }
}

