using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum BattleStage { WATING, START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleStage stage;
    BattleHUD battleHUD;
    EnemyHandler enemyUnit;
    PlayerHandler playerUnit;
    public GameObject specialButton;
    public bool isFighting;
    void Awake()
    {
        isFighting = false;
        stage = BattleStage.WATING;
        battleHUD = GameObject.FindGameObjectWithTag("BattleHUD").GetComponent<BattleHUD>();
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        specialButton.SetActive(false);
    }
    private void Update()
    {
        if (!isFighting)
        {
            isFighting = playerUnit.isFighting;
            if (isFighting)
            {
                StartFight();
            }
        }
    }
    public void StartFight()
    {
        Debug.Log("The fight began");
        stage = BattleStage.START;
        battleHUD.gameObject.SetActive(true);
        StartCoroutine(SetupBattle());
    }
    IEnumerator SetupBattle()
    {
        enemyUnit = GameObject.FindGameObjectWithTag("isFighting").GetComponent<EnemyHandler>();
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        battleHUD.SetHUD(enemyUnit, playerUnit);
        yield return new WaitForSeconds(1f);
        stage = BattleStage.PLAYERTURN;
        PlayerTurn();
    }
    void PlayerTurn()
    {
        Debug.Log("Choose an action");
    }
    public void AttackButton()
    {
        if (stage != BattleStage.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(playerUnit.playerDamage));
    }
    public void SpecialAttackButton()
    {
        if (stage != BattleStage.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(playerUnit.playerDamage*2));
    }
    public void HealButton()
    {
        if (stage != BattleStage.PLAYERTURN)
            return;
        StartCoroutine(PlayerHeal());
    }
    IEnumerator PlayerAttack(int damage)
    {
        Debug.Log("The Player attack the Enemy");
        bool isDead = enemyUnit.TakeDamage(damage);
        yield return new WaitForSeconds(1f);
        battleHUD.SetHP(enemyUnit.enemyHealth, playerUnit.playerHealth);
        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            stage = BattleStage.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            Debug.Log("Enemy Turn");
            stage = BattleStage.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator PlayerHeal()
    {
        Debug.Log("The Player heal herself");
        playerUnit.Heal();
        yield return new WaitForSeconds(1f);
        battleHUD.SetHP(enemyUnit.enemyHealth, playerUnit.playerHealth);
        yield return new WaitForSeconds(1f);
        Debug.Log("Enemy Turn");
        stage = BattleStage.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator EnemyTurn() 
    {
        Debug.Log("The Enemy attack the Player");
        //bool isBoss = enemyUnit.isBoss;
        yield return new WaitForSeconds(1f);
        int enemyAction = 0;
        /*if(isBoss)
        {
            enemyAction = Random.Range(1, 4);
            Debug.Log(enemyAction);
        }
        else
        {
            enemyAction= 1;
        }*/
        if(enemyAction == 1)
        {
            bool isDead = playerUnit.TakeDamage(enemyUnit.enemyDamage);
            yield return new WaitForSeconds(1f);
            battleHUD.SetHP(enemyUnit.enemyHealth, playerUnit.playerHealth);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                Debug.Log("You died");
                stage = BattleStage.LOST;
                StartCoroutine(EndBattle());
            }
            else
            {
                Debug.Log("Player Turn");
                stage = BattleStage.PLAYERTURN;
                PlayerTurn();
            }
        }
        /*else if(enemyAction == 2)
        {
            Debug.Log("The Enemy heal herself");
            enemyUnit.Heal();
            yield return new WaitForSeconds(1f);
            battleHUD.SetHP(enemyUnit.enemyHealth, playerUnit.playerHealth);
            yield return new WaitForSeconds(1f);
            Debug.Log("Player Turn");
            stage = BattleStage.PLAYERTURN;
            PlayerTurn();
        }
        else if (enemyAction == 3)
        {
            Debug.Log("The Enemy armor herself");
            enemyUnit.Armor();
            yield return new WaitForSeconds(1f);
            battleHUD.SetHP(enemyUnit.enemyHealth, playerUnit.playerHealth);
            yield return new WaitForSeconds(1f);
            Debug.Log("Player Turn");
            stage = BattleStage.PLAYERTURN;
            PlayerTurn();
        }*/
        else
        {
            Debug.Log("Someting get wrong");
            yield return new WaitForSeconds(3f);
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator EndBattle()
    {
        if(stage == BattleStage.WON)
        {
            Debug.Log("You Won the Battle");
            playerUnit.isFighting = false;
            isFighting = false;
            playerUnit.GainExperience(enemyUnit.enemyExperience);
            if (playerUnit.playerLevel == 10)
            {
                EnableSpecialAttack();
            }
        }
        else if(stage == BattleStage.LOST)
        {
            Debug.Log("You Lost the Battle");
            playerUnit.isFighting = false;
            isFighting = false;
        }
        yield return new WaitForSeconds(1f);
        stage = BattleStage.WATING;
        battleHUD.gameObject.SetActive(false);
    }
    void EnableSpecialAttack()
    {
        specialButton.SetActive(true);
    }
}
