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
        if (rb.tag == "Poison" && gameObject.name == "Poison(Clone)")
        {
            rb.AddForce(new Vector3(-300, 400, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison2(Clone)")
        {
            rb.AddForce(new Vector3(-300, 0, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison3(Clone)")
        {
            rb.AddForce(new Vector3(0, 25000 * Time.deltaTime, -20000 * Time.deltaTime));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison4(Clone)")
        {
            rb.AddForce(new Vector3(-20000 * Time.deltaTime, 25000 * Time.deltaTime, -20000 * Time.deltaTime));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison5(Clone)")
        {
            rb.AddForce(new Vector3(20000 * Time.deltaTime, 25000 * Time.deltaTime, -20000 * Time.deltaTime));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison6(Clone)")
        {
            rb.AddForce(new Vector3(0, 400, 0));
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(spinx, spiny, spinz);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name == "BigBall(Clone)" && collision.collider.tag == "Ground")
        {
            spinz = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Ground" || other.tag == "Player") && gameObject.tag == "Poison")
        {
            Instantiate(prefab, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
