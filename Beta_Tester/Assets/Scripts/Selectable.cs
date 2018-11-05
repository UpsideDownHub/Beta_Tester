using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public GameOver gameOver;
    public GameObject prefab;
    public GameObject prefabPlanta;
    public GameObject prefabSpellBind;
    public Transform playerT;
    float activateTrapTime;

    // Use this for initialization
    void Start () {
        activateTrapTime = 2;
	}
	
	// Update is called once per frame
	void Update () {
        activateTrapTime += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            List<RaycastHit> _hit = Physics.RaycastAll(ray).Where(a => a.collider.tag == "Selectable" || a.collider.tag == "Trap" || a.collider.tag == "Fall").ToList();

            if (_hit.Count > 0)
            {
                RaycastHit hit = _hit.First();

                if (activateTrapTime >= 2)
                {
                    if (hit.collider.name == "Planta")
                    {
                        Instantiate(prefabPlanta, hit.transform.position, Quaternion.identity);
                    }

                    else if (hit.collider.name == "gelo (2)")
                    {
                        var moveObjects = hit.collider.gameObject.GetComponent<MoveObjects>();
                        moveObjects.speed = 3;
                        gameOver.slider.value += 0.15f;
                    }

                    else if (hit.collider.name == "Enemy1")
                    {
                        var enemyScript = hit.collider.gameObject.GetComponent<EnemyScript>();
                        enemyScript.prefab = prefab;
                        gameOver.slider.value += 0.15f;
                    }

                    else if (hit.collider.name == "Enemy5")
                    {
                        var enemyScript = hit.collider.gameObject.GetComponent<EnemyScript>();
                        enemyScript.isClicked = true;
                    }

                    else if (hit.collider.tag == "Trap")
                    {
                        var animator = hit.collider.gameObject.GetComponent<Animator>();
                        var boxC = hit.collider.gameObject.GetComponent<BoxCollider>();

                        if (hit.transform.position.x < playerT.position.x - 2)
                        {
                            animator.enabled = true;
                            boxC.enabled = true;
                            animator.SetBool("Flying", true);
                            var moveObjects = hit.collider.GetComponent<MoveObjects>();
                            moveObjects.isActivated = true;
                            Instantiate(prefabSpellBind, hit.collider.transform);
                        }
                        else if (hit.transform.position.x > playerT.position.x + 2)
                        {
                            animator.enabled = true;
                            boxC.enabled = true;
                        }
                    }

                    else if (hit.collider.tag == "Fall" || hit.collider.name == "PoisonE")
                    {
                        var rigidbody = hit.transform.GetComponent<Rigidbody>();
                        if (rigidbody == null) return;
                        rigidbody.useGravity = true;
                    }
                    activateTrapTime = 0;
                }
            }
        }
	}
}
