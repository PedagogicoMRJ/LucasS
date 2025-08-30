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
    //private Animator enemyAnim;
    
    //chat
    public event Action OnEnemyDied;
    
    void Start()
    {
        isEnemyDead = false;
        //enemyAnim = GetComponentInChildren<Animator>();
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
