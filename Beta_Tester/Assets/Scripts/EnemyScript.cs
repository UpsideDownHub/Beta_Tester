using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public GameObject prefab;
    float speed;

    private void Start()
    {
        if (gameObject.name == "Enemy1")
            InvokeRepeating("SpawnBottle", 2, 6);
        else if (gameObject.name == "Enemy3")
            speed = transform.position.x;
    }

    private void Update()
    {
        speed -= 2 * Time.deltaTime;
        if (gameObject.name == "Enemy3")
        {
            transform.position = new Vector3(speed, transform.position.y, transform.position.z);
        }
    }

    void SpawnBottle()
    {
        Instantiate(prefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
    }
}
