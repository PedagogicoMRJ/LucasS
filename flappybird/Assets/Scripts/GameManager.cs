using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text scoretext;
    public AudioSource music;
    
    public void Restart(){
        music.Play();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}