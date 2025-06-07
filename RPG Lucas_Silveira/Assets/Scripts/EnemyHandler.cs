using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour, IInteractable
{
    public bool isEnemyDead;
    public string enemyName;
    public int enemyLevel;
    public int enemyHealth;
    public int enemyMaxHealth;
    public int enemyDamage;
    private Animator enemyAnim;

    void start()
    {
        isEnemyDead = false;
        enemyAnim = GetComponentInChildren<Animator>();
    }

    public bool TakeDamage(int damage)
    {
        Debug.Log("The enemy take damage");
        enemyHealth -= damage;
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
        Destroy(gameObject, 2f);
    }

    public void Interact()
    {
        Debug.Log("The enemy is ready to fight");
        gameObjct.tag = "isFighting";
    }
    public void Interactable()
    {
        Debug.Log("The enemy is ready to fight");
        gameObject.tag = "isFighting";
    }
    
}
