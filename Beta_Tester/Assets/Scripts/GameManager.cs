using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public Transform cameraFollow;
    public GameObject[] groundTraps;
    public GameObject[] groundLavas;
    public GameObject[] embers;
    public static bool isLevelCompleted;

    //float minPosC;
    //float maxPosC;
    //public GameObject prefab;

    //public Tilemap tileM;

    private void Start()
    {
        isLevelCompleted = false;

        //minPosC = (-Camera.main.orthographicSize * Camera.main.aspect) * 2;
        //maxPosC = (Camera.main.orthographicSize * Camera.main.aspect) * 2;

        //Instantiate(prefab, tileM.GetCellCenterLocal(new Vector3Int (0,-1,0)), Quaternion.identity, tileM.transform.parent);
    }

    private void Update()
    {
        //print(tileM.HasTile(new Vector3Int(0, 1, 0)));

        //Ativação da lava ao subir a pedra
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
        //Ativação das embers
        if (embers != null)
        {
            for (int j = 0; j < embers.Length; j++)
            {
                if (player.transform.position.x < embers[j].transform.position.x - 31.39f || player.transform.position.x > embers[j].transform.position.x + 31.39f)
                    embers[j].SetActive(false);
                else if (player.transform.position.x > embers[j].transform.position.x - 31.39f)
                    embers[j].SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            cameraFollow.position = new Vector3(player.transform.position.x, cameraFollow.position.y, cameraFollow.position.z);
        }
    }
}
