using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour {

    public float speed;
    //FireBall
    Transform target;
    Vector3 lastPosition;
    float x;
    bool isInLastPosition = false;
   
    //Spikes
    float y;

    private void Start()
    {   //FireBall
        target = GameObject.Find("Player").GetComponent<Transform>();
        if (target == null) Destroy(this.gameObject);
        lastPosition = target.position;
        x = lastPosition.x;

        //Spikes
        y = transform.position.y;
    }

    private void FixedUpdate()
    {   //FireBall
        if (gameObject.name == "FireBall(Clone)" || gameObject.name == "FireBall2(Clone)")
        {
            if (!isInLastPosition)
                transform.position = Vector3.MoveTowards(transform.position, lastPosition, 10 * Time.deltaTime);
            else
            {
                x += speed * Time.deltaTime;
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }

            if (transform.position == lastPosition)
            {
                isInLastPosition = true;
            }
        }
        
        //Spikes
        else if (gameObject.name == "Spikes(Clone)" || gameObject.name == "gelo (2)")
        {
            y += speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            if (transform.position.y > 10)
            {
                Destroy(gameObject);
            }
        }
    }
}
