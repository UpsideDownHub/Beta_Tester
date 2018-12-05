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
    Transform colliderT;
    [SerializeField] bool isControllable = false;
    private readonly float xDistance = 4;
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
    public Transform groundCheck;
    public bool grounded;
    public LayerMask whatIsGround;
    bool isColliding;
    float damageTemp;
    float alphaSpriteTemp;
    bool isDamaged;
    public bool canGetDamage;
    bool isEvading;
    bool isMovingUp;
    Positions nextPosition;
    float tempEvade;

    public static Vector3 initialPosition;
    public static bool isInstantiated;

    float horizontalMove = 0;

    void Start()
    {
        objectsVerified = new List<GameObject>();
        c = sr.material.color;
        slowMotion = GameObject.Find("SlowMotionSlider").GetComponent<Slider>();
        damageTemp = 2;
        tempEvade = 0.4f;
        alphaSpriteTemp = -0.1f;
        colliderT = GameObject.Find("collider").GetComponent<Transform>();

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            initialPosition = transform.position;
            isInstantiated = true;
        }
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        //var y = Input.GetAxis("Vertical");

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

        if (SceneManager.GetActiveScene().buildIndex == 2)
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
        if (rb.velocity.y > 4.5)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y);

        if (Assets.Scripts.PlayerAttrs.life <= 0)
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
            Mathf.Abs(x.transform.position.x - transform.position.x) <= xDistance + 1 &&
            new Vector3(x.transform.position.x, x.transform.position.y - 1).CorrectPositions().y == transform.position.CorrectPositions().y && !objectsVerified.Contains(x)).ToList();

            leftObjects.Where(x => x.gameObject.name == "GroundTrap" && (int)x.gameObject.GetComponent<MoveObjects>().mouseP.Value.y != (int)transform.position.y).ToList().ForEach(y => leftObjects.Remove(y));


            tempEvade += Time.deltaTime;

            if (MenuBetaTester.personality == 1)
            {
                if (tempEvade >= 0.1f)
                {
                    if (leftObjects.Count > 0 || rightObjects.Count > 0)
                    {
                        if (leftObjects.Count > 0)
                            objectsVerified.Add(leftObjects.First());

                        if (rightObjects.Count > 0)
                            objectsVerified.Add(rightObjects.First());

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
                        tempEvade = 0;
                    }
                }
            }
            else if (MenuBetaTester.personality == 2)
            {
                if (tempEvade >= 0.7f)
                {
                    if (leftObjects.Count > 0 || rightObjects.Count > 0)
                    {
                        if (leftObjects.Count > 0)
                            objectsVerified.Add(leftObjects.First());

                        if (rightObjects.Count > 0)
                            objectsVerified.Add(rightObjects.First());

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
                        tempEvade = 0;
                    }
                }
            }
            else if (MenuBetaTester.personality == 3)
            {
                if (tempEvade >= 1.25f)
                {
                    if (leftObjects.Count > 0 || rightObjects.Count > 0)
                    {
                        if (leftObjects.Count > 0)
                            objectsVerified.Add(leftObjects.First());

                        if (rightObjects.Count > 0)
                            objectsVerified.Add(rightObjects.First());

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
                        tempEvade = 0;
                    }
                }
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
            if (SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().name == "levelFelipe")
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

                if (horizontalMove != 0)
                {
                    moving = true;
                    rb.velocity = new Vector3(horizontalMove, rb.velocity.y, rb.velocity.z);
                }
                else
                {
                    moving = false;
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                }
                

                if (Input.GetButtonDown("Jump") && grounded)
                {
                    if (rb.velocity.y < 4.5)
                    {
                        rb.AddForce(new Vector3(0, 300, 0));
                    }
                }
            }
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
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
                    moving = true;
                }

                if (Input.GetKeyDown(KeyCode.Z) && grounded && SceneManager.GetActiveScene().buildIndex != 2)
                {
                    if (rb.velocity.y < 4.5)
                    {
                        rb.AddForce(new Vector3(0, 300, 0));
                    }
                }
            }



            //movimentação da fase de fogo

            if (SceneManager.GetActiveScene().buildIndex == 2)
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
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            grounded = Physics.OverlapSphere(groundCheck.position, 0.2f, whatIsGround).Count() > 0;
            animator.SetBool("jump", !grounded);
            animator.SetBool("moving", moving);
        }
        else
        {
            animator.SetBool("moving2", moving);
        }
        #endregion

        #region FlipAndColliders
        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
            colliderT.localRotation = Quaternion.Euler(0, 0, -26.46f);
        }
        else if (rb.velocity.x < 0)
        {
            sr.flipX = true;
            colliderT.localRotation = Quaternion.Euler(0, 0, 26.46f);
        }

        if (rb.velocity.y > 0 || rb.velocity.y < 0)
        {
            if (sr.flipX)
                colliderT.localRotation = Quaternion.Euler(0, 0, 26.46f);
            else
                colliderT.localRotation = Quaternion.Euler(0, 0, -26.46f);
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (rb.velocity.x == 0 && rb.velocity.y == 0)
                colliderT.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            if (rb.velocity.x == 0)
                colliderT.localRotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;

        if (other.gameObject.layer == 12)
        {
            if (canGetDamage && (other.gameObject.name == "GroundTrap" && (int)other.gameObject.GetComponent<MoveObjects>().mouseP.Value.y == Mathf.RoundToInt(transform.position.y)) || other.gameObject.name != "GroundTrap")
                GetDamage();
        }

        else if (other.tag == "Portal")
        {
            isFadingToNextLevel = true;
            GameManager.isLevelCompleted = true;
            GameOver.val = GameOver.lifeBetaTesterS.value;
            SceneManager.LoadScene(7);
        }
    }

    public void GetDamage()
    {
        if (!isFadingToNextLevel && canGetDamage)
        {
            Assets.Scripts.PlayerAttrs.life--;
            Physics.IgnoreLayerCollision(0, 12, true);
            damageTemp = 0;
            isDamaged = true;
            canGetDamage = false;
            if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 7)
            {
                if (MenuBetaTester.personality == 1)
                    GameOver.val -= 0.015f;
                else if (MenuBetaTester.personality == 2)
                    GameOver.val += 0.015f;
                else if (MenuBetaTester.personality == 3)
                    GameOver.val += 0.05f;
            }
        }
    }
}
