using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float interactRadius;
    
    public bool isStartingAFight;
    private Vector2 movement;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private Vector2 velocityVector;

    private void Start()
    {
        isStartingAFight = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        velocityVector = Vector2.zero;
    }

    private void Update()
    {
        CheckInteractions();
        
        if (isStartingAFight)
        {
            StopMovement();
            return;

        }
        
        HandleMovement();
    }

    private void HandleMovement()
    {
        velocityVector.Set(movement.x, movement.y);
        float multiplier = (movement.x != 0 && movement.y != 0) ? 0.7f : 1f;
        rigidbody2D.velocity = velocityVector * speed * multiplier;
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }

    private void CheckInteractions()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayer);
        if (hit == null || !Input.GetKeyDown(KeyCode.E)) return;

        IInteractable interactable = hit.transform.GetComponent<IInteractable>();
        if (interactable == null) return;

        if (hit.CompareTag("Enemy"))
        {
            Debug.Log("The Heroine found an Enemy");
            isStartingAFight = true;
        }
        
        interactable.Interact();
    }

    public bool IsStartingFight => isStartingAFight;

    public void SetInputVector(Vector2 inputVector)
    {
        movement = inputVector;
    }
    
    private void StopMovement()
    {
        movement = Vector2.zero;
        rigidbody2D.velocity = Vector2.zero;
        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", 0f);
        animator.SetFloat("Magnitude", 0f);
    }

    public void EndFight()
    {
        isStartingAFight = false;
        StopMovement();
    }

}