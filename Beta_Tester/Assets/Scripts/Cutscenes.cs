using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscenes : MonoBehaviour {

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
    //public float letterPause = 0.2f;
    //Coroutine textCoroutine;

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
    }

    private void Update()
    {
        temp += Time.deltaTime;
        if (temp <= 3 && temp >= 0)
        {
            if (PlayerPrefs.GetInt("Language") == 0)
                cutsceneText.text = "Manager: Are we recording? Oh ok.";
            else
                cutsceneText.text = "Empresário: Estamos gravando? Ah ok.";
        }
        if (temp <= 6 && temp >= 3)
        {
            if (PlayerPrefs.GetInt("Language") == 0)
                cutsceneText.text = "Good afternoon Albert. You have been selected to play the game Jimmy in the Hell.";
            else
                cutsceneText.text = "Boa tarde Albert. Você foi selecionado para jogar o game Jimmy in the Hell.";
        }
        if (temp <= 9 && temp >= 6)
        {
            if (PlayerPrefs.GetInt("Language") == 0)
                cutsceneText.text = "I hope you are comfortable.";
            else
                cutsceneText.text = "Espero que esteja confortável.";
        }
        if (temp <= 14 && temp >= 9)
        {
            if (PlayerPrefs.GetInt("Language") == 0)
                cutsceneText.text = "According to the regulation, we will film your gameplay and your reactions for profit. I mean, scientific purposes.";
            else
                cutsceneText.text = "De acordo com o regulamento, nós filmaremos o seu gameplay e suas reações para fins lucrativos. Ehh quer dizer científicos.";
        }
        if (temp <= 19 && temp >= 14)
        {
            if (PlayerPrefs.GetInt("Language") == 0)
                cutsceneText.text = "Your image and audio are being recorded, but safe with us. We always valorize the privacy of our contributors.";
            else
                cutsceneText.text = "Sua imagem e áudio estão sendo gravados, mas estão seguros conosco. Sempre prezamos a privacidade de nossos colaboradores.";
        }
        if (temp <= 20 && temp >= 19)
        {
            albert.enabled = false;
            cutsceneText.text = "";
            siteJimmy.enabled = true;
            siteJimmyA.enabled = true;
        }
        
        if (isFirstSceneCompleted)
        {
            temp2 += Time.deltaTime;
            if (temp2 <= 2 && temp2 >= 0)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Are we ready?";
                else
                    cutsceneText.text = "Estamos prontos?";
            }
            if (temp2 <= 4 && temp2 >= 2)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Do you need something?";
                else
                    cutsceneText.text = "Você precisa de algo?";
            }
            if (temp2 <= 6 && temp2 >= 4)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Albert: No, I'm fine.";
                else
                    cutsceneText.text = "Albert: Não, estou legal.";
            }
            if (temp2 <= 8 && temp2 >= 6)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Manager: Ok.";
                else
                    cutsceneText.text = "Empresário: Ok.";
            }
            if (temp2 <= 10 && temp2 >= 8)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Are your organs healthy?";
                else
                    cutsceneText.text = "Seus órgãos estão saudáveis?";
            }
            if (temp2 <= 12 && temp2 >= 10)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Albert: I think so.";
                else
                    cutsceneText.text = "Albert: Acho que sim.";
            }
            if (temp2 <= 14 && temp2 >= 12)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Manager: Are the kidneys good?";
                else
                    cutsceneText.text = "Empresário: Os rins estão bonzinhos?";
            }
            if (temp2 <= 16 && temp2 >= 14)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Albert: Why are you asking me that?";
                else
                    cutsceneText.text = "Albert: Por que a pergunta?";
            }
            if (temp2 <= 18 && temp2 >= 16)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Manager: Oh it's nothing, I'am just testing your reflexes.";
                else
                    cutsceneText.text = "Empresário: Ah nada, é só para testar seus reflexos.";
            }
            if (temp2 <= 20 && temp2 >= 18)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "We can start.";
                else
                    cutsceneText.text = "Podemos começar.";
            }
            if (temp2 <= 22 && temp2 >= 20)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Albert: Yay!";
                else
                    cutsceneText.text = "Albert: Demoro!";
            }
            if (temp2 <= 23 && temp2 >= 22)
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
            if (temp3 <= 3 && temp3 >= 0)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: (?) 10 versicle...";
                else
                    cutsceneText.text = "Narrador: Jerimonômio 10 versículo...";
            }
            if (temp3 <= 6 && temp3 >= 3)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Dubbing Assistant: Hey Mr. Moreira, you have to dub the game!";
                else
                    cutsceneText.text = "Assistente de dublagem: Ei senhor Moreira, é pra dublar o jogo!";
            }
            if (temp3 <= 9 && temp3 >= 6)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: Oh, Sorry. I am old my son.";
                else
                    cutsceneText.text = "Narrador: Ah, me enganei. Já estou velho meu filho.";
            }
            if (temp3 <= 12 && temp3 >= 9)
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: Wake up Jimmy!";
                else
                    cutsceneText.text = "Narrador: Acorde Jimmy!";
            }
            if (temp3 <= 15 && temp3 >= 12) //3
            {
                cutSceneFireLevelA.SetBool("SecondScene", true);
                cutsceneText.text = "";
            }
            if (temp3 <= 18 && temp3 >= 15) //6
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: WAKE UP JIMMY!!!";
                else
                    cutsceneText.text = "Narrador: ACORDE JIMMY!!!";
            }
            if (temp3 <= 21 && temp3 >= 18) //9
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: Oh my god, where am I?";
                else
                    cutsceneText.text = "Jimmy: Meu deus onde estou?";
            }
            if (temp3 <= 24 && temp3 >= 21) //12
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: In hell!";
                else
                    cutsceneText.text = "Narrador: No inferno!";
            }
            if (temp3 <= 27 && temp3 >= 24) //15
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: I died?";
                else
                    cutsceneText.text = "Jimmy: Eu morri?";
            }
            if (temp3 <= 30 && temp3 >= 27) //18
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: Something like that.";
                else
                    cutsceneText.text = "Narrador: Quase isso.";
            }
            if (temp3 <= 33 && temp3 >= 30) //21
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: What now?";
                else
                    cutsceneText.text = "Jimmy: E agora?";
            }
            if (temp3 <= 36 && temp3 >= 33) //24
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: Now you are scre@#*!";
                else
                    cutsceneText.text = "Narrador: E agora fud@#*!";
            }
            if (temp3 <= 39 && temp3 >= 36) //27
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: You were always been a devil, you deserve to be here.";
                else
                    cutsceneText.text = "Narrador: Você sempre foi um capeta, merece estar aqui.";
            }
            if (temp3 <= 42 && temp3 >= 39) //30
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: If I'am in hell, why am I talking to a cloud? Where is the devil?";
                else
                    cutsceneText.text = "Jimmy: Se eu estou no inferno, por que estou falando com uma nuvem? Cadê o capiroto?";
            }
            if (temp3 <= 45 && temp3 >= 42) //3
            {
                cutSceneFireLevelA.SetBool("ThirdScene", true);
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: The Sprites aren't ready!";
                else
                    cutsceneText.text = "Narrador: Os Sprites não estão prontos!";
            }
            if (temp3 <= 48 && temp3 >= 45) //6
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: And don't call me Cloud, I'am the Narrator. Or rather, Mr. Moreira.";
                else
                    cutsceneText.text = "Narrador: E não me chame de nuvem e sim narrador. Ou melhor, senhor Moreira.";
            }
            if (temp3 <= 51 && temp3 >= 48) //9
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: Mr. Cloud, I mean, Mr. Moreira, can I get out of here?";
                else
                    cutsceneText.text = "Jimmy: Senhor nuvem, quer dizer senhor Moreira, será que eu consigo sair daqui?";
            }
            if (temp3 <= 54 && temp3 >= 51) //12
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: There is only one way. You must save your friends who fell here with you. After that, you must steal the trident from the devil. The trident has the power to control time. If you get the trident you will be able to revert the situation. But don't forget that stealing it will not be easy.";
                else
                    cutsceneText.text = "Narrador: Só existe uma forma. Você deve salvar seus amigos que caíram aqui junto com você. Após isso, você deve roubar o tridente do capiroto. O tridente tem o poder de controlar o tempo. Se você conseguir o tridente poderá reverter sua situação. Mas lembre-se, roubá-lo não será tarefa fácil.";
            }
            if (temp3 <= 57 && temp3 >= 54) //15
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: I will try! Thanks Mr. Cloud!";
                else
                    cutsceneText.text = "Jimmy: Vou tentar! Obrigado senhor nuvem!";
            }
            if (temp3 <= 60 && temp3 >= 57) //18
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Narrator: It's Mr. Moreira!";
                else
                    cutsceneText.text = "Narrador: É Senhor Moreira!";
            }
            if (temp3 <= 63 && temp3 >= 60) //21
            {
                if (PlayerPrefs.GetInt("Language") == 0)
                    cutsceneText.text = "Jimmy: Gee....";
                else
                    cutsceneText.text = "Jimmy: Coroi....";
                Invoke("PlayerCutscene", 1);
            }
            if (temp3 <= 64 && temp3 >= 63)
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

    //public void Interaction()
    //{
    //    textCoroutine = StartCoroutine(TypeText());
    //    startInteraction.Invoke();
    //}

    /*IEnumerator TypeText()
    {
        foreach (char letter in ("Bem vindo Harry!").ToCharArray())
        {
            narrator.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }*/
}
