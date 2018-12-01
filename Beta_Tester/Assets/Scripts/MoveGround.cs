using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MoveGround : MonoBehaviour
{
    //public Text blockNumber;
    public static int maxGoundMove = 5;
    public static int movedGrounds = 0;

    List<GameObject> objsToMove = new List<GameObject>();

    void Update()
    {
        //blockNumber.text = (maxGoundMove - movedGrounds).ToString();
        if (movedGrounds <= maxGoundMove)
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
                    movedGrounds++;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                objsToMove = new List<GameObject>();
            }
        }
    }
}
