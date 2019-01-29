using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAI : MonoBehaviour {

    [Header("Speed of character")]
    public float speed;
    public float distance;

    [Header("Detections")]
    public Transform GroundDetection;
    public Transform WallDetection;
    public Transform Player;

    private Vector2 direction = new Vector2(0, 1);
    private bool moveRight = true;

    [Header("Damage Options")]
    public float AttackRangeMelee;
    public int damage;
    public float DelayAttack;
    private float lastAttack;


    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        //uses raycast to detect when platform edge is beside the enemy.
        
        RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, distance);

        if(groundInfo.collider == false)
        {
            if(moveRight == true)
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

        //Attacking the Player melee
        float distancefromPlayer = Vector3.Distance(transform.position, Player.position);

        if(distancefromPlayer < AttackRangeMelee)
        {
            if(Time.time > lastAttack + DelayAttack)
            {
                Player.SendMessage("TakeDamage", damage);
                lastAttack = Time.time;
            }
        }
   
    }
    //changes rotation of enemy when colliding with wall
    void Rotate()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
