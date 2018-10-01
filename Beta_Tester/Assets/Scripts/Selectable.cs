using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public GameOverBetaTester gameOverBT;
    public GameObject spike1;
    public GameObject spike2;
    public GameObject prefab;
    public GameObject prefabPlanta;
    public MoveSpikes script;
    public EnemyScript script2;
    public EnemyScript script3;
    public Transform playerT;
    public PlayerScript3D playerScript;
    public BoxCollider boxC;
    float activateTrapTime;

    // Use this for initialization
    void Start () {
        activateTrapTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        activateTrapTime += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (activateTrapTime >= 2)
                {
                    if (hit.collider.name == "spike")
                    {
                        spike1.SetActive(true);
                        spike2.SetActive(true);
                    }

                    else if (hit.collider.name == "Planta")
                    {
                        Instantiate(prefabPlanta, hit.transform.position, Quaternion.identity);
                    }

                    else if (hit.collider.name == "gelo (2)")
                    {
                        script.speed = 3;
                        gameOverBT.slider.value += 0.15f;
                    }

                    else if (hit.collider.name == "Trap")
                    {
                        Destroy(hit.collider.gameObject);
                        gameOverBT.slider.value += 0.15f;
                    }

                    else if (hit.collider.name == "Enemy1")
                    {
                        script2.prefab = prefab;
                        gameOverBT.slider.value += 0.15f;
                    }

                    else if (hit.collider.name == "Enemy4")
                    {
                        script3.prefab = prefab;
                        gameOverBT.slider.value += 0.15f;
                    }

                    else if (hit.collider.name == "Enemy5")
                    {
                        EnemyScript.isClicked = true;
                    }

                    else if (hit.collider.tag == "Fall" || hit.collider.name == "PoisonE")
                    {
                        var rigidbody = hit.transform.GetComponent<Rigidbody>();
                        if (rigidbody == null) return;
                        rigidbody.useGravity = true;

                        if (hit.collider.name == "estalactite (1)" && playerT.position.x >= 22 && playerT.position.x <= 26)
                        {
                            playerScript.speed = 0;
                            Invoke("Walk", 2);
                            boxC.enabled = true;
                        }
                        else if (hit.collider.name == "estalactite" && playerT.position.x >= 66 && playerT.position.x <= 70.5f)
                        {
                            playerScript.speed = 0;
                            Invoke("Walk", 2);
                        }
                        else if (hit.collider.name == "gelo(1)" && playerT.position.x >= 2)
                        {

                        }
                    }
                    activateTrapTime = 0;
                }
            }
        }
	}

    void Walk()
    {
        playerScript.speed = 4;
    }
}
