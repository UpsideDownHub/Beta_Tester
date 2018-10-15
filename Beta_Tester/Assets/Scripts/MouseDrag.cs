using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseDrag : MonoBehaviour
{
    Vector3 mouseP;
    Transform objT;
    Rigidbody objRb;
    public Tilemap tileM;

    private void Update()
    {
        print(tileM.HasTile(new Vector3Int(0, 1, 0)));

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Drag")
                {
                    objT = hit.collider.GetComponent<Transform>();
                    objRb = hit.collider.GetComponent<Rigidbody>();
                }
            }
        }
    }

    private void FixedUpdate()
    {        
        if (Input.GetMouseButton(0))
        {
            mouseP = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            if (objT == null)
                return;
            else
                objT.position = mouseP;
        }
        else
        {
            objT = null;
            if (objRb != null)
            {
                objRb.useGravity = true;
            }
            else
                return;
            //mexer aqui no rigidbody
        }
    }
}
