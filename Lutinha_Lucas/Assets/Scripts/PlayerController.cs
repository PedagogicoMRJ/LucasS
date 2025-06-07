using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    WaitForSeconds threeSec;
    public bool isPlayable;
    int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    public bool isPlayer1;
    bool isAttack = false;
    bool isKnockback = false;
    string attack = "None";
    public float speed;
    private Animator anim;
    private Rigidbody2D rig;
    float move;
    // Start is called before the first frame update
    void Start()
    {
        threeSec = new WaitForSeconds(3);
        isPlayable = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        anim = GetComponentInChildren<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(currentHealth <= 0)
        {
            anim.SetBool("Defeat", true);
            StartCoroutine("Ending");
            SceneManager.LoadScene(0);
        }

        if(isKnockback)
        {
            anim.SetBool("Knockback", false);
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                isKnockback = false;
            }
        }

        if (isAttack)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                isAttack = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(currentHealth > 0)
        {
        
            if(!isPlayable)
            {
                isPlayable = healthBar.finishCountDown;
            }
            else
            {

                if(!isAttack && !isKnockback)
                {
                    Movement();
                    Attack();
                }
            }
        }
    }


    void Movement()
    {
        if(isPlayer1)
        {
            move = Input.GetAxis("Horizontal1");
        }
        else
        {
            move = Input.GetAxis("Horizontal2");
        }

        rig.velocity = new Vector2(move * speed, 0.0f);
        anim.SetFloat("Walk", move);
    }

    void Attack()
    {
        if(isPlayer1)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                attack = "LightPunch";
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                attack = "HeavyPunch";
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                attack = "LightKick";
            }
            else if(Input.GetKeyDown(KeyCode.R))
            {
                attack = "HeavyKick";
            }
            else
            {
                attack = "None";
                
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Keypad4))
            {
                attack = "LightPunch";
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                attack = "HeavyPunch";
            }
            else if(Input.GetKeyDown(KeyCode.Keypad1))
            {
                attack = "LightKick";
            }
            else if(Input.GetKeyDown(KeyCode.Keypad2))
            {
                attack = "HeavyKick";
            }
            else
            {
                attack ="None";
            }
        }

        if(attack != "None")
        {
            move = 0;
            rig.velocity = new Vector2(move * speed, 0.0f);
            anim.SetFloat("Walk",move);
            anim.SetTrigger(attack);
            isAttack = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        move = 0;
        rig.velocity = new Vector2(0.0f, 0.0f);
        anim.SetFloat("Walk", move);
        if(collision.gameObject.layer == 3)
        {
            currentHealth -=10;
            if(currentHealth > 0)
            {
            healthBar.SetHealth(currentHealth);
            
            anim.SetBool("Knockback", true);
            
            if(isPlayer1)
            {
                rig.velocity = new Vector2(speed * (-1), 0.0f);
            }
            
            }
        }
    }
}