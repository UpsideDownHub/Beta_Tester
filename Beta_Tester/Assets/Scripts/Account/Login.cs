using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public PhaseCreationManager phaseCreationManager;

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
}
