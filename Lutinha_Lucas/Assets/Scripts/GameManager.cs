using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    // Variável do tipo GameObject que armazena o objeto hud
    public GameObject hud;
    // Variável do tipo GameObject que armazena o objeto menu
    public GameObject menu;
    void Start()
    {
        // Pausa o jogo
        Time.timeScale = 0;
    }
    // Função que inicia a luta do jogo
    public void StartGame()
    {
        // Desativa o objeto associdado a menu
        menu.SetActive(false);
        // Ativa o jogo
        Time.timeScale = 1;
        // Ativa o objeto associdado a hud
        hud.SetActive(true);
    }
    // Função que finaliza a aplicação
    public void QuitGame()
    {
        // Fecha o jogo
        Application.Quit();
    }
}
