using UnityEngine;
using System.Collections;

public class MoveCeuNuvem : MonoBehaviour
{
    public Material currentMaterial;
    public float speed;
    private float offSet;

    Vector3 previousPosition;

    private void Start()
    {
        if (gameObject.name == "ceu_1")
        {
            transform.localScale = new Vector3(Camera.main.orthographicSize * Camera.main.aspect * 2, transform.localScale.y, transform.localScale.z);
        }

        offSet = 0;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(0, 0));
    }

    void Update()
    {
        if (gameObject.name == "ceu_1")
        {
            if (previousPosition.x != Camera.main.transform.position.x)
                offSet += speed * Time.deltaTime;
        }
        else if (gameObject.name == "ceuNuvem")
            offSet += speed * Time.deltaTime;

        previousPosition = Camera.main.transform.position;

        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
    }
}
