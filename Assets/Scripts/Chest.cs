using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    //public GameObject Collectible;
    public float Range;
    public Transform Player;
    public GameObject[] objects;
    public string tagName;
    public Transform SpawnPoint;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        GameObject Collectible = Instantiate(objects[Random.Range(0, objects.Length)], SpawnPoint.position, SpawnPoint.rotation) as GameObject;
    }

   //void OnTriggerEnter2D(Collider2D other)
   // {
       // if(other.gameObject.tag == tagName)
       // {
        //    OpenChest();
        //    print("chest opened");
       // }
   // }
}
 
