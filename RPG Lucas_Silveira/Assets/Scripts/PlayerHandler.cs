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
        // Evita loop infinito se o custo estiver inválido (ex.: não inicializado no Inspector)
        if (playerMaxExperience <= 0f)
        {
            Debug.LogWarning("playerMaxExperience estava <= 0. Definindo para 100 para evitar loop infinito.");
            playerMaxExperience = 100f;
        }

        // Sobe múltiplos níveis se necessário, com guard de segurança
        int safety = 0;
        const int maxLevelUpsPerCall = 50;
        while (playerExperience >= playerMaxExperience && safety < maxLevelUpsPerCall)
        {
            LevelUp();
            safety++;
        }

        if (safety == maxLevelUpsPerCall)
        {
            Debug.LogWarning("Limite de level-ups atingido em uma única chamada. Verifique valores de XP/custos.");
        }

    }
    void LevelUp()
    {
        float prevMax = playerMaxExperience;

        
        playerLevel++;
        playerMaxHealth += 10;
        playerHealth = playerMaxHealth;
        playerArmor++;
        playerDamage += 5;
        playerHeal += 5;
        //playerExperience = playerExperience - playerMaxExperience;
        //playerMaxExperience = playerMaxExperience*1.5f;
        
        // Primeiro desconta o custo antigo...
        playerExperience -= prevMax;

        // ...depois aumenta o custo do próximo nível
        playerMaxExperience = Mathf.Max(1f, Mathf.Ceil(prevMax * 1.5f)); // garante > 0

    }
}