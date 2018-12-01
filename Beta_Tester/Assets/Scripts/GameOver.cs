﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    GameObject player;
    //List<GameObject> groundObjects;
    //List<Transform> groundObjectsT;

    //Jimmy in the hell
    public bool ended = false;
    public static bool inGame = true;
    public GameObject gameOverPanel;
    //public List<Image> vidas;
    public Text vidasText;
    public Button button;
    //BetaTester
    public Slider lifeBetaTester;
    public Image betaTester;
    public Sprite albert1;
    public Sprite albert2;
    public Sprite albert3;
    public Sprite albert4;
    public Sprite albert5;
    public Sprite albert6;
    public Sprite albert7;
    public Sprite albert8;
    public Sprite albert9;
    public Sprite albert10;
    public static int final;

    private void Update()
    {
        if (!inGame) return;
        //foreach (var vida in vidas)
        //    vida.enabled = false;

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (PlayerScript3D.isInstantiated)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player != null) { 
                    player.GetComponent<Transform>();
                    PlayerScript3D.isInstantiated = false;
                }
            }

            if (player != null)
                if (player.transform.position.y <= 0 && !ended)
                    Assets.Scripts.PlayerAttrs.life = 0;
        }

        #region JimmyInTheHell
        //vidas.ForEach(x => x.enabled = false);

        if (Assets.Scripts.PlayerAttrs.life == 0 && !ended)
        {

            var phase = Assets.Scripts.DAL.BetaTesterContext.Phase.GetData().SingleOrDefault(x => x.FileId == Assets.Scripts.DAL.BetaTesterContext.FileId);

            if (phase != null)
            {
                phase.Dies++;
                Assets.Scripts.DAL.BetaTesterContext.Phase.Update(phase);
            }
            gameOverPanel.SetActive(true);
            button.Select();
            ended = true;
        }

        //if (SceneManager.GetActiveScene().buildIndex != 5)
        vidasText.text = "x" + Assets.Scripts.PlayerAttrs.life.ToString();
        //else
        //    vidasText.text = "x" + AIPlayerScript.life.ToString();

        //else
        //    for (int i = 0; i < PlayerScript3D.life; i++)
        //        vidas[i].enabled = true;
        #endregion

        #region BetaTester
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (!GameManager.isLevelCompleted)
            {
                lifeBetaTester.value -= 0.001f;
            }
            else
            {
                if (lifeBetaTester.value >= 0.67f)
                    final = 1;
                else if (lifeBetaTester.value <= 0.34f)
                    final = 3;
                else
                    final = 2;
            }

            if (lifeBetaTester.value >= 0.67f)
            {
                betaTester.sprite = albert8;
            }
            else if (lifeBetaTester.value >= 0.34f)
            {
                betaTester.sprite = albert5;
            }
            else if (lifeBetaTester.value > 0)
            {
                betaTester.sprite = albert1;
            }
        }
        #endregion
    }

    public void Retry()
    {
        Assets.Scripts.PlayerAttrs.life = 5;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void RetryPhaseCreation()
    {
        Assets.Scripts.PlayerAttrs.life = 5;
        gameOverPanel.SetActive(false);
        player.gameObject.SetActive(true);
        player.transform.position = PlayerScript3D.initialPosition;
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
