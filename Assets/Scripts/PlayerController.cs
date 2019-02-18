using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    public float speed = 10.0f;
    public float FlySpeed = 12.0f;
    public float JumpForce = 10.0f;
    public float flyingTime = 2f;

    //other variables
    //private Vector2 moveDirection = Vector2.zero;
    private Vector2 fly;
    private Vector2 jump;
    private Rigidbody2D controller;
    private bool canJump;
    private Vector3 flyingposition;
    private bool canFly;
    private float flyingTimer;
    private int pickedUp;
    private float moveDirection;
    private bool facingRight;

    public Animator PlayerTakesDamage;

    [Header("Enemy")]
    public Transform Knight;

    [Header("Gem Activations")]
    public int JumpActivates;
    public int FlyActivates;
    public int BreathAttackActivates;

    [Header("Player Attributes")]
    public float Health;

    [Header("Melee Damage Options")]
    public float AttackRangeMelee;
    public int MeleeDamage;
    public int BreathDamage;
    public float DelayAttack;
    private float lastAttackMelee;
    public float AttackRangeBreath;
    private float LastAttackBreath;
    public Animator anim; 

    public Text CollectibleNumber;



    [Header("Checks")]
    public Transform GroundCheck;

    void Start()
    {
        //gets the controller on start
        controller = GetComponent<Rigidbody2D>();

      
        facingRight = true;

        // Gravity of Game object
        gameObject.transform.position = new Vector3(-30, 1, 0);

        //Declares Fly as Vector2
        fly = new Vector2(0, 19.8f * FlySpeed);

        //Declares jump force
        jump = Vector2.up * JumpForce;
        canJump = false;
        canFly = false;

        CollectiblesCollected();
    }

    void Fly()
    {
        //flys character
      
          canFly = true;
          flyingTimer = 0;
        controller.gravityScale = 0.0f;
    }

    void Jump()
    {
        //Jumps character
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500f));
        GetComponent<Rigidbody2D>().AddForce(jump);

    }

   void FixedUpdate()
    {
        moveDirection = Input.GetAxis("Horizontal");
        controller.velocity = new Vector2(moveDirection * speed, controller.velocity.y);
        anim.SetFloat("Speed", controller.velocity.x);

    }

    void CollectiblesCollected()
    {
       
       // CollectibleNumber.text = "Collectibles: " + pickedUp;
    }

   public void Update()
    {
        CollectiblesCollected();
        //checks if the player is on grounds
        if (Physics2D.OverlapCircle(GroundCheck.position, 0.2f))
        {
          

            //changes direction character faces to last key
            if (moveDirection > 0 && !facingRight || moveDirection < 0 && facingRight)
            {
                facingRight = !facingRight;
                Rotate();
            }
        }

        if (pickedUp >= JumpActivates)
        {


            //Sets jump on space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canJump)
                {
                    Jump();
                    controller.gravityScale = 5.0f;
                }
            }
        }

        //sets fly on hold m
        if (pickedUp >= FlyActivates)
        {
            if (Input.GetKeyDown(KeyCode.M) && !canFly)
            {
                Fly();
            }

            //returns if M is released flying stops
            if (Input.GetKeyUp(KeyCode.M))
            {
                canFly = false;
                controller.gravityScale = 5.0f;
            }

            if (canFly)
            {
                //  flyingposition.x = moveDirection.x *speed * Time.deltaTime;
                flyingposition = transform.localPosition;
                flyingposition.y += 2;

                transform.position = Vector3.Lerp(transform.position, flyingposition, Time.deltaTime * 2);
                flyingTimer += Time.deltaTime;

                if (flyingTimer >= flyingTime)
                {
                    canFly = false;
                    controller.gravityScale = 5.0f;
                }
            }
        }
       
        //Melee Attack

        if(Input.GetKeyDown(KeyCode.Z))
        {
            MeleeAttack();
           
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            BreathAttack();
        }
       
        }
    


    //Checks if player is grounded as to jump
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
       {
            canJump = true;
           // canFly = true;
           
       }
   }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
           // canFly = false;
        }
    }

    //Adds collectible pick up
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Collectible")
        {
            collider.gameObject.SetActive(false);
            pickedUp += 1;
        }
    }
 
    public void TakeDamage(int damage)
    {
        Health -= damage;
        PlayerTakesDamage.Play("PlayerTakesDamage");
        if(Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public void MeleeAttack()
    {
        float distancefromPlayerMelee = Vector2.Distance(transform.position, Knight.position);

        if(GameObject.FindGameObjectWithTag("Enemy"))
        {
            if (distancefromPlayerMelee < AttackRangeMelee)
            {

                Knight.SendMessage("TakeDamage", MeleeDamage);
                print("melleee");
                lastAttackMelee = Time.time;
            }
        }
       
    }

    public void BreathAttack()
    {
        float distancefromPlayerBreath = Vector2.Distance(transform.position, Knight.position);

        if(GameObject.FindGameObjectWithTag("Enemy"))
        {
            if(distancefromPlayerBreath < AttackRangeBreath)
            {
                Knight.SendMessage("TakeDamage", BreathDamage);
                LastAttackBreath = Time.time;
            }
        }
    }
    void Rotate()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
} 

