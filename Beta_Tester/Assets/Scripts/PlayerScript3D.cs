using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript3D : MonoBehaviour {

    SAP2D.SAP2DAgent agent;
    Rigidbody _Rigidbody;
    Rigidbody Rigidbody { get { return _Rigidbody ?? GetComponent<Rigidbody>(); } }
    bool collided = false;

    void Start()
    {
        agent = GetComponent<SAP2D.SAP2DAgent>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody.velocity = Vector2.zero;
            //Rigidbody.angularVelocity = 0;
            Rigidbody.Sleep();
        }

        if (collided)
        {
            agent.CanMove = true;
        }
        else
            agent.CanMove = false;

        if (transform.position.y <= -3.56)
        {
            print("morreu pela queda");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            collided = true;
        }
    }
}
