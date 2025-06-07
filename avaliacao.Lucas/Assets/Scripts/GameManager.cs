using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalscore;
    public Text scoreboard;
    public static GameManager access;
    public GameObject gameover;
    public GameObject winner;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        access = this;
    }

    public void ScoreBoard()
    {
        scoreboard.text = totalscore.ToString();
        if(totalscore == 160){
            Winner();
        }
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

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    
}
