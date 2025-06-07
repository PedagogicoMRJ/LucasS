using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool isStartingAFight;
    public float speed = 5;
    Vector2 movement;
    Rigidbody2D bodyRig;
    Animator anim;
    public LayerMask interactLayer;
    public float interactRadious;
    // Start is called before the first frame update
    void Start()
    {
        isStartingAFight = false;
        bodyRig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartingAFight)
        {
            isStartingAFight = false;
        }
        InteractableObj();
        Movement();
    }

    public void SetInputVector(Vector2 inputVector)
    {
        movement.x = inputVector.x;
        movement.y = inputVector.y;
    }

    void Movement()
    {
        if (movement.x != 0 && movement.y != 0)
        {
            bodyRig.velocity = new Vector2(movement.x, movement.y) * speed * 0.7f;
        }
        else
        {
            bodyRig.velocity = new Vector2(movement.x, movement.y) * speed;
        }
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Magnitude", movement.magnitude);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadious);
    }

    public void InteractableObj()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRadious, interactLayer);
        if (hit != null)
        {
            IInteractable obj = hit.transform.GetComponent<IInteractable>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (obj == null) return;
                if (hit.CompareTag("Enemy"))
                {
                    Debug.Log("The Heroine found an Enemy");
                    isStartingAFight = true;
                }
                obj.Interact();
            }
        }
    }
}
