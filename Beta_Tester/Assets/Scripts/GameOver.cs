using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    //Jimmy in the hell
    public GameObject gameOverPanel;
<<<<<<< HEAD
    public Image[] vidas;
    //public Image vida1;
    //public Image vida2;
    //public Image vida3;
    //public Image vida4;
    //public Image vida5;
=======
    public List<Image> vidas;
>>>>>>> eafcff900a0cbd00da068146beae040420fd8b41
    public Button button;
    //BetaTester
    public Slider slider;

    private void Update()
    {
<<<<<<< HEAD
        foreach (var vida in vidas)
            vida.enabled = false;
=======
        #region JimmyInTheHell
        vidas.ForEach(x => x.enabled = false);
>>>>>>> eafcff900a0cbd00da068146beae040420fd8b41

        if (PlayerScript3D.life == 0)
        {
            gameOverPanel.SetActive(true);
            button.Select();
        }
        else
            for (int i = 0; i < PlayerScript3D.life; i++)
                vidas[i].enabled = true;
<<<<<<< HEAD

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
=======
        #endregion

        #region BetaTester
        slider.value -= 0.001f;

        if (slider.value == 0 || slider.value == 1)
        {
            print("Game Over Beta Tester");
        }
        #endregion
>>>>>>> eafcff900a0cbd00da068146beae040420fd8b41
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
