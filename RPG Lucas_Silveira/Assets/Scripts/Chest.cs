using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private Animator chestAnim;
    public bool isInteactable => true;
    public string InteractionName = "Open Chest";
    // Start is called before the first frame update
    void Start()
    {
        chestAnim = GetComponent<Animator>();
    }
    public void Interact()
    {
        Open();
    }
    public void Open()
    {
        chestAnim.SetBool("Open", true);
        Debug.Log("Teste");
    }
}
