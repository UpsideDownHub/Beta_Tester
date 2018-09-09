using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript3D : MonoBehaviour {

    public float speed;
    private float x;

    void Start()
    {
        x = transform.position.x;
    }

    void FixedUpdate()
    {
        x += speed * Time.deltaTime;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        if (transform.position.y <= -30)
        {
            print("morreu pela queda");
        }
    }
}
