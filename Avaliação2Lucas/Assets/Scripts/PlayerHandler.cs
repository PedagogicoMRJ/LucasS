using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public bool isPlayer1;
    Vector2 inputVector = Vector2.zero;
    Controller playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayer1)
        {
            inputVector.x = Input.GetAxis("Horizontal1");
            inputVector.y = Input.GetAxis("Vertical1");
            playerController.SetInputVector(inputVector);
        }
        else
        {
            inputVector.x = Input.GetAxis("Horizontal2");
            inputVector.y = Input.GetAxis("Vertical2");
            playerController.SetInputVector(inputVector);            
        }
        
    }
}
