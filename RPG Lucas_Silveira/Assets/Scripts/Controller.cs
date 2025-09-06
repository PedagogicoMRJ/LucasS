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
    
    //Chat
    private EnemyHandler currentEnemy;

    private ControllerEnemyMov currentEnemyMove;
    
    //private EnemyAttack currentEnemyAttackAnim; // NOVO

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
            
            currentEnemyMove = hit.transform.GetComponent<ControllerEnemyMov>();
            if (currentEnemyMove != null)
            {
                currentEnemyMove.SetInBattle(true); // Para o inimigo imediatamente
            }
            
            /* NOVO: ligar o modo batalha no Animator do inimigo
            currentEnemyAttackAnim = hit.transform.GetComponent<EnemyAttack>();
            if (currentEnemyAttackAnim != null)
            {
                currentEnemyAttackAnim.SetInBattle(true);
            }*/

            
             //Guarda referência do inimigo e assina o evento de morte
            currentEnemy = hit.transform.GetComponent<EnemyHandler>();
            if (currentEnemy != null)
            {
                currentEnemy.OnEnemyDied += HandleEnemyDied;
            }
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
    
    private void HandleEnemyDied()
    {
        EndFight();
    }


    public void EndFight()
    {
        isStartingAFight = false;
        
        /* NOVO: desligar modo batalha no Animator do inimigo
        if (currentEnemyAttackAnim != null)
        {
            currentEnemyAttackAnim.SetInBattle(false);
            currentEnemyAttackAnim = null;
        }*/

        
        // Desinscreve e limpa referência para evitar memory leaks
        if (currentEnemy != null)
        {
            currentEnemy.OnEnemyDied -= HandleEnemyDied;
            currentEnemy = null;
        }

        
        StopMovement();
    }

}