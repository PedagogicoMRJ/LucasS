using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyHandler : MonoBehaviour
{
    Bullet bulletParameters;
    private Vector2 randomNum;
    float walkTime;
    public bool hasRandomWalk;
    private Vector2 randomPos;

    public bool hasWalkAnim;
    Animator anim;

    public System.Action killed;

    Transform playerPos;
    public float enemySpeed;
    Rigidbody2D enemyRig;
    Vector2 targetDir;
    // Start is called before the first frame update
    void Start()
    {
        bulletParameters = GetComponentInChildren<Bullet>();
        randomNum = new Vector2((int)Mathf.Round(Random.Range(-1.0f,1.0f)), (int)Mathf.Round(Random.Range(-1.0f, 1.0f)));
        walkTime = 0.0f;
        anim = GetComponentInChildren<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRig = this.GetComponent<Rigidbody2D>();
        randomPos = new Vector2(this.transform.position.x + randomNum.x, this.transform.position.y + randomNum.y);
    }

    // Update is called once per frame
    void Update()
    {
        walkTime += Time.deltaTime;
        targetDir = playerPos.position - this.transform.position;
        targetDir.y += 0.5f;
        targetDir.Normalize();
        if(hasWalkAnim)
        {
            if(hasRandomWalk && (walkTime >= 2))
            {
                Fire();
                walkTime = 0.0f;
                randomNum = new Vector2((int)Mathf.Round(Random.Range(-1.0f, 1.0f)), (int)Mathf.Round(Random.Range(-1.0f, 1.0f)));
                randomPos = new Vector2(this.transform.position.x + randomNum.x, this.transform.position.y + randomNum.y);
            }
            Walk();
        }
        else
        {
            Follow();
        }
        
    }
    void Follow()
    {
        if(enemyRig.velocity.magnitude <= 5.0f) 
        {
            enemyRig.AddForce(targetDir * enemySpeed);
        }
    }

    void Walk()
    {
        if(hasRandomWalk)
        {   
            targetDir = new Vector2(randomPos.x - this.transform.position.x, randomPos.y - this.transform.position.y); 
            targetDir.Normalize();
            enemyRig.velocity = Vector2.zero;
            enemyRig.velocity = new Vector2(targetDir.x, targetDir.y) * enemySpeed;
        }
        
        anim.SetFloat("Horizontal", targetDir.x);
        anim.SetFloat("Vertical", targetDir.y);
        anim.SetFloat("Magnitude", targetDir.magnitude);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene("Menu");
            
        }
        if(collision.tag == "Bullet")
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
    
    void Fire()
    {
        bulletParameters.fireBullet(targetDir, playerPos.position);
    }
}
