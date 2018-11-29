using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    Transform playerT;
    //List<GameObject> groundObjects;
    //List<Transform> groundObjectsT;

    //Jimmy in the hell
    public bool ended = false;
    public GameObject gameOverPanel;
    //public List<Image> vidas;
    public Text vidasText;
    public Button button;
    //BetaTester
    public Slider lifeBetaTester;

    private void Update()
    {
        //foreach (var vida in vidas)
        //    vida.enabled = false;

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (PlayerScript3D.isInstantiated)
            {
                playerT = GameObject.Find("_Player(Clone)").GetComponent<Transform>();
                PlayerScript3D.isInstantiated = false;
            }

            if (playerT != null)
                if (playerT.position.y <= 0)
                    PlayerScript3D.life = 0;
        }

        #region JimmyInTheHell
        //vidas.ForEach(x => x.enabled = false);

        if (PlayerScript3D.life == 0 && !ended)
        {

            var phase = Assets.Scripts.DAL.BetaTesterContext.Phase.GetData().Single(x => x.FileId == Assets.Scripts.DAL.BetaTesterContext.FileId);

            phase.Dies++;
            Assets.Scripts.DAL.BetaTesterContext.Phase.Update(phase);

            gameOverPanel.SetActive(true);
            button.Select();
        }

        //if (SceneManager.GetActiveScene().buildIndex != 5)
        vidasText.text = "x" + PlayerScript3D.life.ToString();
        //else
        //    vidasText.text = "x" + AIPlayerScript.life.ToString();

        //else
        //    for (int i = 0; i < PlayerScript3D.life; i++)
        //        vidas[i].enabled = true;
        #endregion

        #region BetaTester
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            lifeBetaTester.value -= 0.001f;

            if (lifeBetaTester.value == 0)
            {
                //print("Game Over Beta Tester");
            }
        }
        #endregion
    }

    public void Retry()
    {
        PlayerScript3D.life = 5;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void RetryPhaseCreation()
    {
        PlayerScript3D.life = 5;
        gameOverPanel.SetActive(false);
        playerT.gameObject.SetActive(true);
        playerT.transform.position = PlayerScript3D.initialPosition;
        //for (int i = 0; i < groundObjects.Count; i++)
        //{
        //    groundObjects[i].transform.position = groundObjectsT[i].position;
        //}
    }

    //void Teste()
    //{
    //    groundObjects = (GameObject.FindGameObjectsWithTag("Ground")).ToList();

    //    for (int j = 0; j < groundObjects.Count; j++)
    //    {
    //        groundObjectsT[j] = groundObjects[j].transform;
    //    }
    //}
}
