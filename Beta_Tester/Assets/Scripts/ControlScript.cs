using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour {

    Rigidbody2D _Rigidbody2D;
    Rigidbody2D Rigidbody2D { get { return _Rigidbody2D ?? (_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>()); } }

    CircleCollider2D _CircleCollider2D;
    CircleCollider2D CircleCollider2D { get { return _CircleCollider2D ?? (_CircleCollider2D = gameObject.GetComponent<CircleCollider2D>()); } }

    public LayerMask groundMask;

    void Update () {

        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 5, 0, 0);
        if (Input.GetKeyDown(KeyCode.Space) && CircleCollider2D.IsTouchingLayers(groundMask))
        {
            Rigidbody2D.AddForce(new Vector2(0,500));
        }
    }
}
