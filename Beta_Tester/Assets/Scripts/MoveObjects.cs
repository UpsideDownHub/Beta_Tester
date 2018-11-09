using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Shared;

public class MoveObjects : MonoBehaviour {

    public float speed;
    //FireBall
    public GameObject prefabBOOM;
    Transform target;
    Vector3 lastPosition;
    float x;
    bool isInLastPosition = false;
   
    //Spikes
    float y;

    //GroundTrap
    public GameObject prefabPedraPuf;
    public bool isActivated;
    bool isDestroying = true;
    bool isPreparedToLaunch;
    bool isLaunched;
    bool isClicked;
    public bool Activated;
    [HideInInspector] public Vector3? mouseP = null;
    float temp;
    float temp2;
    SpriteRenderer sr;
    Spin spin;
    Animator animator;
    float cameraPosition;
    Transform parent;

    private void Start()
    {   //FireBall
        if (gameObject.tag == "FireBall")
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
            if (target == null) Destroy(this.gameObject);
            lastPosition = target.position;
            lastPosition = lastPosition.CorrectPositions();
            transform.position = new Vector3(transform.position.x, lastPosition.y, transform.position.z);
            x = lastPosition.x;
        }
        //Spikes
        if (gameObject.name == "Spikes(Clone)" || gameObject.name == "gelo (2)" || gameObject.tag == "Trap")
            y = transform.position.y;

        //GroundTrap
        if (gameObject.tag == "Trap")
        {
            sr = GetComponent<SpriteRenderer>();
            spin = GetComponent<Spin>();
            animator = GetComponent<Animator>();
            temp = 0;
            temp2 = 0;
            cameraPosition = Camera.main.orthographicSize * Camera.main.aspect;
            parent = transform.parent;
        }
    }

    private void FixedUpdate()
    {   //FireBall
        if (gameObject.tag == "FireBall")
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
            if (temp == 0)
                spin.spinz = -1;
            else if (temp >= 2)
                spin.spinz = 0;
            temp += Time.deltaTime;

            if (transform.position.x + cameraPosition - 3 <= Camera.main.transform.position.x)
            {
                transform.SetParent(Camera.main.transform);
            }

            if (!isPreparedToLaunch)
            {
                y += speed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                if (transform.position.y >= 10)
                    isPreparedToLaunch = true;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && !isClicked)
                {
                    gameObject.layer = 12;
                    mouseP = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0).CorrectPositions();
                    isLaunched = true;
                    isClicked = true;
                }
            }

            if (isLaunched)
            {
                transform.SetParent(parent);
                sr.sortingOrder = Mathf.RoundToInt(transform.position.y * 10f - 10) * -1;
                transform.position = Vector3.MoveTowards(transform.position, mouseP.Value, 10 * Time.deltaTime);
            }

            if (mouseP == transform.position)
            {
                if (isDestroying)
                {
                    Instantiate(prefabPedraPuf, transform.position, Quaternion.identity);
                    isDestroying = false;
                }

                temp2 += Time.deltaTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "FireBall(Clone)" && other.tag == "Player")
        {
            Instantiate(prefabBOOM, transform.position + new Vector3(1,0,0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
