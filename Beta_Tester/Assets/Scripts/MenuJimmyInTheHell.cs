using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class MenuJimmyInTheHell : MonoBehaviour {

    #region Components
    public Text newGameText;
    public Text continueText;
    public Text optionsText;
    public Text creditsText;
    public Text quitText;
    public Text resolutionText;
    public Text musicText;
    public Text graphicsText;
    public Text languageText;
    public Text fullscreenText;
    public Button newGameButton;
    public Button continueButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button quitButton;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public GameObject optionsPanel;
    public GameObject loadingText;

    public AudioMixer audioMixer;

    public Dropdown resolutionDropDown;
    public Dropdown graphicsDropDown;
    public Dropdown languageDropDown;

    Resolution[] resolutions;
    Resolution resolution;
    #endregion
    List<string> englishOptions;
    List<string> portugueseOptions;
    string englishOption1 = "English";
    string englishOption2 = "Portuguese";
    string portugueseOption1 = "Inglês";
    string portugueseOption2 = "Português";

    private void Start()
    {
        #region Configuração de Resoluções/Qualidades/Tela Cheia/Áudios/Linguagens ao iniciar o jogo
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();
        graphicsDropDown.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        List<string> qualityOptions = new List<string>();
        englishOptions = new List<string>();
        portugueseOptions = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);
        }

        for (int j = 0; j < QualitySettings.names.Length; j++)
        {
            string option = QualitySettings.names[j];
            qualityOptions.Add(option);
        }

        englishOptions.Add(englishOption1);
        englishOptions.Add(englishOption2);
        portugueseOptions.Add(portugueseOption1);
        portugueseOptions.Add(portugueseOption2);

        fullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen"));
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen"));
        resolutionDropDown.AddOptions(resolutionOptions);
        resolutionDropDown.value = PlayerPrefs.GetInt("resolution");
        resolutionDropDown.RefreshShownValue();
        resolution = resolutions[PlayerPrefs.GetInt("resolution")];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        graphicsDropDown.AddOptions(qualityOptions);
        graphicsDropDown.value = PlayerPrefs.GetInt("GraphicsQuality");
        graphicsDropDown.RefreshShownValue();
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsQuality"));

        volumeSlider.value = PlayerPrefs.GetFloat("MVolume");
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("MVolume"));

        if (PlayerPrefs.GetInt("Language") == 0)
        {
            newGameText.text = "New Game";
            continueText.text = "Continue";
            optionsText.text = "Options";
            creditsText.text = "Credits";
            quitText.text = "Quit Game";
            resolutionText.text = "Resolution";
            fullscreenText.text = "Fullscreen";
            musicText.text = "Music";
            graphicsText.text = "Graphics";
            languageText.text = "Language";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(englishOptions);
            languageDropDown.value = 0;
            languageDropDown.RefreshShownValue();
        }
        else
        {
            newGameText.text = "Novo Jogo";
            continueText.text = "Continuar";
            optionsText.text = "Opções";
            creditsText.text = "Créditos";
            quitText.text = "Sair do Jogo";
            resolutionText.text = "Resolução";
            fullscreenText.text = "Tela Cheia";
            musicText.text = "Música";
            graphicsText.text = "Gráficos";
            languageText.text = "Idioma";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(portugueseOptions);
            languageDropDown.value = 1;
            languageDropDown.RefreshShownValue();
        }
        #endregion
    }

    #region Interação com Buttons
    public void NewGameInteraction (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void ContinueInteraction (int sceneIndex) //Implementar banco de dados
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void OptionsInteraction ()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

    public void CreditsInteraction ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitInteraction()
    {
        Application.Quit();
    }
    #endregion

    #region Interação com Sliders/DropDowns/Toggles
    public void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        resolution = resolutions[PlayerPrefs.GetInt("resolution")];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        PlayerPrefs.SetFloat("MVolume", volume);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("MVolume"));
    }

    public void SetQuality (int qualityIndex)
    {
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsQuality"));
    }

    public void SetFullscreen (bool isFullscreen)
    {
        PlayerPrefs.SetInt("FullScreen", Convert.ToInt32(isFullscreen));
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen"));
    }

    public void SetLanguage (int languageIndex)
    {
        if (languageIndex == 0) //Inglês
        {
            newGameText.text = "New Game";
            continueText.text = "Continue";
            optionsText.text = "Options";
            creditsText.text = "Credits";
            quitText.text = "Quit Game";
            resolutionText.text = "Resolution";
            fullscreenText.text = "Fullscreen";
            musicText.text = "Music";
            graphicsText.text = "Graphics";
            languageText.text = "Language";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(englishOptions);
            languageDropDown.RefreshShownValue();

            PlayerPrefs.SetInt("Language", 0);
        }
        else                    //Português
        {
            newGameText.text = "Novo Jogo";
            continueText.text = "Continuar";
            optionsText.text = "Opções";
            creditsText.text = "Créditos";
            quitText.text = "Sair do Jogo";
            resolutionText.text = "Resolução";
            fullscreenText.text = "Tela Cheia";
            musicText.text = "Música";
            graphicsText.text = "Gráficos";
            languageText.text = "Idioma";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(portugueseOptions);
            languageDropDown.RefreshShownValue();

            PlayerPrefs.SetInt("Language", 1);
        }
    }
    #endregion

    IEnumerator LoadAsynchronously(int sceneIndex) //Tela de loading
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        newGameButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        optionsPanel.SetActive(false);
        loadingText.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
