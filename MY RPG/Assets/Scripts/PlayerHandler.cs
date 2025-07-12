using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHandler : MonoBehaviour
{
    public string playerName;
    public int playerLevel;
    public int playerDamage;
    public int playerHeal;
    public int playerHealth;
    public int playerMaxHealth;
    public int playerArmor;
    public float playerExperience;
    public float playerMaxExperience;
    public bool isFighting;
    Vector2 inputVector = Vector2.zero;
    Controller playerController;
    void Start()
    {
        isFighting = false;
        playerController = GetComponent<Controller>();
    }
    void Update()
    {
        if (!isFighting)
        {
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            playerController.SetInputVector(inputVector);
            isFighting = playerController.isStartingAFight;
        }
    }
    public bool TakeDamage(int damage)
    {
        Debug.Log("The player take damage");
        damage -= playerArmor;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        playerHealth -= damage;
        if (playerHealth <= 0)
            return true;
        else
            return false;
    }
    public void Heal()
    {
        Debug.Log("The Player increase her health");
        playerHealth += playerHeal;
        if (playerHealth > playerMaxHealth)
            playerHealth = playerMaxHealth;
    }
    public void GainExperience(int experience)
    {
        playerExperience += experience;
        if (playerExperience >= playerMaxExperience)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        playerLevel++;
        playerMaxHealth += 10;
        playerHealth = playerMaxHealth;
        playerArmor++;
        playerDamage += 5;
        playerHeal += 5;
        playerExperience = playerExperience - playerMaxExperience;
        playerMaxExperience = playerMaxExperience*1.5f;
    }
}