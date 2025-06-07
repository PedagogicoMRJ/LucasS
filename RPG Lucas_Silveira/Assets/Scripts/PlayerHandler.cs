using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    Vector2 inputVector = Vector2.zero;
    Controller playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        playerController.SetInputVector(inputVector);
    }

    public bool TakeDamage(int damage)
    {
        Debug.Log("The player take damage");
        playerHealth -= damage;
        if (playerHealth <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        Debug.Log("The Player increase her health");
        playerHealth += amount;
        if (playerHealth > playerMaxHealth)
            playerHealth = playerMaxHealth;
    }
}
