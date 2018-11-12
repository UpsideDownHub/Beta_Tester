﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public GameObject loadLevelText;
    public GameObject loadButton;
    public GameObject principalPanel;
    public GameObject emailText;
    public GameObject passwordText;
    public InputField emailInputField;
    public InputField passwordInputField;
    public GameObject loginButton;

    //método chamado depois de clicar para fora do campo ou dar enter (event: On End Edit)/e no event: On Value Changed, o método é chamado sempre que digitar alguma coisa
    public void GetInput() //Se utilizar GetInput(string typed) o parametro typed vai ser igual ao texto que você digitar no inputfield
    {
        print("teste");
    }

    //método chamado ao clicar no botão
    public void LoginInteraction()
    {
        print(emailInputField.text);
        print(passwordInputField.text);

        loadLevelText.SetActive(true);
        loadButton.SetActive(true);
        principalPanel.SetActive(true);
        emailText.SetActive(false);
        passwordText.SetActive(false);
        emailInputField.gameObject.SetActive(false);
        passwordInputField.gameObject.SetActive(false);
        loginButton.SetActive(false);
    }
}
