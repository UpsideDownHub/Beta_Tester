using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    float speed;
    float temp;
    public bool ActivateTrap;

    private void Start()
    {
        if (gameObject.tag == "Poison")
            Destroy(gameObject, 3);
        else if (gameObject.name == "BOOM(Clone)" || gameObject.name == "pedra puf(Clone)")
            Destroy(gameObject, 2f);

        if (gameObject.name == "serra" || gameObject.name == "serra(Clone)")
        {
            speed = 0;
            temp = 1;
        }
    }

    private void Update()
    {
        if (gameObject.name == "serra" || gameObject.name == "serra(Clone)")
        {
            temp += Time.deltaTime;

            if (!ActivateTrap)
            {
                if (temp >= 1)
                {
                    speed = -0.5f;
                    if (temp >= 2)
                        temp = 0;
                }
                else if (temp < 1)
                {
                    speed = 0.5f;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
        }
    }

    void GroundLavaAnim()
    {
        var animator = GetComponent<Animator>();
        animator.SetBool("EndAnimation", true);
    }
}
