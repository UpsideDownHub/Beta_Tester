using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using UnityEngine.EventSystems;

public class MenuBetaTester : MonoBehaviour {

    #region Components
    public Text chooseCharacter;
    public GameObject canvas1;
    public GameObject canvas2;
    public Button firstCharacterButton;
    public GameObject betaTester;
    public GameObject betaTesterEngr;

    public Text newGameText;
    public Text optionsText;
    public Text controlsText;
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
    public Text albertPistolText;
    public Text balancedAlbertText;
    public Text medicatedAlbertText;
    public Text pistolDescriptionText;
    public Text balancedDescriptionText;
    public Text medicatedDescriptionText;

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
    public AudioSource clickedSound;
    public static int personality;

    public static bool betaTesterCutscene;
    public GameObject canvasCutscene;


    Button newGameButton;
    Button gameOptionsButton;
    bool teste;

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

        if (PlayerPrefs.HasKey("resolution"))
        {
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
                chooseCharacter.text = "CHOOSE YOUR PERSONALITY";
                newGameText.text = "NEW GAME";
                optionsText.text = "OPTIONS";
                controlsText.text = "CONTROLS";
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
                albertPistolText.text = "Albert Pistol";
                balancedAlbertText.text = "Balanced Albert";
                medicatedAlbertText.text = "Medicated Albert";
                pistolDescriptionText.text = "Created with milk and pear, ovomaltine and bread with mortadella. He hates challenges and says that he is a Pro Player, but only plays in the easy difficult.";
                balancedDescriptionText.text = "Likes challenge in the right measure.Do not make your life easier, but do not make the game impossible either.";
                medicatedDescriptionText.text = "Likes to be challenged to the fullest. The greater the difficulty, the happier he gets.";

                languageDropDown.ClearOptions();
                languageDropDown.AddOptions(englishOptions);
                languageDropDown.value = 0;
                languageDropDown.RefreshShownValue();
            }
            else
            {
                chooseCharacter.text = "ESCOLHA SUA PERSONALIDADE";
                newGameText.text = "NOVO JOGO";
                optionsText.text = "OPÇÕES";
                controlsText.text = "CONTROLES";
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
                albertPistolText.text = "Albert Pistola";
                balancedAlbertText.text = "Albert Equilibrado";
                medicatedAlbertText.text = "Albert Medicado";
                pistolDescriptionText.text = "Criado a leite com pera, ovomaltine e pão com mortadela. Odeia desafios, diz que é Pro Player, mas só joga no easy.";
                balancedDescriptionText.text = "Gosta de desafios na medida certa. Não facilite sua vida, mas também não torne o jogo impossível.";
                medicatedDescriptionText.text = "Gosta de ser desafiado ao máximo. Quanto maior a dificuldade, mais feliz ele fica.";

                languageDropDown.ClearOptions();
                languageDropDown.AddOptions(portugueseOptions);
                languageDropDown.value = 1;
                languageDropDown.RefreshShownValue();
            }
        }
        else
        {
            fullscreenToggle.isOn = true;
            Screen.fullScreen = true;
            resolutionDropDown.AddOptions(resolutionOptions);
            resolutionDropDown.value = resolutionOptions.Count - 1;
            resolutionDropDown.RefreshShownValue();
            resolution = resolutions[resolutionOptions.Count - 1];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

            graphicsDropDown.AddOptions(qualityOptions);
            graphicsDropDown.value = qualityOptions.Count - 1;
            graphicsDropDown.RefreshShownValue();
            QualitySettings.SetQualityLevel(qualityOptions.Count - 1);

            musicSlider.value = 0;
            soundSlider.value = 0;
            masterSlider.value = 0;

            chooseCharacter.text = "CHOOSE YOUR PERSONALITY";
            newGameText.text = "NEW GAME";
            optionsText.text = "OPTIONS";
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
            albertPistolText.text = "Albert Pistol";
            balancedAlbertText.text = "Balanced Albert";
            medicatedAlbertText.text = "Medicated Albert";
            pistolDescriptionText.text = "Created with milk and pear, ovomaltine and bread with mortadella. He hates challenges and says that he is a Pro Player, but only plays in the easy difficult.";
            balancedDescriptionText.text = "Likes challenge in the right measure.Do not make your life easier, but do not make the game impossible either.";
            medicatedDescriptionText.text = "Likes to be challenged to the fullest. The greater the difficulty, the happier he gets.";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(englishOptions);
            languageDropDown.value = 0;
            languageDropDown.RefreshShownValue();
        }
        #endregion

        newGameButton = newGameText.transform.parent.GetComponent<Button>();
        newGameButton.Select();
        gameOptionsButton = gameOptionsText.transform.parent.GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!EventSystem.current.alreadySelecting)
                teste = true;
        }
        if (teste)
        {
            newGameButton.Select();
            teste = false;
        }
    }

    #region Interação com Buttons
    public void NewGameInteraction ()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);
        firstCharacterButton.Select();
    }

    public void OptionsInteraction ()
    {
        betaTester.SetActive(false);
        betaTesterEngr.SetActive(false);
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        gameOptionsButton.Select();
        backButton.SetActive(true);
    }

    public void QuitInteraction()
    {
        Application.Quit();
    }

    public void LevelEditorInteraction()
    {
        SceneManager.LoadScene(5);
    }

    public void GameOptionsButton()
    {
        optionsPanel.SetActive(false);
        gameOptionsPanel.SetActive(true);
        languageDropDown.Select();
    }

    public void AudioButton()
    {
        optionsPanel.SetActive(false);
        audioPanel.SetActive(true);
        masterSlider.Select();
    }

    public void VideoButton()
    {
        optionsPanel.SetActive(false);
        videoPanel.SetActive(true);
        resolutionDropDown.Select();
    }

    public void BackButton()
    {
        if (optionsPanel.activeSelf)
        {
            betaTester.SetActive(true);
            betaTesterEngr.SetActive(true);
            menuPanel.SetActive(true);
            optionsPanel.SetActive(false);
            newGameButton.Select();
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
            chooseCharacter.text = "CHOOSE YOUR PERSONALITY";
            newGameText.text = "NEW GAME";
            optionsText.text = "OPTIONS";
            controlsText.text = "CONTROLS";
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
            albertPistolText.text = "Albert Pistol";
            balancedAlbertText.text = "Balanced Albert";
            medicatedAlbertText.text = "Medicated Albert";
            pistolDescriptionText.text = "Created with milk and pear, ovomaltine and bread with mortadella. He hates challenges and says that he is a Pro Player, but only plays in the easy difficult.";
            balancedDescriptionText.text = "Likes challenge in the right measure.Do not make your life easier, but do not make the game impossible either.";
            medicatedDescriptionText.text = "Likes to be challenged to the fullest. The greater the difficulty, the happier he gets.";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(englishOptions);
            languageDropDown.RefreshShownValue();

            PlayerPrefs.SetInt("Language", 0);
        }
        else                    //Português
        {
            chooseCharacter.text = "ESCOLHA SUA PERSONALIDADE";
            newGameText.text = "NOVO JOGO";
            optionsText.text = "OPÇÕES";
            controlsText.text = "CONTROLES";
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
            albertPistolText.text = "Albert Pistola";
            balancedAlbertText.text = "Albert Equilibrado";
            medicatedAlbertText.text = "Albert Medicado";
            pistolDescriptionText.text = "Criado a leite com pera, ovomaltine e pão com mortadela. Odeia desafios, diz que é Pro Player, mas só joga no easy.";
            balancedDescriptionText.text = "Gosta de desafios na medida certa. Não facilite sua vida, mas também não torne o jogo impossível.";
            medicatedDescriptionText.text = "Gosta de ser desafiado ao máximo. Quanto maior a dificuldade, mais feliz ele fica.";

            languageDropDown.ClearOptions();
            languageDropDown.AddOptions(portugueseOptions);
            languageDropDown.RefreshShownValue();

            PlayerPrefs.SetInt("Language", 1);
        }
    }
    #endregion

    public void Personality(int personalityIndex)
    {
        if (personalityIndex == 1)
        {
            personality = 1;
        }
        else if (personalityIndex == 2)
        {
            personality = 2;
        }
        else if (personalityIndex == 3)
        {
            personality = 3;            
        }

        canvas2.SetActive(false);
        canvasCutscene.SetActive(true);
        betaTesterCutscene = true;
    }

    public void ClickedSound()
    {
        clickedSound.Play();
    }
}
