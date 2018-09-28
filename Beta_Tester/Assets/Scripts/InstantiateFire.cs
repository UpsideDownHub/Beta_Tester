using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFire : MonoBehaviour {

    public GameObject prefab;

    private void Start()
    {
        InvokeRepeating("FireBall", 4, 2.5f);
    }

    void FireBall()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
