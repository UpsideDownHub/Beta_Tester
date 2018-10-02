using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour {

    public Slider slider;
    public Text text;
    Transform playerT;
    GameObject obj;
    Vector3 mouseP;
    public float Distance;
    float currentLife = 0.1f;
    float maxLife = 10;
    public static bool isFighting;
    CinemachineVirtualCamera cm;
    float x;

    private void Start()
    {
        playerT = GameObject.Find("Player").GetComponent<Transform>();
        cm = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        x = cm.transform.position.x;
    }

    private void Update()
    {
        if (playerT.position.x >= 385.5f && !isFighting)
        {
            text.enabled = true;
            slider.gameObject.SetActive(true);
            slider.value += CalculateLife();
            cm.Follow = null;
            x += 0.05f;
            cm.transform.position = new Vector3(x, cm.transform.position.y, cm.transform.position.z);
            Time.timeScale = 0f;
            if (slider.value == 1)
            {
                isFighting = true;
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
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseUp()
    {
        mouseP = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        if (mouseP.y >= -48f && mouseP.y <= -38f)
        {
            if (mouseP.x < transform.position.x && mouseP.x >= transform.position.x - 8.1f) //Esquerda
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/PoisonBoss"), transform.position, Quaternion.identity);
            else if (mouseP.x > transform.position.x && mouseP.x <= transform.position.x + 8.1f) //Direita
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/PoisonBoss"), transform.position, Quaternion.identity);
            else if (mouseP.x < transform.position.x - 8.1f && mouseP.x >= transform.position.x - 12.9f) //Esquerda
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/PoisonBoss1"), transform.position, Quaternion.identity);
            else if (mouseP.x > transform.position.x + 8.1f && mouseP.x <= transform.position.x + 12.9f) //Direita
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/PoisonBoss3"), transform.position, Quaternion.identity);
            else if (mouseP.x < transform.position.x - 12.9) //Esquerda
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/PoisonBoss2"), transform.position, Quaternion.identity);
            else if (mouseP.x > transform.position.x + 12.9) //Direita
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/PoisonBoss4"), transform.position, Quaternion.identity);
        }
    }

    float CalculateLife()
    {
        return currentLife / maxLife;
    }
}
