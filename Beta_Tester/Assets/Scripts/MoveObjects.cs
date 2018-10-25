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

    //GroundTrap
    public bool isActivated;
    bool isPreparedToLaunch;
    bool isLaunched;
    Vector3 mouseP;
    float temp;
    float temp2;

    private void Start()
    {   //FireBall
        target = GameObject.Find("Player").GetComponent<Transform>();
        if (target == null) Destroy(this.gameObject);
        lastPosition = target.position;
        x = lastPosition.x;

        //Spikes
        y = transform.position.y;

        //GroundTrap
        temp = 0;
        temp2 = 0;
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

        //GroundTrap
        else if (gameObject.tag == "Trap" && isActivated)
        {
            var spin = gameObject.GetComponent<Spin>();
            if (temp == 0)
                spin.spinz = -1;
            else if (temp >= 2)
                spin.spinz = 0;
            temp += Time.deltaTime;

            if (!isPreparedToLaunch)
            {
                y += speed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                if (transform.position.y >= 10)
                    isPreparedToLaunch = true;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    gameObject.layer = 12;
                    mouseP = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                    isLaunched = true;
                }
            }

            if (isLaunched)
                transform.position = Vector3.MoveTowards(transform.position, mouseP, 10 * Time.deltaTime);

            if (mouseP == transform.position)
            {
                temp2 += Time.deltaTime;
                var animator = gameObject.GetComponent<Animator>();
                animator.SetBool("TouchGround", true);
                if (temp2 >= 0.4f)
                    gameObject.SetActive(false);
            }
        }
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);        
    }

    private void OnBecameInvisible()
    {
        if (gameObject.name != "GroundTrap")
            gameObject.SetActive(false);

        if (gameObject.name == "FireBall(Clone)")
            Destroy(gameObject);
    }
}
