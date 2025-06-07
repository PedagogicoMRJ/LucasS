using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    // Vari�vel do tipo GameObject que armazena o objeto hud
    public GameObject hud;
    // Vari�vel do tipo GameObject que armazena o objeto menu
    public GameObject menu;
    void Start()
    {
        // Pausa o jogo
        Time.timeScale = 0;
    }
    // Fun��o que inicia a luta do jogo
    public void StartGame()
    {
        // Desativa o objeto associdado a menu
        menu.SetActive(false);
        // Ativa o jogo
        Time.timeScale = 1;
        // Ativa o objeto associdado a hud
        hud.SetActive(true);
    }
    // Fun��o que finaliza a aplica��o
    public void QuitGame()
    {
        // Fecha o jogo
        Application.Quit();
    }
}
