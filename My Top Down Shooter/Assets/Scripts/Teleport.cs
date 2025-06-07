using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleport : MonoBehaviour
{
    public GameManager gameManager;
    bool winner;
    CircleCollider2D circleCollider;
    bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        winner = false;
        isActive= false;
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
    }

    public void IsActive(bool active)
    {
        isActive = active;
        if (isActive)
        {
            circleCollider.enabled = true; 
            winner = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((isActive)&& (collision.tag == "Player"))
        {
            gameManager.EndScreen(winner);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
