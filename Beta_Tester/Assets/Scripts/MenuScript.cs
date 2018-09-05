using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class MenuScript : MonoBehaviour {
    
    #region Components
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

    Resolution[] resolutions;
    Resolution resolution;
    #endregion

    private void Start()
    {
        #region Configuração de Resoluções/Qualidades/Tela Cheia/Áudios ao iniciar o jogo
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();
        graphicsDropDown.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        List<string> qualityOptions = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        for (int j = 0; j < QualitySettings.names.Length; j++)
        {
            string option = QualitySettings.names[j];
            qualityOptions.Add(option);
        }

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
    #endregion

    IEnumerator LoadAsynchronously(int sceneIndex)
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
