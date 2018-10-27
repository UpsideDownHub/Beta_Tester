using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    private void Start()
    {
        if (gameObject.tag == "Poison")
            Destroy(gameObject, 3);
        else if (gameObject.name == "BOOM(Clone)")
            Destroy(gameObject, 2f);
    }

    void GroundLavaAnim()
    {
        var animator = GetComponent<Animator>();
        animator.SetBool("EndAnimation", true);
    }
}
