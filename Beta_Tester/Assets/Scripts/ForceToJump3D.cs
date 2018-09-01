using UnityEngine;
using System.Collections;

public class ForceToJump3D : MonoBehaviour
{
    public Vector2 impulseRange;
    public float impulseHorizontal;
    bool alreadyIn = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && !alreadyIn)
        {
            var f = Random.Range(impulseRange.x, impulseRange.y);
            collision.GetComponent<Rigidbody>().AddForce(Vector2.up * f, ForceMode.Impulse);
            collision.GetComponent<Rigidbody>().AddForce(Vector2.right * impulseHorizontal);
            alreadyIn = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        alreadyIn = false;
    }
}
