using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public GameObject spike1;
    public GameObject spike2;
    public GameObject prefab;
    public MoveSpikes script;
    public EnemyScript script2;
    public EnemyScript script3;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "spike")
                {
                    spike1.SetActive(true);
                    spike2.SetActive(true);
                }

                if (hit.collider.name == "gelo (2)")
                {
                    script.speed = 3;
                }

                if (hit.collider.name == "Trap")
                {
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.name == "Enemy1")
                {
                    script2.prefab = prefab;
                }

                if (hit.collider.name == "Enemy4")
                {
                    script3.prefab = prefab;
                }

                if (hit.collider.gameObject.layer == 12)
                {
                    var rigidbody = hit.transform.GetComponent<Rigidbody>();
                    if (rigidbody == null) return;
                    rigidbody.useGravity = true;
                }
            }
        }
	}
}
