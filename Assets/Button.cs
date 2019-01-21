using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [Header("Animations")]
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.Play("Door1");
            anim.Play("Door2");
            anim.Play("Door3");
        }
    }

}
