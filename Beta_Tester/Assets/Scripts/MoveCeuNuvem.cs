using UnityEngine;
using System.Collections;

public class MoveCeuNuvem : MonoBehaviour
{
    public Material currentMaterial;
    public float speed;
    private float offSet;

    void Update()
    {
        offSet += speed * Time.deltaTime;

        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
    }
}
