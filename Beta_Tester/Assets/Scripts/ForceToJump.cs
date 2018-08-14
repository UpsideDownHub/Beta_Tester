using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ForceToJump : MonoBehaviour
{

    //public UnityEvent ForceToJumpTrigger;
    public Vector2 impulseRange;
    public float impulseHorizontal;
    public LayerMask groundMask;
    bool alreadyIn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>().IsTouchingLayers(groundMask) && !alreadyIn)
        {
            var f = Random.Range(impulseRange.x, impulseRange.y);
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * f, ForceMode2D.Impulse);
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.right * impulseHorizontal);
            alreadyIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        alreadyIn = false;
    }
}
