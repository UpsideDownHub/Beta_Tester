using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscenes : MonoBehaviour
{

    public GameObject level;
    public GameObject canvasCutscene;
    public GameObject cutScene;
    public GameObject circle;
    public Transform circle2T;
    public Transform playerCutscene;
    public Text cutsceneText;
    public Text skipText;
    public Image siteJimmy;
    public Animator siteJimmyA;
    public Animator cutSceneFireLevelA;
    public Image albert;
    float temp;
    float temp2;
    float temp3;
    bool isFirstSceneCompleted;
    bool isSecondSceneCompleted;
    bool beginGame;
    float x;
    float x2;
    float y;

    private void Start()
    {
        temp = 0;
        temp2 = 0;
        temp3 = 0;
        x = playerCutscene.position.x;
        x2 = circle2T.localScale.x;
        y = circle2T.localScale.y;
        if (PlayerPrefs.GetInt("Language") == 0)
            skipText.text = "PRESS SPACE TO SKIP";
        else
            skipText.text = "PRESSIONE ESPAÇO PARA PULAR";

        var csj = GameObject.Find("CutSceneJump");
        if (csj != null)
            if (csj.GetComponent<cutSceneJump>().JumpCutScene)
                JumpCutScene();
    }

    private void Update()
    {
        temp += Time.deltaTime;
        if (temp <= 5 && temp >= 0)
        {
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
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Albert: Why are you asking me that?";
                else
                    cutsceneText.text = "Albert: Por que a pergunta?";
            }
            if (temp2 <= 36 && temp2 >= 32)
            {
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
                albert.enabled = false;
                cutScene.SetActive(true);
                isSecondSceneCompleted = true;
                isFirstSceneCompleted = false;
            }
        }

        if (isSecondSceneCompleted)
        {
            temp3 += Time.deltaTime;
            if (temp3 <= 5 && temp3 >= 0)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: (?) 10 versicle...";
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
            }
            if (temp3 <= 30 && temp3 >= 25) //10
            {
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
            if (temp3 <= 105 && temp3 >= 85) //35
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: There is only one way. You must save your friends who fell here with you. After that, you must steal the trident from the devil. The trident has the power to control time. If you get the trident you will be able to revert the situation. But don't forget that stealing it will not be easy.";
                else
                    cutsceneText.text = "Narrador: Só existe uma forma. Você deve salvar seus amigos que caíram aqui junto com você. Após isso, você deve roubar o tridente do capiroto. O tridente tem o poder de controlar o tempo. Se você conseguir o tridente poderá reverter sua situação. Mas lembre-se, roubá-lo não será tarefa fácil.";
            }
            if (temp3 <= 110 && temp3 >= 105) //40
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: I will try! Thanks Mr. Cloud!";
                else
                    cutsceneText.text = "Jimmy: Vou tentar! Obrigado senhor nuvem!";
            }
            if (temp3 <= 115 && temp3 >= 110) //45
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: It's Mr. Moreira!";
                else
                    cutsceneText.text = "Narrador: É Senhor Moreira!";
            }
            if (temp3 <= 118 && temp3 >= 115) //48
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: Gee....";
                else
                    cutsceneText.text = "Jimmy: Coroi....";
                Invoke("PlayerCutscene", 1);
            }
            if (temp3 <= 119 && temp3 >= 118)
            {
                cutsceneText.text = "";
                beginGame = true;
            }
        }

        if (beginGame)
        {
            if (circle2T.localScale.x > 0)
            {
                x2 -= 24 * Time.deltaTime;
                y -= 24 * Time.deltaTime;
                circle2T.localScale = new Vector3(x2, y, circle2T.localScale.z);
            }
            else
            {
                circle2T.localScale = new Vector3(0, 0, 0);
                circle.SetActive(true);
                level.SetActive(true);
                Destroy(cutScene);
                Destroy(canvasCutscene);
            }
        }

        if (playerCutscene.gameObject.activeSelf)
        {
            x += 5 * Time.deltaTime;
            playerCutscene.position = new Vector3(x, playerCutscene.position.y, playerCutscene.position.z);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            circle.SetActive(true);
            level.SetActive(true);
            Destroy(cutScene);
            Destroy(canvasCutscene);
        }
    }

    public void SiteJimmyAnimationEnd()
    {
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
        circle.SetActive(true);
        level.SetActive(true);
        Destroy(cutScene);
        Destroy(canvasCutscene);
    }
}
