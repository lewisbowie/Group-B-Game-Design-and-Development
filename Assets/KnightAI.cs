using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KnightAI : MonoBehaviour
{

    [Header("Speed of character")]
    public float speed;
    public float distance;

    [Header("Detections")]
    public Transform GroundDetection;
    public Transform WallDetection;
    public Transform Player;

    //private variables
    private Vector2 direction = new Vector2(0, 1);
    private bool moveRight = true;
    private bool Move = true;
<<<<<<< HEAD
    private Vector3 newXPosition;
    private bool CheckGround = true;
    private bool CheckWall = true;
    private float ResumeMovementCounter = 2.0f;
=======
>>>>>>> a17b0b0eef06207200a83f698e16ff3921aafc28

    [Header("Damage Options")]
    public float AttackRangeMelee;
    public int damage;
    public float DelayAttack;
    public float minDistance;
    private float lastAttack;

    [Header("Enemy Attributes")]
    public float Health;


    // Update is called once per frame
<<<<<<< HEAD
    void Update()
    {

=======
    void Update () {
       
        if (Move) 
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
       
        //uses raycast to detect when platform edge is beside the enemy.
        
        RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, distance);
>>>>>>> a17b0b0eef06207200a83f698e16ff3921aafc28

        //moves enemy along platform
        if (Move)
        {
            NormalMove();
        }

        //uses raycast to detect when platform edge is beside the enemy.
        if (CheckGround)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, distance);

            if (groundInfo.collider == false)
            {
                if (moveRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    moveRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    moveRight = true;
                }
            }

        }
        if (CheckWall)
        {
            RaycastHit2D Wall = Physics2D.Raycast(WallDetection.position, direction, distance);

            if (Wall == true)
            {
                if (Wall.collider.CompareTag("Wall"))
                {
                    //makes the enemy detect the wall with tag wall using raycast
                    Rotate();
                    speed *= -1;
                    direction *= -1;
                    moveRight = false;
                }
                if (Wall.collider.CompareTag("Player"))
                {
                    //makes the enemy detect the wall with tag wall using raycast
                    Rotate();
                    speed *= -1;
                    direction *= -1;
                    moveRight = false;
                }
            }
        }

        //Attacking the Player melee
<<<<<<< HEAD
        float distancefromPlayer = Vector2.Distance(transform.position, Player.position);
=======
        float distancefromPlayer = Vector3.Distance(transform.position, Player.position);
     
>>>>>>> a17b0b0eef06207200a83f698e16ff3921aafc28


        if (distancefromPlayer < AttackRangeMelee)
        {
            if (Time.time > lastAttack + DelayAttack)
            {
                Player.SendMessage("TakeDamage", damage);
                lastAttack = Time.time;
            }

<<<<<<< HEAD
        }
        if (distancefromPlayer < minDistance)
        {
            //sets movement and wall and ground checks to false
            Move = false;
            CheckWall = false;
            CheckGround = false;

            //calls function to move enemy towards the player
            MovetoPlayer();
        }

        else
        {
            //adds delay to the enemy going back to normal movement
            ResumeMovementCounter = Time.time;
            //if the player is far enough away from the player then the enemy will continue their normal movement
            NormalMove();
            CheckWall = true;
            CheckGround = true;
=======
            if(distancefromPlayer <= minDistance)
            {
                //logic
                //notice when player is in range
                //look at player
                //follow player
                Move = false;

                //needs work currently goes away from player on sometimes, doesnt detect what side the player is at
                transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
                // when player is out of range go back to normal movement
              
              
               
            }
          
>>>>>>> a17b0b0eef06207200a83f698e16ff3921aafc28

        }
    }
    //changes rotation of enemy when colliding with wall
    void Rotate()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void MovetoPlayer()
    {
<<<<<<< HEAD
        //moves the enemy to the player when in range
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.position.x, transform.position.y), speed * Time.deltaTime);
    }
    void NormalMove()
    {
        //the normal movement for the enemy along the platform
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
=======
      
        transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
    }
}
>>>>>>> a17b0b0eef06207200a83f698e16ff3921aafc28
