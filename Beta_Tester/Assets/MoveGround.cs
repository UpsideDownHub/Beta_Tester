using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveGround : MonoBehaviour
{

    List<GameObject> objsToMove = new List<GameObject>();

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse0)) //Input.GetKey(KeyCode.LeftControl) && 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);
            if (objsToMove.Count == 0)
            {
                print(string.Join(", ", hits.Select(x => x.collider.gameObject.name).ToArray()));
                if (hits.Count(x => x.collider.gameObject.tag == "Ground") > 0 && !hits.Any(x => x.collider.gameObject.tag != "Ground"))
                {
                    foreach (var hit in hits)
                        objsToMove.Add(hit.collider.gameObject);
                }
            }
            else
            {
                var newP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                foreach (var obj in objsToMove)
                    obj.transform.position = new Vector3((int)newP.x + 1, (int)newP.y);

                objsToMove = new List<GameObject>();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            objsToMove = new List<GameObject>();
        }
    }
}
