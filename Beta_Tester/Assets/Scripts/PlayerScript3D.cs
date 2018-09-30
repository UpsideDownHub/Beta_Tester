using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript3D : MonoBehaviour {

    public Rigidbody rb;
    public Animator animator;
    public SpriteRenderer sr;
    public float speed;
    private float x;
    bool moving;
    public static int life = 5;
    public static bool speedDirection = true;
    public Transform groundCheck;
    public bool grounded;
    public LayerMask whatIsGround;

    void Start()
    {
        x = transform.position.x;
    }

    private void Update()
    {
        if (life <= 0)
            gameObject.SetActive(false);

        //if (speedDirection)
        //    x += speed * Time.deltaTime;
        //else
        //    x -= speed * Time.deltaTime;
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            moving = false;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            x -= speed * Time.deltaTime;
            moving = true;
            sr.flipX = true;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            x += speed * Time.deltaTime;
            moving = true;
            sr.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && grounded && rb.velocity.y == 0)
        {
            rb.AddForce(new Vector3(0, 200, 0));
        }

        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        if (transform.position.y <= -30)
        {
            print("morreu pela queda");
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

        //print((int)rb.velocity.x + "/" + (int)rb.velocity.y);
        animator.SetBool("jump", !grounded);
        animator.SetBool("moving", moving);
    }
}
