using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscenes : MonoBehaviour
{
    public GameObject level;
    public GameObject ceu;
    public GameObject canvasCutscene;
    public GameObject cutScene;
    public Transform playerCutscene;
    public Text cutsceneText;
    //public Text skipText;
    public Image siteJimmy;
    public Animator siteJimmyA;
    public Animator cutSceneFireLevelA;
    public Image albert;
    public Animator albertAnimator;
    float temp;
    float temp2;
    float temp3;
    bool isFirstSceneCompleted; //betatester
    bool isSecondSceneCompleted; //jimmy
    bool beginGame;
    float x;

    public GameObject logoJimmy;
    public GameObject pressAnyKey;
    public GameObject CreditsText;
    public GameObject ceuNuvem;
    public GameObject tarjaPreta;
    public GameObject blackBG;
    public Animator cutsceneImagesJimmyA;
    RectTransform rtCutSceneImages;
    RectTransform rtCreditsText;
    float bottomRT;
    float leftRT;
    float rightRT;
    float posXRTCredits;
    Text creditsTextUI;
    float temp4;
    float temp5;
    bool isCreditsTime;
    bool canMoveCreditsText;
    int i = 0;
    public Text bigText;

    public AudioSource acdcBackInBlack;
    public AudioSource truckIdle;
    public AudioSource ExplosionTruck;
    bool dontRepeat;
    bool dontRepeat2;
    bool dontRepeat3;
    bool isPlaying;
    bool canSkipSchoolCutscene;
    bool canSkipDesertCutscene;
    bool isTheEndOfCredits;

    RectTransform canvasRT;
    RectTransform ceuNuvemRT;
    public static bool isPlayingCutscene;

    public GameObject thanksObj;
    public Animator albertVictorySceneA;
    float temp6;
    bool dontRepeat4;

    public GameObject loadingText;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            temp = 0;
            temp2 = 0;
        }
        
        //if (PlayerPrefs.GetInt("Language") == 0)
        //    skipText.text = "PRESS SPACE TO SKIP";
        //else
        //    skipText.text = "PRESSIONE ESPAÇO PARA PULAR";

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            temp3 = 0;
            x = playerCutscene.localPosition.x;

            var csj = GameObject.Find("CutSceneJump");
            if (csj != null)
                if (csj.GetComponent<cutSceneJump>().JumpCutScene)
                    JumpCutScene();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            temp4 = 0;
            temp5 = 0;

            rtCutSceneImages = GameObject.Find("CutsceneImages").GetComponent<RectTransform>();
            bottomRT = rtCutSceneImages.offsetMin.y;
            leftRT = rtCutSceneImages.offsetMin.x;
            rightRT = rtCutSceneImages.offsetMax.x;

            rtCreditsText = CreditsText.GetComponent<RectTransform>();
            posXRTCredits = rtCreditsText.anchoredPosition.x;
            creditsTextUI = CreditsText.GetComponent<Text>();

            canvasRT = GameObject.Find("Canvas").GetComponent<RectTransform>();
            ceuNuvemRT = ceuNuvem.GetComponent<RectTransform>();
        }

        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            temp6 = 0;
        }
    }

    private void Update()
    {
        #region BetaTester Cutscene
        if (SceneManager.GetActiveScene().buildIndex == 0 && MenuBetaTester.betaTesterCutscene)
        {
            temp += Time.deltaTime;
            if (temp <= 5 && temp >= 0)
            {
                isPlayingCutscene = true;
                Camera.main.backgroundColor = new Color(0, 0, 0);

                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Manager: Are we recording? Oh ok.";
                else
                    cutsceneText.text = "Empresário: Estamos gravando? Ah ok.";
            }
            if (temp <= 10 && temp >= 5)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Good afternoon Albert. You have been selected to play the game Jimmy in the Hell.";
                else
                    cutsceneText.text = "Boa tarde Albert. Você foi selecionado para jogar o game Jimmy in the Hell.";
            }
            if (temp <= 15 && temp >= 10)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "I hope you are comfortable.";
                else
                    cutsceneText.text = "Espero que esteja confortável.";
            }
            if (temp <= 22 && temp >= 15)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "According to the regulation, we will film your gameplay and your reactions for profit. I mean, scientific purposes.";
                else
                    cutsceneText.text = "De acordo com o regulamento, nós filmaremos o seu gameplay e suas reações para fins lucrativos. Ehh quer dizer científicos.";
            }
            if (temp <= 29 && temp >= 22)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Your image and audio are being recorded, but safe with us. We always valorize the privacy of our contributors.";
                else
                    cutsceneText.text = "Sua imagem e áudio estão sendo gravados, mas estão seguros conosco. Sempre prezamos a privacidade de nossos colaboradores.";
            }
            if (temp <= 30 && temp >= 29)
            {
                tarjaPreta.SetActive(false);
                albert.enabled = false;
                cutsceneText.text = "";
                siteJimmy.enabled = true;
                siteJimmyA.enabled = true;
            }

            if (isFirstSceneCompleted)
            {
                temp2 += Time.deltaTime;
                if (temp2 <= 4 && temp2 >= 0)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Are we ready?";
                    else
                        cutsceneText.text = "Estamos prontos?";
                }
                if (temp2 <= 8 && temp2 >= 4)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Do you need something?";
                    else
                        cutsceneText.text = "Você precisa de algo?";
                }
                if (temp2 <= 12 && temp2 >= 8)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Albert: No, I'm fine.";
                    else
                        cutsceneText.text = "Albert: Não, estou legal.";
                }
                if (temp2 <= 16 && temp2 >= 12)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: Ok.";
                    else
                        cutsceneText.text = "Empresário: Ok.";
                }
                if (temp2 <= 20 && temp2 >= 16)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Are your organs healthy?";
                    else
                        cutsceneText.text = "Seus órgãos estão saudáveis?";
                }
                if (temp2 <= 24 && temp2 >= 20)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Albert: I think so.";
                    else
                        cutsceneText.text = "Albert: Acho que sim.";
                }
                if (temp2 <= 28 && temp2 >= 24)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: Are the kidneys good?";
                    else
                        cutsceneText.text = "Empresário: Os rins estão bonzinhos?";
                }
                if (temp2 <= 32 && temp2 >= 28)
                {
                    albertAnimator.Play("Albert2");

                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Albert: Why are you asking me that?";
                    else
                        cutsceneText.text = "Albert: Por que a pergunta?";
                }
                if (temp2 <= 36 && temp2 >= 32)
                {
                    albertAnimator.Play("Albert");

                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: Oh it's nothing, I'am just testing your reflexes.";
                    else
                        cutsceneText.text = "Empresário: Ah nada, é só para testar seus reflexos.";
                }
                if (temp2 <= 40 && temp2 >= 36)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "We can start.";
                    else
                        cutsceneText.text = "Podemos começar.";
                }
                if (temp2 <= 44 && temp2 >= 40)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Albert: Yay!";
                    else
                        cutsceneText.text = "Albert: Demoro!";
                }
                if (temp2 <= 45 && temp2 >= 44)
                {
                    StartCoroutine(LoadAsynchronously(1));
                    temp = 46;
                }
            }
        }
        #endregion

        #region MenuJimmy Cutscene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!pressAnyKey.activeSelf)
                isPlayingCutscene = true;
            else
                isPlayingCutscene = false;

            if (MenuJimmy.isAnyKeyPressed)
            {
                pressAnyKey.SetActive(false);

                if (rtCutSceneImages.offsetMin.y < canvasRT.sizeDelta.y)
                {
                    bottomRT -= (55 * canvasRT.sizeDelta.normalized.y) * Time.deltaTime;
                    leftRT -= (5 / canvasRT.sizeDelta.normalized.y) * Time.deltaTime;
                    rightRT -= (5 / canvasRT.sizeDelta.normalized.y) * Time.deltaTime;

                    rtCutSceneImages.offsetMax = new Vector2(rightRT, rtCutSceneImages.offsetMax.y);
                    rtCutSceneImages.offsetMin = new Vector2(-leftRT, -bottomRT);

                    if (!dontRepeat)
                    {
                        truckIdle.volume = 0;
                        truckIdle.PlayDelayed(9);
                        Invoke("RaisesVolumeOverTime", 9);
                        dontRepeat = true;
                    }

                    if (truckIdle.volume < 1 && isPlaying)
                        truckIdle.volume += 0.05f * Time.deltaTime;
                }
                else
                {
                    if (!isSecondSceneCompleted)
                    {
                        canSkipSchoolCutscene = true;

                        temp4 += Time.deltaTime;
                        if (temp4 <= 4 && temp4 >= 0)
                        {
                            //skipText.gameObject.SetActive(true);
                            tarjaPreta.SetActive(true);

                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Teacher: Good morning children!";
                            else
                                cutsceneText.text = "Professora: Bom dia crianças!";
                        }
                        if (temp4 <= 8 && temp4 >= 4)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Today we are going to have a incredible tour in the museum.";
                            else
                                cutsceneText.text = "Hoje daremos um passeio incrível ao museu.";
                        }
                        if (temp4 <= 12 && temp4 >= 8)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "As we will be in public, I beg you, please, to behave as normal people";
                            else
                                cutsceneText.text = "Como estaremos em público, peço, por gentileza, que vocês se comportem como gente.";
                        }
                        if (temp4 <= 16 && temp4 >= 12)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Don't shit in the bus!";
                            else
                                cutsceneText.text = "Nada de cagar no ônibus!";
                        }
                        if (temp4 <= 20 && temp4 >= 16)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Stop fighting!";
                            else
                                cutsceneText.text = "Parem de brigar!";
                        }
                        if (temp4 <= 24 && temp4 >= 20)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Jimmy, what are you lighting up back there?";
                            else
                                cutsceneText.text = "Jimmy, o que você está acendendo aí no fundo?";
                        }
                        if (temp4 <= 28 && temp4 >= 24)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Give it to me!";
                            else
                                cutsceneText.text = "Passe isso pra cá!";
                        }
                        if (temp4 <= 32 && temp4 >= 28)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Are you ready driver?";
                            else
                                cutsceneText.text = "Tudo pronto motorista?";
                        }
                        if (temp4 <= 36 && temp4 >= 32)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Driver: Yes.";
                            else
                                cutsceneText.text = "Motorista: Tudo tranquilo.";
                        }
                        if (temp4 <= 40 && temp4 >= 36)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Teacher: Then let's go before I regret it!";
                            else
                                cutsceneText.text = "Professora: Então vamos antes que eu me arrependa!";
                        }
                        if (temp4 <= 44 && temp4 >= 40)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Driver: Ma'am, you can't smoke in the bus";
                            else
                                cutsceneText.text = "Motorista: Senhora, você não pode fumar no ônibus.";
                        }
                        if (temp4 <= 48 && temp4 >= 44)
                        {
                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Teacher: Oh sorry.";
                            else
                                cutsceneText.text = "Professora: Ah desculpe.";
                        }
                        if (temp4 <= 52 && temp4 >= 48)
                        {
                            acdcBackInBlack.enabled = true;

                            if (PlayerPrefs.GetInt("Language") == 0)
                                cutsceneText.text = "Driver: Let's go guys!";
                            else
                                cutsceneText.text = "Motorista: Vamos lá galera!";
                        }
                        if (temp4 <= 53 && temp4 >= 52)
                        {
                            tarjaPreta.SetActive(false);
                            cutsceneText.text = "";
                            cutsceneImagesJimmyA.SetBool("secondPart", true);
                            Invoke("BusOutOfTheScreenSchool", 1.6f);
                            temp4 = 54;
                        }
                    }
                }
                if (isSecondSceneCompleted)
                {
                    temp5 += Time.deltaTime;
                    if (temp5 <= 6 && temp5 >= 0)
                    {
                        cutsceneText.text = "";

                        if (PlayerPrefs.GetInt("Language") == 0)
                            bigText.text = "4 hours later";
                        else
                            bigText.text = "4 horas depois";
                    }
                    if (temp5 <= 12 && temp5 >= 6)
                    {
                        if (!dontRepeat3)
                        {
                            blackBG.SetActive(false);
                            ceuNuvem.SetActive(true);
                            truckIdle.Play();
                            dontRepeat3 = true;
                        }

                        bigText.text = "";

                        if (PlayerPrefs.GetInt("Language") == 0)
                            cutsceneText.text = "Teacher: Hey driver, are we on the right track?";
                        else
                            cutsceneText.text = "Professora: Ei motorista, estamos no caminho certo?";
                    }
                    if (temp5 <= 18 && temp5 >= 12)
                    {
                        if (PlayerPrefs.GetInt("Language") == 0)
                            cutsceneText.text = "Driver: I think so ma'am, I'm following the GPS.";
                        else
                            cutsceneText.text = "Motorista: Acho que sim dona, estou seguindo o GPS.";
                    }
                    if (temp5 <= 24 && temp5 >= 18)
                    {
                        if (PlayerPrefs.GetInt("Language") == 0)
                            cutsceneText.text = "Jimmy: ♪ Driver, you can run, the guys aren't afraid to die! ♪";
                        else
                            cutsceneText.text = "Jimmy: ♪ Motorista, pode correr, que a galera não tem medo de morrer! ♪";
                    }
                    if (temp5 <= 30 && temp5 >= 24)
                    {
                        if (PlayerPrefs.GetInt("Language") == 0)
                            cutsceneText.text = "Teacher: Shut up brat!";
                        else
                            cutsceneText.text = "Professora: Cala a boca moleque!";
                    }
                    if (temp5 <= 36 && temp5 >= 30)
                    {
                        if (PlayerPrefs.GetInt("Language") == 0)
                            cutsceneText.text = "Students: ♪ Driver, you can run, the guys aren't afraid to die! ♪";
                        else
                            cutsceneText.text = "Alunos: ♪ Motorista, pode correr, que a galera não tem medo de morrer! ♪";
                    }
                    if (temp5 <= 42 && temp5 >= 36)
                    {
                        if (PlayerPrefs.GetInt("Language") == 0)
                            cutsceneText.text = "Driver: Okay!";
                        else
                            cutsceneText.text = "Motorista: Demoro!";
                    }
                    if (temp5 <= 46 && temp5 >= 42)
                    {
                        if (dontRepeat)
                        {
                            cutsceneText.text = "";
                            tarjaPreta.SetActive(false);
                            cutsceneImagesJimmyA.SetBool("fourthPart", true);
                            truckIdle.Stop();
                            ExplosionTruck.PlayDelayed(0.5f);
                            dontRepeat = false;
                        }
                    }
                    if (temp5 <= 50 && temp5 >= 46)
                    {
                        blackBG.SetActive(true);

                        cutsceneText.text = "";

                        if (PlayerPrefs.GetInt("Language") == 0)
                            bigText.text = "And died.";
                        else
                            bigText.text = "E morreu.";
                    }
                    if (temp5 <= 51 && temp5 >= 50)
                    {
                        var menuJimmy = GameObject.Find("Main Camera").GetComponent<MenuJimmy>();
                        menuJimmy.StartGame();
                        canSkipDesertCutscene = false;
                        temp5 = 52;
                    }
                }
            }
        }
#endregion

        #region levelCleber Cutscene
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            temp3 += Time.deltaTime;
            if (temp3 <= 5 && temp3 >= 0)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: 10 versicle...";
                else
                    cutsceneText.text = "Narrador: Jerimonômio 10 versículo...";
            }
            if (temp3 <= 10 && temp3 >= 5)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Dubbing Assistant: Hey Mr. Moreira, you have to dub the game!";
                else
                    cutsceneText.text = "Assistente de dublagem: Ei senhor Moreira, é pra dublar o jogo!";
            }
            if (temp3 <= 15 && temp3 >= 10)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: Oh, Sorry. I am old my son.";
                else
                    cutsceneText.text = "Narrador: Ah, me enganei. Já estou velho meu filho.";
            }
            if (temp3 <= 20 && temp3 >= 15)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: Wake up Jimmy!";
                else
                    cutsceneText.text = "Narrador: Acorde Jimmy!";
            }
            if (temp3 <= 25 && temp3 >= 20) //5
            {
                cutSceneFireLevelA.SetBool("SecondScene", true);
                cutsceneText.text = "";
                tarjaPreta.SetActive(false);
            }
            if (temp3 <= 30 && temp3 >= 25) //10
            {
                tarjaPreta.SetActive(true);
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: WAKE UP JIMMY!!!";
                else
                    cutsceneText.text = "Narrador: ACORDE JIMMY!!!";
            }
            if (temp3 <= 35 && temp3 >= 30) //15
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: Oh my god, where am I?";
                else
                    cutsceneText.text = "Jimmy: Meu deus onde estou?";
            }
            if (temp3 <= 40 && temp3 >= 35) //20
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: In hell!";
                else
                    cutsceneText.text = "Narrador: No inferno!";
            }
            if (temp3 <= 45 && temp3 >= 40) //25
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: I died?";
                else
                    cutsceneText.text = "Jimmy: Eu morri?";
            }
            if (temp3 <= 50 && temp3 >= 45) //30
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: Something like that.";
                else
                    cutsceneText.text = "Narrador: Quase isso.";
            }
            if (temp3 <= 55 && temp3 >= 50) //35
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: What now?";
                else
                    cutsceneText.text = "Jimmy: E agora?";
            }
            if (temp3 <= 60 && temp3 >= 55) //40
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: Now you are scre@#*!";
                else
                    cutsceneText.text = "Narrador: E agora fud@#*!";
            }
            if (temp3 <= 65 && temp3 >= 60) //45
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: You were always been a devil, you deserve to be here.";
                else
                    cutsceneText.text = "Narrador: Você sempre foi um capeta, merece estar aqui.";
            }
            if (temp3 <= 70 && temp3 >= 65) //50
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: If I'am in hell, why am I talking to a cloud? Where is the devil?";
                else
                    cutsceneText.text = "Jimmy: Se eu estou no inferno, por que estou falando com uma nuvem? Cadê o capiroto?";
            }
            if (temp3 <= 75 && temp3 >= 70) //5
            {
                cutSceneFireLevelA.SetBool("ThirdScene", true);
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: The Sprites aren't ready!";
                else
                    cutsceneText.text = "Narrador: Os Sprites não estão prontos!";
            }
            if (temp3 <= 80 && temp3 >= 75) //10
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: And don't call me Cloud, I'am the Narrator. Or rather, Mr. Moreira.";
                else
                    cutsceneText.text = "Narrador: E não me chame de nuvem e sim narrador. Ou melhor, senhor Moreira.";
            }
            if (temp3 <= 85 && temp3 >= 80) //15
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: Mr. Cloud, I mean, Mr. Moreira, can I get out of here?";
                else
                    cutsceneText.text = "Jimmy: Senhor nuvem, quer dizer senhor Moreira, será que eu consigo sair daqui?";
            }
            if (temp3 <= 90 && temp3 >= 85) //20
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: There is only one way.";
                else
                    cutsceneText.text = "Narrador: Só existe uma forma.";
            }
            if (temp3 <= 95 && temp3 >= 90) //25
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "You must save your friends who fell here with you.";
                else
                    cutsceneText.text = "Você deve salvar seus amigos que caíram aqui junto com você.";
            }
            if (temp3 <= 100 && temp3 >= 95) //30
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "After that, you must steal the trident from the devil.";
                else
                    cutsceneText.text = "Após isso, você deve roubar o tridente do capiroto.";
            }
            if (temp3 <= 105 && temp3 >= 100) //35
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "The trident has the power to control time.";
                else
                    cutsceneText.text = "O tridente tem o poder de controlar o tempo.";
            }
            if (temp3 <= 110 && temp3 >= 105) //40
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "If you get the trident you will be able to revert the situation.";
                else
                    cutsceneText.text = "Se você conseguir o tridente poderá reverter sua situação.";
            }
            if (temp3 <= 115 && temp3 >= 110) //45
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "But don't forget that stealing it will not be easy.";
                else
                    cutsceneText.text = "Mas lembre-se, roubá-lo não será tarefa fácil.";
            }
            if (temp3 <= 120 && temp3 >= 115) //50
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: I will try! Thanks Mr. Cloud!";
                else
                    cutsceneText.text = "Jimmy: Vou tentar! Obrigado senhor nuvem!";
            }
            if (temp3 <= 125 && temp3 >= 120) //55
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: It's Mr. Moreira!";
                else
                    cutsceneText.text = "Narrador: É Senhor Moreira!";
            }
            if (temp3 <= 128 && temp3 >= 125) //58
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: Gee....";
                else
                    cutsceneText.text = "Jimmy: Coroi....";
                Invoke("PlayerCutscene", 1);
            }
            if (temp3 <= 132 && temp3 >= 128)
            {
                cutsceneText.text = "";
                tarjaPreta.SetActive(false);
            }
            if (temp3 <= 133 && temp3 >= 132)
            {
                beginGame = true;
                temp3 = 135;
            }

            if (beginGame)
            {
                level.SetActive(true);
                ceu.SetActive(true);
                isPlayingCutscene = false;
                Destroy(cutScene);
                Destroy(canvasCutscene);
            }

            if (playerCutscene.gameObject.activeSelf)
            {
                x += 5 * Time.deltaTime;
                playerCutscene.localPosition = new Vector3(x, playerCutscene.localPosition.y, playerCutscene.localPosition.z);
            }
        }
        #endregion

        #region VictoryScene Cutscene
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            temp6 += Time.deltaTime;
            if (GameOver.final == 1)
            {
                if (!dontRepeat4)
                {
                    albertVictorySceneA.Play("AlbertVictoryScene1");
                    dontRepeat4 = true;
                }

                if (temp6 <= 6 && temp6 >= 0)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: Hey Albert, did you like the game?";
                    else
                        cutsceneText.text = "Empresário: Ei Albert, o que achou do game?";
                }
                if (temp6 <= 12 && temp6 >= 6)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Albert: Hmm...";
                    else
                        cutsceneText.text = "Albert: Hum...";
                }
                if (temp6 <= 18 && temp6 >= 12)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Very good!";
                    else
                        cutsceneText.text = "Achei muito bom!";
                }
                if (temp6 <= 24 && temp6 >= 18)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Amazing graphics! It looked like the real world!";
                    else
                        cutsceneText.text = "Que gráficos! Parecia o mundo real!";
                }
                if (temp6 <= 30 && temp6 >= 24)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "I can't wait to play the final version!";
                    else
                        cutsceneText.text = "Não vejo a hora de jogar a versão final!";
                }
                if (temp6 <= 36 && temp6 >= 30)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: We are very happy to know that we are going to fill our money pocket! Eh well, I mean ...";
                    else
                        cutsceneText.text = "Empresário: Ficamos muito felizes em saber que vamos encher nosso bolso de dinheiro! Eh bem, quer dizer...";
                }
                if (temp6 <= 42 && temp6 >= 36)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "We are very happy to know that we are satisfying our players.";
                    else
                        cutsceneText.text = "Ficamos muito felizes em saber que estamos satisfazendo nossos jogadores.";
                }
                if (temp6 <= 48 && temp6 >= 42)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Thanks Albert!";
                    else
                        cutsceneText.text = "Obrigado Albert!";
                }
                if (temp6 <= 49 && temp6 >= 48)
                {
                    thanksObj.SetActive(true);
                    cutsceneText.text = "";
                    albertVictorySceneA.gameObject.SetActive(false);
                    tarjaPreta.SetActive(false);
                    temp6 = 50;
                }
            }
            else if (GameOver.final == 2)
            {
                if (!dontRepeat4)
                {
                    albertVictorySceneA.Play("AlbertVictoryScene1");
                    dontRepeat4 = true;
                }

                if (temp6 <= 6 && temp6 >= 0)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: Hey Albert, did you like the game?";
                    else
                        cutsceneText.text = "Empresário: Ei Albert, o que achou do game?";
                }
                if (temp6 <= 12 && temp6 >= 6)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Albert: Hmm...";
                    else
                        cutsceneText.text = "Albert: Hum...";
                }
                if (temp6 <= 18 && temp6 >= 12)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Despite some problems, It's very good!";
                    else
                        cutsceneText.text = "Apesar de alguns problemas, eu achei muito bom!";
                }
                if (temp6 <= 24 && temp6 >= 18)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Amazing graphics! It looked like the real world!";
                    else
                        cutsceneText.text = "Que gráficos! Parecia o mundo real!";
                }
                if (temp6 <= 30 && temp6 >= 24)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "I can't wait to play the final version";
                    else
                        cutsceneText.text = "Não vejo a hora de jogar a versão final!";
                }
                if (temp6 <= 36 && temp6 >= 30)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: We are very happy to know that we are going to fill our money pocket! Eh well, I mean ...";
                    else
                        cutsceneText.text = "Empresário: Ficamos muito felizes em saber que vamos encher nosso bolso de dinheiro! Eh bem, quer dizer...";
                }
                if (temp6 <= 42 && temp6 >= 36)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "We are very happy to know that we are satisfying our players.";
                    else
                        cutsceneText.text = "Ficamos muito felizes em saber que estamos satisfazendo nossos jogadores.";
                }
                if (temp6 <= 48 && temp6 >= 42)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "To fix the bugs, we'll need you to spend some time in captivity. Well, I mean, at the company headquarters.";
                    else
                        cutsceneText.text = "Para corrigir os bugs, precisaremos que você fique mais um tempo no cativeiro. Bem, quer dizer, na sede da empresa.";
                }
                if (temp6 <= 54 && temp6 >= 48)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Thanks Albert!";
                    else
                        cutsceneText.text = "Obrigado Albert!";
                }
                if (temp6 <= 55 && temp6 >= 54)
                {
                    thanksObj.SetActive(true);
                    cutsceneText.text = "";
                    albertVictorySceneA.gameObject.SetActive(false);
                    tarjaPreta.SetActive(false);
                    temp6 = 56;
                }
            }
            else if (GameOver.final == 3)
            {
                if (!dontRepeat4)
                {
                    albertVictorySceneA.Play("AlbertVictoryScene2");
                    dontRepeat4 = true;
                }

                if (temp6 <= 6 && temp6 >= 0)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: Hey Albert, did you like the game?";
                    else
                        cutsceneText.text = "Empresário: Ei Albert, o que achou do game?";
                }
                if (temp6 <= 12 && temp6 >= 6)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Albert: It's shit! I want my life back!";
                    else
                        cutsceneText.text = "Albert: Uma merda! Quero minha vida de volta!";
                }
                if (temp6 <= 18 && temp6 >= 12)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: Oh my God, stop the production!";
                    else
                        cutsceneText.text = "Empresário: Meu deus, parem a produção!";
                }
                if (temp6 <= 24 && temp6 >= 18)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Assistant: Sir, I have bad news.";
                    else
                        cutsceneText.text = "Assistente: Senhor, tenho péssimas notícias.";
                }
                if (temp6 <= 30 && temp6 >= 24)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "We've already made a million copies.";
                    else
                        cutsceneText.text = "Já fizemos um milhão de cópias.";
                }
                if (temp6 <= 36 && temp6 >= 30)
                {
                    if (PlayerPrefs.GetInt("Language") == 0)
                        cutsceneText.text = "Manager: Bury everything in the desert!";
                    else
                        cutsceneText.text = "Empresário: Enterrem tudo no deserto!";
                }
                if (temp6 <= 37 && temp6 >= 36)
                {
                    thanksObj.SetActive(true);
                    cutsceneText.text = "";
                    albertVictorySceneA.gameObject.SetActive(false);
                    tarjaPreta.SetActive(false);
                    temp6 = 38;
                }
            }
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                StartCoroutine(LoadAsynchronously(1));
                isPlayingCutscene = false;
            }

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (canSkipDesertCutscene)
                {
                    var menuJimmy = GameObject.Find("Main Camera").GetComponent<MenuJimmy>();
                    menuJimmy.StartGame();
                    CancelInvoke();
                    truckIdle.Stop();
                    canSkipDesertCutscene = false;
                }

                if (canSkipSchoolCutscene)
                {
                    CenaDeserto();
                    acdcBackInBlack.Stop();
                    acdcBackInBlack.enabled = false;
                    CancelInvoke();
                    canSkipSchoolCutscene = false;
                }
            }

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                level.SetActive(true);
                ceu.SetActive(true);
                isPlayingCutscene = false;
                Destroy(cutScene);
                Destroy(canvasCutscene);
            }
        }

        if (isCreditsTime)
        {
            if (canMoveCreditsText)
            {
                posXRTCredits -= 160 * Time.deltaTime;
                rtCreditsText.anchoredPosition = new Vector2(posXRTCredits, rtCreditsText.anchoredPosition.y);
                if (dontRepeat2)
                {
                    i++;
                    Invoke("CantMoveCreditsText", 3.575f);
                    Invoke("MoveCreditsText", 4.825f);
                    dontRepeat2 = false;
                }
            }

            if (i % 2 == 0 && !canMoveCreditsText)
            {
                posXRTCredits = 574.6f;
                rtCreditsText.anchoredPosition = new Vector2(posXRTCredits, rtCreditsText.anchoredPosition.y);
            }

            if (i == 1)
            {
                creditsTextUI.text = "Writers";
            }
            else if (i == 3)
            {
                creditsTextUI.text = "Cleber Araujo Lima";
            }
            else if (i == 5)
            {
                creditsTextUI.text = "Lucas Francisco de Araujo";
            }
            else if (i == 7)
            {
                creditsTextUI.text = "Artists";
            }
            else if (i == 9)
            {
                creditsTextUI.text = "Cleber Araujo Lima";
            }
            else if (i == 11)
            {
                creditsTextUI.text = "Felipe Martins";
            }
            else if (i == 13)
            {
                creditsTextUI.text = "Designers";
            }
            else if (i == 15)
            {
                creditsTextUI.text = "Cleber Araujo Lima";
            }
            else if (i == 17)
            {
                creditsTextUI.text = "Felipe Martins";
            }
            else if (i == 19)
            {
                creditsTextUI.text = "Lucas Francisco de Araujo";
            }
            else if (i == 21)
            {
                creditsTextUI.text = "Max da Mata Novo Guterres";
            }
            else if (i == 23)
            {
                creditsTextUI.text = "VFX Artist";
            }
            else if (i == 25)
            {
                creditsTextUI.text = "Felipe Martins";
            }
            else if (i == 27)
            {
                creditsTextUI.text = "Programmers";
            }
            else if (i == 29)
            {
                creditsTextUI.text = "Lucas Francisco de Araujo";
                isTheEndOfCredits = true;
            }
            else if (i == 31)
            {
                creditsTextUI.text = "Max da Mata Novo Guterres";
            }
            //else if (i == 33)
            //{
            //    creditsTextUI.text = "";
            //}
            else if (i == 33)
            {
                isCreditsTime = false;
            }
        }

        if (isTheEndOfCredits)
        {
            acdcBackInBlack.volume -= 0.05f * Time.deltaTime;
            if (acdcBackInBlack.volume <= 0)
            {
                CenaDeserto();
                isTheEndOfCredits = false;
            }
        }
    }

    #region levelCleber Cutscene Functions
    public void SiteJimmyAnimationEnd()
    {
        tarjaPreta.SetActive(true);
        albert.enabled = true;
        siteJimmy.enabled = false;
        isFirstSceneCompleted = true;
    }

    void PlayerCutscene()
    {
        playerCutscene.gameObject.SetActive(true);
    }

    public void JumpCutScene()
    {
        level.SetActive(true);
        Destroy(cutScene);
        Destroy(canvasCutscene);
    }
    #endregion

    void BusOutOfTheScreenSchool()
    {
        truckIdle.Stop();
        LogoJimmy();
        CreditsText.SetActive(true);
        isCreditsTime = true;
        CreditsText.transform.SetParent(cutsceneImagesJimmyA.transform);
        dontRepeat2 = true;
        Invoke("MoveCreditsText", 2);
        Invoke("CenaDeserto", 240);
    }

    void CenaDeserto()
    {
        ceuNuvemRT.offsetMin = new Vector2(rtCutSceneImages.offsetMin.x, canvasRT.sizeDelta.y);
        ceuNuvemRT.offsetMax = new Vector2(rtCutSceneImages.offsetMax.x, ceuNuvemRT.offsetMax.y);

        truckIdle.Stop();
        isCreditsTime = false;
        CreditsText.SetActive(false);
        logoJimmy.SetActive(false);
        cutsceneImagesJimmyA.SetBool("thirdPart", true);
        tarjaPreta.SetActive(true);
        isSecondSceneCompleted = true;
        canSkipSchoolCutscene = false;
        canSkipDesertCutscene = true;
        blackBG.SetActive(true);
    }

    void RaisesVolumeOverTime()
    {
        isPlaying = true;
    }

    void LogoJimmy()
    {
        logoJimmy.SetActive(true);
    }

    void MoveCreditsText()
    {
        dontRepeat2 = true;
        canMoveCreditsText = true;
    }

    void CantMoveCreditsText()
    {
        canMoveCreditsText = false;
    }

    IEnumerator LoadAsynchronously(int sceneIndex) //Tela de loading
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        canvasCutscene.SetActive(false);
        loadingText.transform.position = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + 3.5f, loadingText.transform.position.y);
        loadingText.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
