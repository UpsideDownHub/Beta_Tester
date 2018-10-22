using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript3D : MonoBehaviour {

    public Rigidbody rb;
    public Animator animator;
    public SpriteRenderer sr;
    Slider slowMotion;
    bool isPressing;
    bool isEnded;
    Color c;
    public float speed;
    bool moving;
    public static int life;
    public static bool speedDirection = true;
    public Transform groundCheck;
    public bool grounded;
    public LayerMask whatIsGround;
    bool isColliding;
    CinemachineVirtualCamera cm;

    void Start()
    {
        life = 5;
        c = sr.material.color;
        slowMotion = GameObject.Find("SlowMotionSlider").GetComponent<Slider>();
        cm = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
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
    }

    private void FixedUpdate()
    {
        if (life <= 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

        #region Movement
        //if (speed == 0)
        //{
        //    moving = false;
        //}
        //else
        //{
        //    if (speedDirection)
        //    {
        //        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        //        moving = true;
        //        sr.flipX = false;
        //    }
        //    else
        //    {
        //        rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        //        moving = true;
        //        sr.flipX = true;
        //    }
        //}
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
        #endregion

        if (transform.position.y <= -30)
        {
            print("morreu pela queda");
        }

        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
            animator.SetBool("jump", !grounded);
        }

        animator.SetBool("moving", moving);

        isColliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;

        if (other.gameObject.layer == 12)
        {
            StartCoroutine("DamageAndInvulnerable");
            //gameOverBT.slider.value += 0.05f;
        }

        else if (other.tag == "Portal")
        {
            cm.Follow = null;
            rb.AddForce(new Vector3(0, 150, 0));
            GameManager.isLevelCompleted = true;
        }
    }

    public IEnumerator DamageAndInvulnerable()
    {
        life--;
        Physics.IgnoreLayerCollision(0, 12, true);
        c.a = 0.5f;
        sr.material.color = c;
        yield return new WaitForSeconds(2f);
        Physics.IgnoreLayerCollision(0, 12, false);
        c.a = 1f;
        sr.material.color = c;
    }
}
