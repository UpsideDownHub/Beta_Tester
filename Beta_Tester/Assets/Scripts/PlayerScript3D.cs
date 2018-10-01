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
    private float x;
    bool moving;
    public static int life = 5;
    public static bool speedDirection = true;
    public Transform groundCheck;
    public bool grounded;
    public LayerMask whatIsGround;
    bool isColliding;

    void Start()
    {
        x = transform.position.x;
        c = sr.material.color;
        slowMotion = GameObject.Find("SlowMotionSlider").GetComponent<Slider>();
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

        if (Input.GetKeyDown(KeyCode.Z) && grounded && rb.velocity.y <= 0.1 && rb.velocity.y >= -0.1)
        {
            rb.AddForce(new Vector3(0, 21000 * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.Mouse1) && !isEnded)
        {
            Time.timeScale = 0.5f;
            slowMotion.value -= 0.01f;
            isPressing = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) || slowMotion.value == 0)
        {
            Time.timeScale = 1f;
            isPressing = false;
        }

        if (!isPressing)
            slowMotion.value += 0.02f;
        if (slowMotion.value == 0)
            isEnded = true;
        else if (slowMotion.value == 1)
            isEnded = false;

        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        if (transform.position.y <= -30)
        {
            print("morreu pela queda");
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

        //print((int)rb.velocity.x + "/" + (int)rb.velocity.y);
        animator.SetBool("jump", !grounded);
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
