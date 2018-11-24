using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Jimmy in the hell
    public GameObject gameOverPanel;
    //public List<Image> vidas;
    public Text vidasText;
    public Button button;
    //BetaTester
    public Slider lifeBetaTester;

    private void Update()
    {
        //foreach (var vida in vidas)
        //    vida.enabled = false;

        #region JimmyInTheHell
        //vidas.ForEach(x => x.enabled = false);

        if (PlayerScript3D.life == 0)
        {
            gameOverPanel.SetActive(true);
            button.Select();
        }
        if (SceneManager.GetActiveScene().buildIndex != 5)
            vidasText.text = "x" + PlayerScript3D.life.ToString();
        else
            vidasText.text = "x" + AIPlayerScript.life.ToString();

        //else
        //    for (int i = 0; i < PlayerScript3D.life; i++)
        //        vidas[i].enabled = true;
        #endregion

        #region BetaTester
        if (SceneManager.GetActiveScene().buildIndex != 5)
        {
            lifeBetaTester.value -= 0.001f;

            if (lifeBetaTester.value == 0)
            {
                //print("Game Over Beta Tester");
            }
        }
        #endregion
    }

    public void Retry()
    {
        PlayerScript3D.life = 5;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
