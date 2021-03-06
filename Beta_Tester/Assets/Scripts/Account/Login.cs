﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Login : MonoBehaviour
{
    public GameObject topPanel;
    public GameObject principalPanel;
    public GameObject emailText;
    public GameObject passwordText;
    public InputField emailInputField;
    public InputField passwordInputField;
    public GameObject errorText;
    public GameObject loginButton;
    public PhaseCreationManager phaseCreationManager;

    private void Start()
    {
        if (Assets.Scripts.DAL.BetaTesterContext.UserId != 0)
        {
            phaseCreationManager.StartBuild();

            topPanel.SetActive(true);
            principalPanel.SetActive(true);
            emailText.SetActive(false);
            passwordText.SetActive(false);
            emailInputField.gameObject.SetActive(false);
            passwordInputField.gameObject.SetActive(false);
            errorText.SetActive(false);
            loginButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "EmailInputField")
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                    passwordInputField.Select();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "PasswordInputField")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    LoginInteraction();
                }
            }
        }
            
    }

    //método chamado depois de clicar para fora do campo ou dar enter (event: On End Edit)/e no event: On Value Changed, o método é chamado sempre que digitar alguma coisa
    public void GetInput() //Se utilizar GetInput(string typed) o parametro typed vai ser igual ao texto que você digitar no inputfield
    {
        print("teste");
    }

    //método chamado ao clicar no botão
    public void LoginInteraction()
    {
        Assets.Scripts.DAL.BetaTesterContext.User.GetData();
        if (Assets.Scripts.DAL.BetaTesterContext.User.Data.Any(x => x.Email == emailInputField.text && x.Password == passwordInputField.text))
        {
            Assets.Scripts.DAL.BetaTesterContext.UserId = Assets.Scripts.DAL.BetaTesterContext.User.Data.Single(x => x.Email == emailInputField.text && x.Password == passwordInputField.text).UserId;
            phaseCreationManager.StartBuild();

            topPanel.SetActive(true);
            principalPanel.SetActive(true);
            emailText.SetActive(false);
            passwordText.SetActive(false);
            emailInputField.gameObject.SetActive(false);
            passwordInputField.gameObject.SetActive(false);
            errorText.SetActive(false);
            loginButton.SetActive(false);
        }
        else
        {
            errorText.SetActive(true);
        }
    }
}
