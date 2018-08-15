using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorManager : MonoBehaviour
{
    public GameObject TrapDoor1;
    public GameObject TrapDoor2;
    [Range(0, 10)]
    float openLength = 2;
    bool? open;
    Vector3 maxOpeningTrapDoor1;
    Vector3 maxOpeningTrapDoor2;
    Vector3 minOpeningTrapDoor1;
    Vector3 minOpeningTrapDoor2;


    private void Awake()
    {
        maxOpeningTrapDoor1 = TrapDoor1.transform.position + new Vector3(-openLength, 0, 0);
        maxOpeningTrapDoor2 = TrapDoor2.transform.position + new Vector3(openLength, 0, 0);
        minOpeningTrapDoor1 = TrapDoor1.transform.position;
        minOpeningTrapDoor2 = TrapDoor2.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (open.HasValue)
            if (Vector3.Distance(TrapDoor1.transform.position, maxOpeningTrapDoor1) > 0.1f && Vector3.Distance(TrapDoor2.transform.position, maxOpeningTrapDoor2) > 0.1f && !open.Value ||
                Vector3.Distance(TrapDoor1.transform.position, minOpeningTrapDoor1) > 0.1f && Vector3.Distance(TrapDoor2.transform.position, minOpeningTrapDoor2) > 0.1f && open.Value)
                MovePlataforms();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    //StartCoroutine(MovePlataforms());
                    if (!open.HasValue)
                    {
                        open = true;
                        return;
                    }
                    open = open.Value ? false : true;
                }
            }
        }
    }

    void MovePlataforms()
    {
        if (!open.Value)
        {
            TrapDoor1.transform.position = Vector3.MoveTowards(TrapDoor1.transform.position, maxOpeningTrapDoor1, 5 * Time.deltaTime);
            TrapDoor2.transform.position = Vector3.MoveTowards(TrapDoor2.transform.position, maxOpeningTrapDoor2, 5 * Time.deltaTime);
            //if (TrapDoor1.transform.position == maxOpeningTrapDoor1 && TrapDoor2.transform.position == maxOpeningTrapDoor2)
            //StopCoroutine(MovePlataforms());
        }
        else
        {
            TrapDoor1.transform.position = Vector3.MoveTowards(TrapDoor1.transform.position, minOpeningTrapDoor1, 5 * Time.deltaTime);
            TrapDoor2.transform.position = Vector3.MoveTowards(TrapDoor2.transform.position, minOpeningTrapDoor2, 5 * Time.deltaTime);
            //if (TrapDoor1.transform.position == minOpeningTrapDoor1 && TrapDoor2.transform.position == minOpeningTrapDoor2)
            //StopCoroutine(MovePlataforms());
        }
        //yield return new WaitForSeconds(0.30f);
    }


}
