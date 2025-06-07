using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour, IInteractable
{
    public string nameText;
    public string[] speechText;
    private DialogueManager dm;

    void Start()
    {
        dm = FindAnyObjectByType<DialogueManager>();
    }

    public void Interact()
    {
        dm.Speech(nameText, speechText);
    }
}
