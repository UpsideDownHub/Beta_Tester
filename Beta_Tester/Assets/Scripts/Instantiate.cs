using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instantiate : MonoBehaviour {

    //public GameObject ballPrefab;
    public GameObject spikesPrefab;
    public GameObject fireBallPrefab;
    public Transform fireBallEnemy;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
            InvokeRepeating("Spikes", 0, 3);

        if (SceneManager.GetActiveScene().buildIndex == 3)
            InvokeRepeating("FireBall", 4, 2.5f);

        //InvokeRepeating("Ball", 4, 6);
    }

    void Spikes()
    {
        Instantiate(spikesPrefab, transform.position, Quaternion.identity);
    }

    void FireBall()
    {
        Instantiate(fireBallPrefab, fireBallEnemy.position, Quaternion.identity);
    }

    //void Ball()
    //{
    //    Instantiate(ballPrefab, new Vector3(44.83f, 20.2f, 0), Quaternion.identity);
    //}
}
