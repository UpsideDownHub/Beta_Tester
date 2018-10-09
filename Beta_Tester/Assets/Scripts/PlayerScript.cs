using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public SpriteRenderer sr;
    Slider slowMotion;
    bool isPressing;
    bool isEnded;
    Color c;
    public float speed;
    float x;
    float y;
    bool moving;
    public static int life = 5;
    public static bool speedDirection = true;
    bool isColliding;

    private void Start()
    {
        c = sr.material.color;
        slowMotion = GameObject.Find("SlowMotionSlider").GetComponent<Slider>();
        x = transform.position.x;
        y = transform.position.y;
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
    }

    private void FixedUpdate()
    {
        if (life <= 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

        #region Movement
        if (speed == 0)
        {
            moving = false;
        }
        else
        {
            if (speedDirection)
            {
                x += speed * Time.deltaTime;
                moving = true;
                sr.flipX = false;
            }
            else
            {
                x -= speed * Time.deltaTime;
                moving = true;
                sr.flipX = true;
            }
        }
        //if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        //{
        //    moving = false;
        //    rb.velocity = new Vector3(0, 0, 0);
        //}

        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rb.velocity = new Vector3(-speed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
        //    moving = true;
        //    sr.flipX = true;
        //}

        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rb.velocity = new Vector3(speed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
        //    moving = true;
        //    sr.flipX = false;
        //}
        transform.position = new Vector3(x, y, transform.position.z);
        #endregion
        
        animator.SetBool("jump", false);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
