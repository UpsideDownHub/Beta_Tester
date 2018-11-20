using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class MenuBetaTester : MonoBehaviour {

    #region Components
    public Text chooseCharacter;
    public GameObject canvas1;
    public GameObject canvas2;
    public Button firstCharacterButton;

    public Text newGameText;
    public Text continueText;
    public Text optionsText;
    public Text creditsText;
    public Text quitText;
    public Text levelEditor;
    public Text resolutionText;
    public Text musicText;
    public Text soundText;
    public Text masterText;
    public Text graphicsText;
    public Text languageText;
    public Text fullscreenText;
    public Text optionsOnlyText;
    public Text gameOptionsText;
    public Text gameOptionsOnlyText;
    public Text audioText;
    public Text audioOnlyText;
    public Text videoText;
    public Text videoOnlyText;
    public Text backText;
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider masterSlider;
    public Toggle fullscreenToggle;
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public GameObject gameOptionsPanel;
    public GameObject audioPanel;
    public GameObject videoPanel;
    public GameObject backButton;

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

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        audioMixer.SetFloat("musicV", PlayerPrefs.GetFloat("musicVolume"));
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        audioMixer.SetFloat("soundV", PlayerPrefs.GetFloat("soundVolume"));
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        audioMixer.SetFloat("masterV", PlayerPrefs.GetFloat("masterVolume"));

        if (PlayerPrefs.GetInt("Language") == 0)
        {
            chooseCharacter.text = "CHOOSE YOUR CHARACTER";
            newGameText.text = "NEW GAME";
            continueText.text = "CONTINUE";
            optionsText.text = "OPTIONS";
            creditsText.text = "CREDITS";
            quitText.text = "QUIT GAME";
            levelEditor.text = "LEVEL EDITOR";
            resolutionText.text = "RESOLUTION:";
            fullscreenText.text = "FULL SCREEN:";
            musicText.text = "MUSIC VOLUME:";
            soundText.text = "SOUND VOLUME:";
            masterText.text = "MASTER VOLUME:";
            graphicsText.text = "GRAPHICS:";
            languageText.text = "LANGUAGE:";
            optionsOnlyText.text = "OPTIONS";
            gameOptionsText.text = "GAME OPTIONS";
            gameOptionsOnlyText.text = "GAME OPTIONS";
            audioText.text = "AUDIO";
            audioOnlyText.text = "AUDIO";
            videoText.text = "VIDEO";
            videoOnlyText.text = "VIDEO";
            backText.text = "BACK";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(englishOptions);
            languageDropDown.value = 0;
            languageDropDown.RefreshShownValue();
        }
        else
        {
            chooseCharacter.text = "ESCOLHA SEU PERSONAGEM";
            newGameText.text = "NOVO JOGO";
            continueText.text = "CONTINUAR";
            optionsText.text = "OPÇÕES";
            creditsText.text = "CRÉDITOS";
            quitText.text = "SAIR DO JOGO";
            levelEditor.text = "EDITOR DE NÍVEL";
            resolutionText.text = "RESOLUÇÃO:";
            fullscreenText.text = "TELA CHEIA:";
            musicText.text = "VOLUME DA MÚSICA:";
            soundText.text = "VOLUME DOS SONS:";
            masterText.text = "VOLUME GERAL:";
            graphicsText.text = "GRÁFICOS:";
            languageText.text = "IDIOMA:";
            optionsOnlyText.text = "OPÇÕES";
            gameOptionsText.text = "OPÇÕES DE JOGO";
            gameOptionsOnlyText.text = "OPÇÕES DE JOGO";
            audioText.text = "ÁUDIO";
            audioOnlyText.text = "ÁUDIO";
            videoText.text = "VÍDEO";
            videoOnlyText.text = "VÍDEO";
            backText.text = "VOLTAR";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(portugueseOptions);
            languageDropDown.value = 1;
            languageDropDown.RefreshShownValue();
        }
        #endregion
    }

    #region Interação com Buttons
    public void NewGameInteraction ()
    {
        canvas1.SetActive(false);
        canvas1.SetActive(true);
        firstCharacterButton.Select();
    }

    public void ContinueInteraction (int sceneIndex) //Implementar banco de dados
    {
        //StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void OptionsInteraction ()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        backButton.SetActive(true);
    }

    public void CreditsInteraction ()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitInteraction()
    {
        Application.Quit();
    }

    public void LevelEditorInteraction()
    {
        SceneManager.LoadScene(7);
    }

    public void GameOptionsButton()
    {
        optionsPanel.SetActive(false);
        gameOptionsPanel.SetActive(true);
    }

    public void AudioButton()
    {
        optionsPanel.SetActive(false);
        audioPanel.SetActive(true);
    }

    public void VideoButton()
    {
        optionsPanel.SetActive(false);
        videoPanel.SetActive(true);
    }

    public void BackButton()
    {
        if (optionsPanel.activeSelf)
        {
            menuPanel.SetActive(true);
            optionsPanel.SetActive(false);
            backButton.SetActive(false);
        }
        else if (gameOptionsPanel.activeSelf)
        {
            optionsPanel.SetActive(true);
            gameOptionsPanel.SetActive(false);
        }
        else if (audioPanel.activeSelf)
        {
            optionsPanel.SetActive(true);
            audioPanel.SetActive(false);
        }
        else if (videoPanel.activeSelf)
        {
            optionsPanel.SetActive(true);
            videoPanel.SetActive(false);
        }
    }
    #endregion

    #region Interação com Sliders/DropDowns/Toggles
    public void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        resolution = resolutions[PlayerPrefs.GetInt("resolution")];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicVolume (float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
        audioMixer.SetFloat("musicV", PlayerPrefs.GetFloat("musicVolume"));
    }

    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("masterVolume", volume);
        audioMixer.SetFloat("masterV", PlayerPrefs.GetFloat("masterVolume"));
    }

    public void SetSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat("soundVolume", volume);
        audioMixer.SetFloat("soundV", PlayerPrefs.GetFloat("soundVolume"));
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
            newGameText.text = "NEW GAME";
            continueText.text = "CONTINUE";
            optionsText.text = "OPTIONS";
            creditsText.text = "CREDITS";
            quitText.text = "QUIT GAME";
            levelEditor.text = "LEVEL EDITOR";
            resolutionText.text = "RESOLUTION:";
            fullscreenText.text = "FULL SCREEN:";
            musicText.text = "MUSIC VOLUME:";
            soundText.text = "SOUND VOLUME:";
            masterText.text = "MASTER VOLUME:";
            graphicsText.text = "GRAPHICS:";
            languageText.text = "LANGUAGE:";
            optionsOnlyText.text = "OPTIONS";
            gameOptionsText.text = "GAME OPTIONS";
            gameOptionsOnlyText.text = "GAME OPTIONS";
            audioText.text = "AUDIO";
            audioOnlyText.text = "AUDIO";
            videoText.text = "VIDEO";
            videoOnlyText.text = "VIDEO";
            backText.text = "BACK";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(englishOptions);
            languageDropDown.RefreshShownValue();

            PlayerPrefs.SetInt("Language", 0);
        }
        else                    //Português
        {
            newGameText.text = "NOVO JOGO";
            continueText.text = "CONTINUAR";
            optionsText.text = "OPÇÕES";
            creditsText.text = "CRÉDITOS";
            quitText.text = "SAIR DO JOGO";
            levelEditor.text = "EDITOR DE NÍVEL";
            resolutionText.text = "RESOLUÇÃO:";
            fullscreenText.text = "TELA CHEIA:";
            musicText.text = "VOLUME DA MÚSICA:";
            soundText.text = "VOLUME DOS SONS:";
            masterText.text = "VOLUME GERAL:";
            graphicsText.text = "GRÁFICOS:";
            languageText.text = "IDIOMA:";
            optionsOnlyText.text = "OPÇÕES";
            gameOptionsText.text = "OPÇÕES DE JOGO";
            gameOptionsOnlyText.text = "OPÇÕES DE JOGO";
            audioText.text = "ÁUDIO";
            audioOnlyText.text = "ÁUDIO";
            videoText.text = "VÍDEO";
            videoOnlyText.text = "VÍDEO";
            backText.text = "VOLTAR";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(portugueseOptions);
            languageDropDown.RefreshShownValue();

            PlayerPrefs.SetInt("Language", 1);
        }
    }
    #endregion

    public void Character(int characterIndex)
    {
        if (characterIndex == 1)
        {
            //configurações do personagem 1
        }
        else if (characterIndex == 2)
        {
            //configurações do personagem 2
        }
        else if (characterIndex == 3)
        {
            //configurações do personagem 3
        }

        SceneManager.LoadScene(1);
    }
}
