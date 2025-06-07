using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    
    private Rigidbody2D rig;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent <Rigidbody2D>();
        anim = GetComponent <Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 movementH = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        Vector3 movementV = new Vector3(0f, Input.GetAxis("Vertical"), 0f);
        transform.position += movementV * speed * Time.deltaTime;
        transform.position += movementH * speed * Time.deltaTime;

        if(Input.GetAxis("Horizontal") != 0f){
            if(Input.GetAxis("Horizontal") < 0f)
            {
                anim.SetBool("Left", true);
                anim.SetBool("Right", false);
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
            }
            else
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", true);
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
            }
        }

        if(Input.GetAxis("Vertical") != 0f){
            if(Input.GetAxis("Vertical") > 0f)
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
                anim.SetBool("Down", false);
                anim.SetBool("Up", true);
            }
            else
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            GameManager.access.GameOver();
            Destroy(gameObject);
        }
    }
}
