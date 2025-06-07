using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class bulletHandler : MonoBehaviour
{
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
        if(collision.tag != "Player2")
        {
            bulletRig.velocity = Vector2.zero;
            bulletAnim.SetTrigger("Impact");
            Destroy(gameObject, 0.12f);
            SceneManager.LoadScene("Menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
