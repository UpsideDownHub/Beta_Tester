using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
    public GameObject prefab;
    Rigidbody rb;
    public float spinx;
    public float spiny;
    public float spinz;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb.tag == "Poison")
        {
            rb.AddForce(new Vector3(-300, 400, 0));
            if (gameObject.name == "PoisonE")
                rb.velocity = new Vector3(0, 0, 0);
        }
        if (rb.tag == "Poison" && gameObject.name == "Poison2(Clone)")
        {
            rb.AddForce(new Vector3(-300, 0, 0));
        }
        if (rb.tag == "Poison" && gameObject.name == "Poison(Clone)")
        {
            rb.AddForce(new Vector3(-300, 400, 0));
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(spinx, spiny, spinz);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.tag == "Ground" || collision.collider.tag == "Player") && gameObject.tag == "Poison")
        {
            Instantiate(prefab, transform.position + new Vector3(-2,0,0), Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (gameObject.name == "BigBall(Clone)" && collision.collider.tag == "Ground")
        {
            spinz = 3;
        }
    }
}
