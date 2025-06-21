using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyLevel;
    public Slider enemyHealth;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerLevel;
    public Slider playerHealth;
    private void Start()
    {
        gameObject.SetActive(false);
       
    }
    public void SetHUD(EnemyHandler enemyStatus, PlayerHandler playerStatus)
    {
        enemyName.text = enemyStatus.enemyName;
        enemyLevel.text = "LVL " + enemyStatus.enemyLevel;
        enemyHealth.maxValue = enemyStatus.enemyMaxHealth;
        enemyHealth.value = enemyStatus.enemyHealth;
        playerName.text = playerStatus.playerName;
        playerLevel.text = "LVL " + playerStatus.playerLevel;
        playerHealth.maxValue = playerStatus.playerMaxHealth;
        playerHealth.value = playerStatus.playerHealth;
    }
    public void SetHP(int enemyHP, int playerHP)
    {
        enemyHealth.value = enemyHP;
        playerHealth.value = playerHP;
    }
}
