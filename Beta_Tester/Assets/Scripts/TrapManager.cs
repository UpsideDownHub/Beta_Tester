using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    float speed;
    Vector3 initialPosition;
    public bool ActivateTrap;

    private void Start()
    {
        if (gameObject.tag == "Poison")
            Destroy(gameObject, 3);
        else if (gameObject.name == "BOOM(Clone)" || gameObject.name == "BOOM 1(Clone)" || gameObject.name == "pedra puf(Clone)")
            Destroy(gameObject, 2f);
        if (gameObject.name == "Dust(Clone)")
            Destroy(gameObject, 4);


        if (gameObject.name == "serra" || gameObject.name == "serra(Clone)")
        {
            speed = 0;
            initialPosition = transform.position;
        }
    }

    private void Update()
    {
        if (gameObject.name == "serra" || gameObject.name == "serra(Clone)")
        {
            if (!ActivateTrap)
            {
                if (transform.position.y >= initialPosition.y)
                {
                    speed = -0.5f;
                }
                else if (transform.position.y <= initialPosition.y - 0.8f)
                {
                    speed = 0.5f;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
                Invoke("ResetTrap", 1);
        }
    }

    void GroundLavaAnim()
    {
        var animator = GetComponent<Animator>();
        animator.SetBool("EndAnimation", true);
    }

    public void ResetTrap() //serra
    {
        ActivateTrap = false;
        if (gameObject.name == "serra")
        {
            var rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
