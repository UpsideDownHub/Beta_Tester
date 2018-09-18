using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public GameObject prefab;
    public GameObject prefab2;
    public GameObject prefab3;
    float speed;
    public static bool isClicked = false;

    private void Start()
    {
        if (gameObject.name == "Enemy1" || gameObject.name == "Enemy4")
            InvokeRepeating("SpawnBottle", 2, 6);
        else if (gameObject.name == "Enemy5")
            InvokeRepeating("SpawnBottle2", 2, 6);
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

        if (isClicked)
        {
            CancelInvoke();
            InvokeRepeating("SpawnBottle2", 0, 6);
            InvokeRepeating("SpawnBottle3", 0.5f, 6);
            InvokeRepeating("SpawnBottle4", 1, 6);
            isClicked = false;
        }
    }

    void SpawnBottle()
    {
        Instantiate(prefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
    }

    void SpawnBottle2()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    void SpawnBottle3()
    {
        Instantiate(prefab2, transform.position, Quaternion.identity);
    }

    void SpawnBottle4()
    {
        Instantiate(prefab3, transform.position, Quaternion.identity);
    }
}
