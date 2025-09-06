using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyHandler : MonoBehaviour, IInteractable
{
    public bool isBoss;
    public bool IsInteractable => true;
    public bool isEnemyDead;
    public bool isArmored = false;
    public string enemyName;
    public int enemyLevel;
    public int enemyHealth;
    public int enemyMaxHealth;
    public int enemyHeal;
    public int enemyDamage;
    public int enemyExperience;
    public int enemyArmor;
    
    //chat
    public event Action OnEnemyDied;
    
    [SerializeField] private Animator animator;
    [SerializeField] private float attackInterval = 1.5f; // tempo entre ataques

    bool inBattle;
    private Coroutine attackRoutine;
    private static readonly int AttackTriggerHash = Animator.StringToHash("AttackTrigger");

    
    void Start()
    {
        isEnemyDead = false;
        if (animator == null)
            animator = GetComponent<Animator>();
    }
    
    // Chame quando a batalha começar/terminar
    public void SetInBattle(bool value)
    {
        if (inBattle == value) return;
        inBattle = value;

        if (inBattle && !isEnemyDead)
        {
            if (attackRoutine != null) StopCoroutine(attackRoutine);
            attackRoutine = StartCoroutine(AttackLoop());
        }
        else
        {
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                attackRoutine = null;
            }
        }
    }
    
    private IEnumerator AttackLoop()
    {
        while (inBattle && !isEnemyDead)
        {
            EnemyAttack();
            yield return new WaitForSeconds(attackInterval);
        }
        attackRoutine = null;
    }


    public void EnemyAttack()
    {
        if (isEnemyDead) return;

        if (animator == null)
        {
            Debug.LogWarning("EnemyAttack called but Animator is missing.");
            return;
        }

        // Ativa a animação de ataque
        animator.SetTrigger("AttackTriggerHash");
    }
    public bool TakeDamage(int damage)
    {
        Debug.Log("The enemy take damage");
        damage -= enemyArmor;
        enemyHealth -= damage;
        if (isArmored)
        {
            enemyArmor -= 10;
            isArmored = false;
            Debug.Log("The Armor was Broken");
        }
        if (enemyHealth <= 0)
        {
            EnemyDie();
            return true;
        }
        else
            return false;
    }

    void EnemyDie()
    {
        Debug.Log("The Enemy died");
        
        // Notifica quem estiver ouvindo (ex.: Controller) que o inimigo morreu
        OnEnemyDied?.Invoke();

        
        Destroy(gameObject, 2f);
        StopAllCoroutines();
    }

    public void Interact()
    {
        Debug.Log("The enemy is ready to fight");
        gameObject.tag = "isFighting";
    }
    public void Interactable()
    {
        Debug.Log("The enemy is ready to fight");
        gameObject.tag = "isFighting";
    }
    public void Heal()
    {
        Debug.Log("The Enemy increase her health");
        enemyHealth += enemyHeal;
        if (enemyHealth > enemyMaxHealth)
            enemyHealth = enemyMaxHealth;
    }
    public void Armor()
    {
        if (!isArmored)
        {
            enemyArmor += 10;
            isArmored = true;
            Debug.Log("The Enemy increase her Armor");
        }
        else
        {
            Heal();
        }
    }
}
