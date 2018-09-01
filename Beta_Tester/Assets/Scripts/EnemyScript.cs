using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public GameObject prefab;

    private void Start()
    {
        if (gameObject.name == "Enemy1")
            InvokeRepeating("SpawnBottle", 2, 6);
        else if (gameObject.name == "Enemy2")
            Invoke("SpawnRain", 0);
    }

    void SpawnBottle()
    {
        Instantiate(prefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
    }

    void SpawnRain()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
