using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFireBall : MonoBehaviour {

    Transform target;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, 10 * Time.deltaTime);
    }
}
