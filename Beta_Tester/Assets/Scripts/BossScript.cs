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
    public GameObject wall1;
    public GameObject wall2;
    Transform playerT;
    GameObject obj;
    Vector3 mouseP;
    public float Distance;
    float loadLife = 0.01f;
    float currentLife = 15;
    float maxLife = 15;
    public static bool isFighting;
    CinemachineVirtualCamera cm;
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
        wall1.transform.position = new Vector3(wall1.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect, wall1.transform.position.y);
        wall2.transform.position = new Vector3(wall2.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect, wall2.transform.position.y);
    }

    private void Update()
    {
        attackTemp += Time.deltaTime;

        if (playerT.position.x >= 385.5f && playerT.position.x <= 386)
            cm.Follow = null;

        if (playerT.position.x >= transform.position.x && !isFighting)
        {
            text.enabled = true;
            slider.gameObject.SetActive(true);
            slider.value += loadLife;
            cm.transform.position = new Vector3(cm.transform.position.x + 0.05f, cm.transform.position.y, cm.transform.position.z);
            Time.timeScale = 0f;
            if (slider.value == 1)
            {   
                isFighting = true;
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
            wall1.SetActive(false);
            wall2.SetActive(false);
            animator.SetBool("isDead", true);
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            Time.timeScale = 0f;
        }
    }

    private void OnMouseDown()
    {
        if (attackTemp >= 3)
        {
            animator.SetBool("isPreparingAttack", true);
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
                
                animator.Play("SlimeBossAttack", 0, 1);
                CancelInvoke();
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

    void MovementAnimation()
    {
        animator.SetBool("isPreparingAttack", false);
        animator.Play("SlimeBossMovement", 0, 0);
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
