using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    Transform playerT;
    public GameObject prefab;
    public GameObject prefab2;
    public GameObject prefab3;
    float speed;
    float speed2;
    float speed3;
    public static bool isClicked = false;
    public static bool isClicked2 = false;

    private void Start()
    {
        playerT = GameObject.Find("Player").GetComponent<Transform>();

        if (gameObject.name == "Enemy1" || gameObject.name == "Enemy4")
            InvokeRepeating("SpawnBottle", 2, 6);
        else if (gameObject.name == "Enemy5" || gameObject.name == "Enemy6")
            InvokeRepeating("SpawnBottle2", 2, 6);
        else if (gameObject.name == "Enemy3" || gameObject.name == "Enemy3 (1)")
            speed = transform.position.x;
        else if (gameObject.name == "Enemy3 (2)" || gameObject.name == "Enemy3 (3)")
            speed2 = transform.position.x;
        else if (gameObject.name == "Enemy3 (4)" || gameObject.name == "Enemy3 (5)" || gameObject.name == "Enemy3 (6)")
            speed3 = transform.position.x;
    }

    private void Update()
    {
        if ((gameObject.name == "Enemy3" || gameObject.name == "Enemy3 (1)") && playerT.position.x >= 102.2f)
        {
            speed -= 2 * Time.deltaTime;
            transform.position = new Vector3(speed, transform.position.y, transform.position.z);
        }
        else if ((gameObject.name == "Enemy3 (2)" || gameObject.name == "Enemy3 (3)") && playerT.position.x >= 157)
        {
            speed2 -= 2 * Time.deltaTime;
            transform.position = new Vector3(speed2, transform.position.y, transform.position.z);
        }
        else if ((gameObject.name == "Enemy3 (4)" || gameObject.name == "Enemy3 (5)" || gameObject.name == "Enemy3 (6)") && playerT.position.x >= 293.7f)
        {
            speed3 -= 2 * Time.deltaTime;
            transform.position = new Vector3(speed3, transform.position.y, transform.position.z);
        }

        if (isClicked && gameObject.name == "Enemy5")
        {
            ClickInteraction();
        }
        else if (isClicked2 && gameObject.name == "Enemy6")
        {
            ClickInteraction();
        }

        if (playerT == null) return;

        if ((gameObject.name == "Enemy3" || gameObject.name == "Enemy3 (1)" || gameObject.name == "Enemy3 (2)" || gameObject.name == "Enemy3 (3)" || gameObject.name == "Enemy3 (4)" || gameObject.name == "Enemy3 (5)" || gameObject.name == "Enemy3 (6)") && transform.position.x <= -100)
            Destroy(gameObject);
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
        Instantiate(prefab2, transform.position, prefab2.transform.rotation);
    }

    void SpawnBottle4()
    {
        Instantiate(prefab3, transform.position, prefab3.transform.rotation);
    }

    void ClickInteraction()
    {
        CancelInvoke();
        InvokeRepeating("SpawnBottle2", 0, 6);
        InvokeRepeating("SpawnBottle3", 0.5f, 6);
        InvokeRepeating("SpawnBottle4", 1, 6);
        isClicked = false;
        isClicked2 = false;
    }
}
