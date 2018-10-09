using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    bool Draging;
    Vector3 mouseP;
    Transform objT;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                objT = hit.collider.GetComponent<Transform>();
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            Draging = true;
        }
        else
        {
            Draging = false;
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
