using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageScript : MonoBehaviour {

    public Text pressAnyKey;
    public Text chooseCharacter;
    public Text gameOver;
    public Text retry;
    //public Text menu;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Language") == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                pressAnyKey.text = "PRESS ANY KEY";
                chooseCharacter.text = "CHOOSE YOUR CHARACTER";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                gameOver.text = "GAME OVER";
                retry.text = "RETRY";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                gameOver.text = "GAME OVER";
                retry.text = "RETRY";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                gameOver.text = "GAME OVER";
                retry.text = "RETRY";
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                pressAnyKey.text = "APERTE QUALQUER TECLA";
                chooseCharacter.text = "ESCOLHA SEU PERSONAGEM";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                gameOver.text = "FIM DE JOGO";
                retry.text = "TENTAR NOVAMENTE";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                gameOver.text = "FIM DE JOGO";
                retry.text = "TENTAR NOVAMENTE";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                gameOver.text = "FIM DE JOGO";
                retry.text = "TENTAR NOVAMENTE";
            }
        }
    }
}
