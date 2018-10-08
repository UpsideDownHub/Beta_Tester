using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerScript : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    Rigidbody Rigidbody;
    CapsuleCollider CapsuleCollider;
    int verifyDistance = 4;
    List<List<int>> data;
    Vector3 position = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        this.data = PhaseCreationManager.data;
        Rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        if ((position.x + 1) <= (int)transform.position.x)
        {
            position = new Vector3((int)transform.position.x, (int)transform.position.y);
            VerifyNearPositions();
        }
    }

    void VerifyNearPositions()
    {
        var _x = (int)position.x;
        var _y = (int)position.y;
        for (int x = _x; x <= _x + verifyDistance; x++)
        {
            for (int y = data[x].Count - 1; y >= 0; y--)
            {
                if (data[x][y] != 0 && data[x][y] != 2)
                {
                    if (x == _x + 2 && Mathf.Abs(y - 10) > _y)
                    {
                        int height = Mathf.Abs(y - 10) - _y;
                        Rigidbody.AddForce(new Vector3(0, 300 + (50 * (height - 1)), 0));

                        //if()
                    }
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody.AddForce(new Vector3(0, 0, 0));
        Rigidbody.velocity = new Vector3(1.5f, Rigidbody.velocity.y, Rigidbody.velocity.z);
    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody.velocity = new Vector3(2, Rigidbody.velocity.y, Rigidbody.velocity.z);
    }

}
