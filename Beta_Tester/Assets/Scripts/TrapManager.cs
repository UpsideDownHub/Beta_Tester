using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    private void Start()
    {
        if (gameObject.tag == "Poison")
            Destroy(this.gameObject, 3);
    }

    void GroundLavaAnim()
    {
        var animator = GetComponent<Animator>();
        animator.SetBool("EndAnimation", true);
    }
}
