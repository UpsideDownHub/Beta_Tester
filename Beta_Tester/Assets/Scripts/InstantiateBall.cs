using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBall : MonoBehaviour {

    public GameObject prefab;

    private void Start()
    {
        InvokeRepeating("Ball", 4, 6);
    }

    void Ball()
    {
        Instantiate(prefab, new Vector3(44.83f, 20.2f, 0), Quaternion.identity);
    }
}
