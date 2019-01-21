using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

    [Header("Player Movement")]
    public float speed = 10.0f;
    public float gravity = 30.0f;
    public float FlySpeed = 12.0f;

    //other variables
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 fly;
    private Rigidbody2D controller;

    [Header("Checks")]
    public Transform GroundCheck;
   
    void Start()
    {
        //gets the controller on start
        controller = GetComponent<Rigidbody2D>();

        // Gravity of Game object
        gameObject.transform.position = new Vector3(-18, -8, 0);

        //sets upforce to a Vector2
        fly = new Vector2(0, 19.8f * FlySpeed);
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().gravityScale = gravity;
        //checks if the player is on grounds
        if (Physics2D.OverlapCircle(GroundCheck.position, 0.2f))
        {
            //moves character on x and y axis
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0.0f);
            //changes direction character faces to last key
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetKey(KeyCode.Space))
            {
             //on spacebar moves character up, character falls if space let go
                GetComponent<Rigidbody2D>().AddForce(fly);
                GetComponent<Rigidbody2D>().gravityScale = 1;
               
            }
        }
     
        // moves the player
       controller.velocity = ( moveDirection);
    }
}
