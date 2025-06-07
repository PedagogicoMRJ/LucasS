using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int totalscore;
    public Text scoreboard;
    public Text score;
    public static GameManager access;

    public GameObject gameover;
    public GameObject winner;

    public AudioSource fundo;
    
    public void ScoreBoard()
    {
        scoreboard.text = totalscore.ToString();
    }

    public void GameOver()
    {
        gameover.SetActive(true);
    }

    public void Winner()
    {
        score.text = totalscore.ToString();
        winner.SetActive(true);
    }

    public void Restar()
    {
        SceneManager.LoadScene("SampleScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        access = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
