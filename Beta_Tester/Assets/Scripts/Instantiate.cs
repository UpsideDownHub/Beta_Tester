using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instantiate : MonoBehaviour {

    //public GameObject ballPrefab;
    public GameObject spikesPrefab;
    public GameObject fireBallPrefab;
    public Transform fireBallEnemy;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");

        if (SceneManager.GetActiveScene().buildIndex == 4)
            InvokeRepeating("Spikes", 0, 3);

        if (SceneManager.GetActiveScene().buildIndex == 3)
            InvokeRepeating("FireBall", 0, 3);

        //InvokeRepeating("Ball", 4, 6);
    }

    private void Update()
    {
        if (!player.activeSelf)
            CancelInvoke();
    }

    void Spikes()
    {
        Instantiate(spikesPrefab, transform.position, Quaternion.identity);
    }

    void FireBall()
    {
        Instantiate(fireBallPrefab, fireBallEnemy.position, fireBallPrefab.transform.rotation);
    }

    //void Ball()
    //{
    //    Instantiate(ballPrefab, new Vector3(44.83f, 20.2f, 0), Quaternion.identity);
    //}
}
