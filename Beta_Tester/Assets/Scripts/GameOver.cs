using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    //Jimmy in the hell
    public GameObject gameOverPanel;
    public List<Image> vidas;
    public Button button;
    //BetaTester
    public Slider slider;

    private void Update()
    {
        #region JimmyInTheHell
        vidas.ForEach(x => x.enabled = false);

        if (PlayerScript3D.life == 0)
        {
            gameOverPanel.SetActive(true);
            button.Select();
        }
        else
            for (int i = 0; i < PlayerScript3D.life; i++)
                vidas[i].enabled = true;
        #endregion

        #region BetaTester
        slider.value -= 0.001f;

        if (slider.value == 0 || slider.value == 1)
        {
            print("Game Over Beta Tester");
        }
        #endregion
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
