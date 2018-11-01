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

        if (rb == null) return;

        if (rb.tag == "Poison" && gameObject.name == "Poison(Clone)")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(-300, 400, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison2(Clone)")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(-300, 0, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison3(Clone)")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(0, 350, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison4(Clone)")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(-300 , 350, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison5(Clone)")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(300, 350, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison6(Clone)")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(0, 400, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "Poison7(Clone)")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(300, 350, 0)); //150x
        }
        else if (rb.tag == "Poison" && gameObject.name == "PoisonBoss(Clone)") //Curto alcance ambos lados
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(0, 350, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "PoisonBoss1(Clone)") //Médio alcance pra esquerda
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(-150, 350, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "PoisonBoss2(Clone)") //Longo alcance pra esquerda
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(-270, 350, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "PoisonBoss3(Clone)") //Médio alcance pra direita
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(150, 370, 0));
        }
        else if (rb.tag == "Poison" && gameObject.name == "PoisonBoss4(Clone)") //Longo alcance pra direita
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(270, 350, 0));
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
