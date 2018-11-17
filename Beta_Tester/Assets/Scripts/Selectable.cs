using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour {

    public Slider timeToClickSlider;
    public GameOver gameOver;
    public GameObject prefab;
    public GameObject prefabPlanta;
    public GameObject prefabSpellBind;
    public Transform playerT;
    //public Text textTime;
    float activateTrapTime;

    // Use this for initialization
    void Start () {
        activateTrapTime = 2;
	}
	
	// Update is called once per frame
	void Update () {
        //textTime.text = (activateTrapTime > 2? 0 : Mathf.Abs(activateTrapTime - 2)).ToString();
        activateTrapTime += Time.deltaTime;
        timeToClickSlider.value = CalculateTimeToClick();

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
                        activateTrapTime = 0;
                    }

                    else if (hit.collider.name == "gelo (2)")
                    {
                        var moveObjects = hit.collider.gameObject.GetComponent<MoveObjects>();
                        moveObjects.speed = 3;
                        gameOver.lifeBetaTester.value += 0.15f;
                        activateTrapTime = 0;
                    }

                    else if (hit.collider.name == "Enemy1")
                    {
                        var enemyScript = hit.collider.gameObject.GetComponent<EnemyScript>();
                        enemyScript.prefab = prefab;
                        gameOver.lifeBetaTester.value += 0.15f;
                        activateTrapTime = 0;
                    }

                    else if (hit.collider.name == "Enemy5")
                    {
                        var enemyScript = hit.collider.gameObject.GetComponent<EnemyScript>();
                        enemyScript.isClicked = true;
                        activateTrapTime = 0;
                    }

                    else if (hit.collider.tag == "Trap")
                    {
                        var animator = hit.collider.gameObject.GetComponent<Animator>();
                        var boxC = hit.collider.gameObject.GetComponent<BoxCollider>();
                        var moveObjects = hit.collider.GetComponent<MoveObjects>();

                        if (hit.transform.position.x < playerT.position.x - 2)
                        {
                            boxC.size = new Vector3(boxC.size.x, 1.962251f, boxC.size.z);
                            boxC.center = new Vector3(boxC.center.x, 0.02961075f, boxC.center.z);
                            animator.enabled = true;
                            boxC.enabled = true;
                            animator.SetBool("Flying", true);
                            moveObjects.isActivated = true;
                            Instantiate(prefabSpellBind, hit.collider.transform);

                            if (!moveObjects.Activated)
                                activateTrapTime = 0;
                            moveObjects.Activated = true;
                        }
                        else if (hit.transform.position.x > playerT.position.x + 2)
                        {
                            animator.enabled = true;
                            var boxC2 = hit.collider.gameObject.AddComponent<BoxCollider>();
                            boxC2.center = new Vector3(-0.1026254f, -0.4772545f, 0);
                            boxC2.size = new Vector3(1.438596f, 0.7692058f, 0.2f);

                            if (!moveObjects.Activated)
                                activateTrapTime = 0;
                            moveObjects.Activated = true;
                        }
                    }

                    else if (hit.collider.tag == "Fall" || hit.collider.name == "PoisonE")
                    {
                        var rigidbody = hit.transform.GetComponent<Rigidbody>();
                        if (rigidbody == null) return;
                        rigidbody.useGravity = true;
                        activateTrapTime = 0;
                    }
                }
            }
        }
	}

    float CalculateTimeToClick()
    {
        return activateTrapTime / 2;
    }
}
