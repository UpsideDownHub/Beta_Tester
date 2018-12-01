using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageScript : MonoBehaviour {

    public Text pressAnyKey;
    public Text chooseCharacter;
    public Text resume;
    public Text quit;
    public Text gameOver;
    public Text retry;
    public Text loadLevel;
    //public Text load;
    public Text password;
    public Text typeYourEmail;
    public Text typeYourPassword;
    public Text login;
    public Text errorText;
    public Text thanksText;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Language") == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                chooseCharacter.text = "CHOOSE YOUR CHARACTER";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                pressAnyKey.text = "PRESS ANY KEY TO START";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                resume.text = "RESUME";
                quit.text = "QUIT";
                gameOver.text = "GAME OVER";
                retry.text = "RETRY";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                resume.text = "RESUME";
                quit.text = "QUIT";
                gameOver.text = "GAME OVER";
                retry.text = "RETRY";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                resume.text = "RESUME";
                quit.text = "QUIT";
                gameOver.text = "GAME OVER";
                retry.text = "RETRY";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                resume.text = "RESUME";
                quit.text = "QUIT";
                gameOver.text = "GAME OVER";
                retry.text = "RETRY";
                //loadLevel.text = "LOAD LEVEL";
                //load.text = "LOAD";
                password.text = "Password:";
                typeYourEmail.text = "Type your email";
                typeYourPassword.text = "Type your password";
                login.text = "Login";
                errorText.text = "Email or Password are incorrect";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                thanksText.text = "Thanks for playing!";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                resume.text = "RESUME";
                quit.text = "QUIT";
                gameOver.text = "GAME OVER";
                retry.text = "RETRY";
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                chooseCharacter.text = "ESCOLHA SEU PERSONAGEM";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                pressAnyKey.text = "PRESSIONE QUALQUER TECLA PARA COMEÇAR";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                resume.text = "RESUMIR";
                quit.text = "SAIR";
                gameOver.text = "FIM DE JOGO";
                retry.text = "TENTAR NOVAMENTE";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                resume.text = "RESUMIR";
                quit.text = "SAIR";
                gameOver.text = "FIM DE JOGO";
                retry.text = "TENTAR NOVAMENTE";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                resume.text = "RESUMIR";
                quit.text = "SAIR";
                gameOver.text = "FIM DE JOGO";
                retry.text = "TENTAR NOVAMENTE";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                resume.text = "RESUMIR";
                quit.text = "SAIR";
                gameOver.text = "FIM DE JOGO";
                retry.text = "TENTAR NOVAMENTE";
                //loadLevel.text = "CARREGAR NÍVEL";
                //load.text = "CARREGAR";
                password.text = "Senha:";
                typeYourEmail.text = "Digite seu email";
                typeYourPassword.text = "Digite sua senha";
                login.text = "Entrar";
                errorText.text = "Email ou senha estão incorretos";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                thanksText.text = "Obrigado por jogar!";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                resume.text = "RESUMIR";
                quit.text = "SAIR";
                gameOver.text = "FIM DE JOGO";
                retry.text = "TENTAR NOVAMENTE";
            }
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
