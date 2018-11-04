using System.Linq;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Shared;

public class PlayerScript3D : MonoBehaviour
{

    public Rigidbody rb;
    public Animator animator;
    public SpriteRenderer sr;
    [SerializeField] bool isControllable = false;
    private readonly float xDistance = 3;
    List<GameObject> objectsVerified;

    Slider slowMotion;
    bool isPressing;
    bool isEnded;
    bool isFadingToNextLevel;
    Color c;
    public float speed;
    float x = 5;
    float y;
    bool moving;
    public static int life = 5;
    public static bool speedDirection = true;
    public Transform groundCheck;
    public bool grounded;
    public LayerMask whatIsGround;
    bool isColliding;
    CinemachineVirtualCamera cm;
    float damageTemp;
    float alphaSpriteTemp;
    bool isDamaged;
    bool canGetDamage;
    bool isEvading;
    bool isMovingUp;
    Positions nextPosition;

    void Start()
    {
        objectsVerified = new List<GameObject>();
        c = sr.material.color;
        slowMotion = GameObject.Find("SlowMotionSlider").GetComponent<Slider>();
        cm = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        damageTemp = 2;
        alphaSpriteTemp = -0.1f;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            animator.SetBool("moving", false);
        }

        #region SlowMotionConfig
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (!isEnded)
            {
                Time.timeScale = 0.5f;
                slowMotion.value -= 0.01f;
                isPressing = true;
            }
            else
            {
                Time.timeScale = 1f;
                isPressing = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Time.timeScale = 1f;
            isPressing = false;
        }

        if (!isPressing)
        {
            slowMotion.value += 0.02f;
            if (slowMotion.value == 1)
                isEnded = false;
        }
        else
        {
            if (slowMotion.value == 0)
                isEnded = true;
        }
        #endregion

        if (SceneManager.GetActiveScene().buildIndex == 3)
            sr.sortingOrder = Mathf.RoundToInt(transform.position.y * 10f) * -1;

        #region DamageAndInvulnerability
        damageTemp += Time.deltaTime;

        if (damageTemp >= 2)
        {
            canGetDamage = true;
            Physics.IgnoreLayerCollision(0, 12, false);
            c.a = 1;
            sr.material.color = c;
            isDamaged = false;
        }
        else
        {
            canGetDamage = false;
        }

        if (isDamaged)
        {
            alphaSpriteTemp += Time.deltaTime;
            if (alphaSpriteTemp >= 0)
            {
                c.a = 1;
                sr.material.color = c;
                if (alphaSpriteTemp >= 0.1f)
                    alphaSpriteTemp = -0.1f;
            }
            else
            {
                c.a = 0;
                sr.material.color = c;
            }
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if (life <= 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

        isColliding = false;

        #region Movement
        #region [Not Controllable]
        if (isControllable)
        {
            moving = true;

            var leftObjects = GameObject.FindGameObjectsWithTag("FireBall").ToList();
            leftObjects.AddRange(GameObject.FindGameObjectsWithTag("Trap").Where(x => x.GetComponent<MoveObjects>() != null && x.GetComponent<MoveObjects>().mouseP.HasValue).ToList());
            var rightObjects = GameObject.FindGameObjectsWithTag("Trap").ToList();

            rightObjects.Where(x => x.gameObject.name == "GroundTrap" && x.GetComponent<Animator>() != null && !x.GetComponent<Animator>().enabled).ToList().ForEach(y => rightObjects.Remove(y));

            leftObjects = leftObjects.Where(x => x.transform.position.x < transform.position.x &&
            Mathf.Abs(x.transform.position.x - transform.position.x) <= xDistance &&
            x.transform.position.CorrectPositions().y == transform.position.CorrectPositions().y && !objectsVerified.Contains(x)).ToList();

            rightObjects.ForEach(x => print(x.transform.position.CorrectPositions().y));

            rightObjects = rightObjects.Where(x => x.transform.position.x > transform.position.x &&
            Mathf.Abs(x.transform.position.x - transform.position.x) <= xDistance && // xDistance + 1
            new Vector3(x.transform.position.x, x.transform.position.y - 1).CorrectPositions().y == transform.position.CorrectPositions().y && !objectsVerified.Contains(x)).ToList(); 

            if (leftObjects.Count > 0 || rightObjects.Count > 0)
            {
                if(leftObjects.Count > 0)
                    objectsVerified.Add(leftObjects.First());

                if (rightObjects.Count > 0)
                    objectsVerified.Add(rightObjects.First());

            //}


            //if (leftObjects.Count > 0)
            //{
            //    objectsVerified.Add(leftObjects.First());
                var position = (Positions)transform.position.CorrectPositions().y;
                var newPosition = position.SearchNewPath();
                nextPosition = newPosition;

                if (newPosition == Positions.Top || (newPosition == Positions.Middle && Mathf.RoundToInt(transform.position.y) < (int)Positions.Middle))
                {
                    y = 5;
                    isMovingUp = true;
                }
                else
                {
                    y = -5;
                    isMovingUp = false;
                }

                isEvading = true;
            }

            if (isMovingUp)
            {
                if (transform.position.y >= (int)nextPosition)
                    isEvading = false;
            }
            else
            {
                if (transform.position.y <= (int)nextPosition)
                    isEvading = false;
            }

            if (isEvading)
                rb.velocity = new Vector3(x, y, rb.velocity.z);
            else
                rb.velocity = new Vector3(x, 0, rb.velocity.z);
        }

        #endregion

        #region [Controllable]
        else
        {
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                moving = false;
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
                moving = true;
                sr.flipX = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
                moving = true;
                sr.flipX = false;
            }

            if (Input.GetKeyDown(KeyCode.Z) && grounded && rb.velocity.y <= 0.1 && rb.velocity.y >= -0.1 && transform.position.x <= 377 && SceneManager.GetActiveScene().buildIndex != 3) //tirar velocity
            {
                rb.AddForce(new Vector3(0, 300, 0));
            }

            //movimentação da fase de fogo

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    rb.velocity = new Vector3(rb.velocity.x, speed, rb.velocity.z);
                    moving = true;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    rb.velocity = new Vector3(rb.velocity.x, -speed, rb.velocity.z);
                    moving = true;
                }
            }
        }
        #endregion
        #endregion

        #region Animation
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
            animator.SetBool("jump", !grounded);
            animator.SetBool("moving", moving);
        }
        else
        {
            animator.SetBool("moving2", moving);
        }
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;

        if (other.gameObject.layer == 12)
        {
            if (canGetDamage)
            {
                GetDamage();
            }
            //gameOverBT.slider.value += 0.05f;
        }

        else if (other.tag == "Portal")
        {
            isFadingToNextLevel = true;
            cm.Follow = null;
            rb.AddForce(new Vector3(0, 150, 0));
            GameManager.isLevelCompleted = true;
        }
    }

    void GetDamage()
    {
        if (!isFadingToNextLevel)
        {
            life--;
            Physics.IgnoreLayerCollision(0, 12, true);
            damageTemp = 0;
            isDamaged = true;
        }
    }
}
