using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public GameObject spike1;
    public GameObject spike2;

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
                var boxcollider = hit.transform.GetComponent<BoxCollider>();
                if (boxcollider == null) return;
                if (boxcollider.name == "spike")
                {
                    spike1.SetActive(true);
                    spike2.SetActive(true);
                    boxcollider.transform.position = new Vector3(boxcollider.transform.position.x, 5.174f, 0);
                }
                var rigidbody = hit.transform.GetComponent<Rigidbody>();
                if (rigidbody == null) return;
                rigidbody.useGravity = true;
                if (rigidbody.name == "Trap")
                {
                    Destroy(rigidbody.gameObject);
                }
            }
        }
	}
}
