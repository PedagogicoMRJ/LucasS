using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public bool isBulletEnemy;
    Animator bulletAnim;
    Rigidbody2D bulletRig;
    // Start is called before the first frame update
    void Start()
    {
        bulletRig = GetComponent<Rigidbody2D>();
        bulletAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isBulletEnemy)
        {
            if(collision.tag != "Enemy")
            {
                bulletRig.velocity = Vector2.zero;
                Destroy(gameObject, 0.12f);
            }
        }
        else
        {
            if(collision.tag != "Player")
            {
                bulletRig.velocity = Vector2.zero;
                bulletAnim.SetTrigger("Impact");
                Destroy(gameObject, 0.12f);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
