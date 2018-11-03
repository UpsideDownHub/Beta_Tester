using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour {

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;
    public Slider slider;
    public Text text;
    public GameObject walls;
    Transform playerT;
    GameObject obj;
    Vector3 mouseP;
    public float Distance;
    float loadLife = 0.01f;
    float currentLife = 15;
    float maxLife = 15;
    public static bool isFighting;
    CinemachineVirtualCamera cm;
    float x;
    Animator animator;
    bool exceededTimeLimit = false;
    float attackTemp;
    bool canAttack;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerT = GameObject.Find("Player").GetComponent<Transform>();
        cm = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        attackTemp = 0;
    }

    private void Update()
    {
        attackTemp += Time.deltaTime;

        if (playerT.position.x >= 385.5f && !isFighting)
        {
            text.enabled = true;
            slider.gameObject.SetActive(true);
            slider.value += loadLife;
            cm.Follow = null;
            x = cm.transform.position.x;
            x += 0.05f;
            cm.transform.position = new Vector3(x, cm.transform.position.y, cm.transform.position.z);
            Time.timeScale = 0f;
            if (slider.value == 1)
            {   
                isFighting = true;
                walls.SetActive(true);
                animator.enabled = true;
                Time.timeScale = 1f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (obj != null)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, mouseP, Distance);
        }
        if (slider.value == 0)
        {
            walls.SetActive(false);
            CancelInvoke();
            animator.SetBool("isDead", true);
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            Time.timeScale = 0f;
        }
    }

    private void OnMouseDown()
    {
        if (attackTemp >= 3)
        {
            animator.SetBool("one", true);
            animator.SetBool("two", false);
            animator.SetBool("three", false);
            animator.SetBool("four", false);
            Invoke("AttackAnimation2", 1);
            Invoke("AttackAnimation3", 2);
            Invoke("TimeLimit", 3);
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    private void OnMouseUp()
    {
        if (!exceededTimeLimit && canAttack)
        {
            mouseP = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            if (mouseP.y >= -48f && mouseP.y <= -38f)
            {
                if (mouseP.x < transform.position.x && mouseP.x >= transform.position.x - 8.1f) //Esquerda
                    obj = Instantiate(prefab1, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                else if (mouseP.x > transform.position.x && mouseP.x <= transform.position.x + 8.1f) //Direita
                    obj = Instantiate(prefab1, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                else if (mouseP.x < transform.position.x - 8.1f && mouseP.x >= transform.position.x - 12.9f) //Esquerda
                    obj = Instantiate(prefab2, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                else if (mouseP.x > transform.position.x + 8.1f && mouseP.x <= transform.position.x + 12.9f) //Direita
                    obj = Instantiate(prefab4, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                else if (mouseP.x < transform.position.x - 12.9) //Esquerda
                    obj = Instantiate(prefab3, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                else if (mouseP.x > transform.position.x + 12.9) //Direita
                    obj = Instantiate(prefab5, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                //talvez mudar o spriterenderer ao invés da animação
                CancelInvoke();
                animator.SetBool("one", false);
                animator.SetBool("two", false);
                animator.SetBool("three", false);
                animator.SetBool("four", true);
                Invoke("MovementAnimation", 1.5f);
                currentLife -= 1;
                slider.value = CalculateLife();
                attackTemp = 0;
            }
            else
            {
                CancelInvoke();
                Invoke("MovementAnimation", 0);
            }
        }
        else
        {
            exceededTimeLimit = false;
            return;
        }
    }

    float CalculateLife()
    {
        return currentLife / maxLife;
    }

    void AttackAnimation2()
    {
        animator.SetBool("one", false);
        animator.SetBool("two", true);
        animator.SetBool("three", false);
        animator.SetBool("four", false);
    }

    void AttackAnimation3()
    {
        animator.SetBool("one", false);
        animator.SetBool("two", false);
        animator.SetBool("three", true);
        animator.SetBool("four", false);
    }

    void MovementAnimation()
    {
        animator.SetBool("one", false);
        animator.SetBool("two", false);
        animator.SetBool("three", false);
        animator.SetBool("four", false);
        animator.Play(0, -1, 0);
    }

    void TimeLimit()
    {
        OnMouseUp();
        exceededTimeLimit = true;
    }

    void Dead()
    {
        slider.gameObject.SetActive(false);
        text.enabled = false;
        Time.timeScale = 1f;
        cm.Follow = playerT;
        animator.updateMode = AnimatorUpdateMode.Normal;
        gameObject.SetActive(false);
    }
}
