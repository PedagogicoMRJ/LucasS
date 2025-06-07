using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool isPlayer1;
    public float velocity = 5;
    Vector2 movement;
    Rigidbody2D bodyRig;
    private Animator anim;
    float move;
    // Start is called before the first frame update
    void Start()
    {
        bodyRig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isPlayer1)
        {
            if(movement.x !=0 && movement.y !=0)
            {
                bodyRig.velocity = new Vector2(movement.x, movement.y) * velocity * 0.7f;
            }
            else
            {
                bodyRig.velocity = new Vector2(movement.x, movement.y) * velocity;
            }
            anim.SetFloat("Horizontal1", movement.x);
            anim.SetFloat("Vertical1", movement.y);
                //move = Input.GetAxis("Horizontal1");
                anim.SetFloat("Magnitude", movement.magnitude);

        }
        else
        {
            if(movement.x !=0 && movement.y !=0)
            {
                bodyRig.velocity = new Vector2(movement.x, movement.y) * velocity * 0.7f;
            }
            else
            {
                bodyRig.velocity = new Vector2(movement.x, movement.y) * velocity;
            }
            anim.SetFloat("Horizontal2", movement.x);
            anim.SetFloat("Vertical2", movement.y);
            //move = Input.GetAxis("Horizontal2");
            anim.SetFloat("Magnitude", movement.magnitude);
        }

        
    }

    public void SetInputVector(Vector2 inputVector)
    {
        movement.x = inputVector.x;
        movement.y = inputVector.y;
    }
}
