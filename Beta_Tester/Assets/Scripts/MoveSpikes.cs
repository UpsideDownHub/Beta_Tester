using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikes : MonoBehaviour {

    public float speed;
    float y;

    private void Start()
    {
        y = transform.position.y;
    }

    private void Update()
    {
        y += speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }
}
