﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public Transform cameraFollow;
    public Transform circleT;
    float x;
    float y;
    public static bool isLevelCompleted;
    bool isLevelInBeginning;
    public GameObject[] groundTraps;
    public GameObject[] groundLavas;

    //public Tilemap tileM;

    private void Start()
    {
        x = circleT.localScale.x;
        y = circleT.localScale.x;

        isLevelInBeginning = true;
    }

    private void Update()
    {
        if (isLevelInBeginning)
        {
            if (circleT.localScale.x >= 0 && circleT.localScale.x <= 70)
            {
                Time.timeScale = 0f;
                x += 0.8f;
                y += 0.8f;
                circleT.localScale = new Vector3(x, y, circleT.localScale.z);
            }
            else
            {
                Time.timeScale = 1f;
                circleT.localScale = new Vector3(70, 70, circleT.localScale.z);
                isLevelInBeginning = false;
            }
        }

        if (isLevelCompleted)
        {
            //if (SceneManager.GetActiveScene().buildIndex == 4)
            //    circleT.position = new Vector3(12.85f, -5.59f, circleT.position.z);
            //else if (SceneManager.GetActiveScene().buildIndex == 5)
            //    circleT.position = new Vector3(-2.29f, -8.57f, circleT.position.z);

            if (circleT.localScale.x > 0)
            {
                x -= 24 * Time.deltaTime;
                y -= 24 * Time.deltaTime;
                circleT.localScale = new Vector3(x, y, circleT.localScale.z);
            }
            else
            {
                circleT.localScale = new Vector3(0, 0, 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //Invoke("LoadScene", 1.5f);
                isLevelCompleted = false;
            }
        }

        //print(tileM.HasTile(new Vector3Int(0, 1, 0)));

        if (groundTraps != null)
        {
            for (int i = 0; i < groundTraps.Length; i++)
            {
                var moveObjects = groundTraps[i].GetComponent<MoveObjects>();
                if (moveObjects.isActivated)
                {
                    var animator = groundLavas[i].GetComponent<Animator>();
                    animator.enabled = true;
                    var boxC = groundLavas[i].GetComponent<BoxCollider>();
                    boxC.enabled = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (player.transform.position.x >= -24)
            {
                cameraFollow.position = new Vector3(player.transform.position.x, cameraFollow.position.y, cameraFollow.position.z);
            }
        }
    }

    //void LoadScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}
