using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript3D : MonoBehaviour {

    public float speed;
    private float x;
    public static int life = 5;
    public static bool speedDirection = true;
    public Image vida1;
    public Image vida2;
    public Image vida3;
    public Image vida4;
    public Image vida5;
    public Rigidbody rb;

    void Start()
    {
        x = transform.position.x;
    }

    private void Update()
    {
        if (life == 5)
        {
            vida1.enabled = true;
            vida2.enabled = true;
            vida3.enabled = true;
            vida4.enabled = true;
            vida5.enabled = true;
        }
        else if (life == 4)
        {
            vida1.enabled = true;
            vida2.enabled = true;
            vida3.enabled = true;
            vida4.enabled = true;
            vida5.enabled = false;
        }
        else if (life == 3)
        {
            vida1.enabled = true;
            vida2.enabled = true;
            vida3.enabled = true;
            vida4.enabled = false;
            vida5.enabled = false;
        }
        else if (life == 2)
        {
            vida1.enabled = true;
            vida2.enabled = true;
            vida3.enabled = false;
            vida4.enabled = false;
            vida5.enabled = false;
        }
        else if (life == 1)
        {
            vida1.enabled = true;
            vida2.enabled = false;
            vida3.enabled = false;
            vida4.enabled = false;
            vida5.enabled = false;
        }
        else if (life <= 0)
        {
            vida1.enabled = false;
            vida2.enabled = false;
            vida3.enabled = false;
            vida4.enabled = false;
            vida5.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (speedDirection)
            x += speed * Time.deltaTime;
        else
            x -= speed * Time.deltaTime;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        if (transform.position.y <= -30)
        {
            print("morreu pela queda");
        }
    }
}
