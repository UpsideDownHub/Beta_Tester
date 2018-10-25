using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverPanel;
    public Image[] vidas;
    //public Image vida1;
    //public Image vida2;
    //public Image vida3;
    //public Image vida4;
    //public Image vida5;
    public Button button;

    private void Update()
    {
        foreach (var vida in vidas)
            vida.enabled = false;

        if (PlayerScript3D.life == 0)
        {
            gameOverPanel.SetActive(true);
            button.Select();
        }
        else
            for (int i = 0; i < PlayerScript3D.life; i++)
                vidas[i].enabled = true;

        //if (PlayerScript3D.life == 5)
        //{
        //    vida1.enabled = true;
        //    vida2.enabled = true;
        //    vida3.enabled = true;
        //    vida4.enabled = true;
        //    vida5.enabled = true;
        //}
        //if (PlayerScript3D.life == 4)
        //{
        //    vida1.enabled = true;
        //    vida2.enabled = true;
        //    vida3.enabled = true;
        //    vida4.enabled = true;
        //    vida5.enabled = false;
        //}
        //if (PlayerScript3D.life == 3)
        //{
        //    vida1.enabled = true;
        //    vida2.enabled = true;
        //    vida3.enabled = true;
        //    vida4.enabled = false;
        //    vida5.enabled = false;
        //}
        //if (PlayerScript3D.life == 2)
        //{
        //    vida1.enabled = true;
        //    vida2.enabled = true;
        //    vida3.enabled = false;
        //    vida4.enabled = false;
        //    vida5.enabled = false;
        //}
        //if (PlayerScript3D.life == 1)
        //{
        //    vida1.enabled = true;
        //    vida2.enabled = false;
        //    vida3.enabled = false;
        //    vida4.enabled = false;
        //    vida5.enabled = false;
        //}
        //if (PlayerScript3D.life <= 0)
        //{
        //    vida1.enabled = false;
        //    vida2.enabled = false;
        //    vida3.enabled = false;
        //    vida4.enabled = false;
        //    vida5.enabled = false;
        //    gameOverPanel.SetActive(true);
        //    button.Select();
        //}
    }

    public void Retry()
    {
        gameOverPanel.SetActive(false);
        PlayerScript3D.life = 5;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
