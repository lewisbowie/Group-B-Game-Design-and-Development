using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public GameObject Collectible;
    public float Range;
    public Transform Player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distancefromPlayerChest = Vector2.Distance(transform.position, Player.position);
        if (gameObject.tag == "Player")
        {
            if (distancefromPlayerChest < Range)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {

                    Instantiate(Collectible, transform.position, transform.rotation);
                    print("ev");
                }

            }
        }
    


    }

}
 
