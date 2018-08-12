using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Collider2D collider;
    SAP2D.SAP2DAgent agent;
    public LayerMask ground;
    Rigidbody2D _Rigidbody2D;
    Rigidbody2D Rigidbody2D { get { return _Rigidbody2D ?? GetComponent<Rigidbody2D>(); } }
    bool wasTouchingGround = true;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        agent = GetComponent<SAP2D.SAP2DAgent>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.angularVelocity = 0;
            Rigidbody2D.Sleep();
        }

        if (collider.IsTouchingLayers(ground))
        {
            agent.CanMove = true;
            if (!wasTouchingGround)
            {
                Rigidbody2D.velocity = Vector2.zero;
                Rigidbody2D.angularVelocity = 0;
                Rigidbody2D.Sleep();
            }
        }
        else
            agent.CanMove = false;

        wasTouchingGround = collider.IsTouchingLayers(ground);
    }
}
