using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isFinalLevel;
    public int level;
    public GameObject canvas;
    public GameObject win;
    public GameObject lose;
    bool winner;
    float time = 0;
    bool endScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        winner = false;
        endScreen = false;
        canvas.SetActive(false);
        win.SetActive(false);
        lose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(endScreen)
        {
<<<<<<< Updated upstream
            time = 0.016f;
            //time += Time.deltaTime;
=======
            time += 0.016f;
>>>>>>> Stashed changes
            if(time > 3.0f)
            {
                if(isFinalLevel)
                {
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    SceneManager.LoadScene("Level" + level);
                }
            }
        }
    }

    public void EndScreen(bool youWin)
    {
        Time.timeScale = 0;
        winner = youWin;
        //time = 0;
        //endScreen = true;
        canvas.SetActive(true);
        if(winner)
        {
            win.SetActive(true);
            lose.SetActive(false);
            level++;
        }
        else
        {
            win.SetActive(false);
            lose.SetActive(true);
        }
    }
}
