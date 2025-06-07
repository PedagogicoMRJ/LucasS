using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public GameManager gamemanager;
    bool playerLive;

    Vector2 inputVector = Vector2.zero;
    Vector2 mouseVector = Vector2.zero;
    Controller playerController;
    private bool aiming;
    // Start is called before the first frame update
    void Start()
    {
        playerLive = true;
        playerController = GetComponent<Controller>();
        aiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            inputVector = Vector2.zero;
            mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aiming = true;
        }
        else{
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aiming = false;
        }
            playerController.SetVectors(inputVector, mouseVector, aiming);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            playerLive = false;
            gamemanager.EndScreen(playerLive);
        }
        if(collision.tag == "bulletE")
        {
            playerLive = false;
            gamemanager.EndScreen(playerLive);
        }
    }
}
