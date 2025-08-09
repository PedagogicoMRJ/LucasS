using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject hudRoot; // arraste aqui o GameObject do seu HUD
    
    [SerializeField] private KeyCode toggleKey = KeyCode.F;
    
    private void Start()
    {
        if (hudRoot != null)
            hudRoot.SetActive(false); // começa oculto
    }

    private void Update()
    {
        if (hudRoot == null) return;

        if (Input.GetKeyDown(toggleKey))
            hudRoot.SetActive(!hudRoot.activeSelf);
    }
}
