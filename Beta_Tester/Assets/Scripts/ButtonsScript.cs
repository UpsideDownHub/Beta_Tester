using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour {

    public Button newGameButton;
    public Button continueButton;
    public Button optionsButton;
    public Button creditsButton;

    private void Update()
    {
        
    }

    public void NewGameInteraction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void ContinueInteraction()
    {
        if (continueButton.enabled == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void OptionsInteraction()
    {

    }
    public void CreditsInteraction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
