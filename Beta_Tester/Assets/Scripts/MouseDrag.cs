using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    bool Draging;
    Vector3 mouseP;
    Transform objT;
    Rigidbody objRb;

    private void FixedUpdate()
    {
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
        
        if (Input.GetMouseButton(0))
        {
            Draging = true;
        }
        else
        {
            Draging = false;
            objT = null;
            if (objRb != null)
            {
                objRb.useGravity = true;
            }
            else
                return;
            //mexer aqui no rigidbody
        }

        if (Draging)
        {
            mouseP = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            if (objT == null)
                return;
            else
                objT.position = mouseP;
        }
    }
}
