using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAnimation : MonoBehaviour {

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        if (gameObject.name == "lava__0 " + "(1)")
            animator.Play("Lava", -1, 0.5f);
        else if (gameObject.name == "lava__0 " + "(2)")
            animator.Play("Lava", -1, 0.15f);
        else if (gameObject.name == "lava__0 " + "(3)")
            animator.Play("Lava", -1, 0.4f);
        else if (gameObject.name == "lava__0 " + "(4)")
            animator.Play("Lava", -1, 0.7f);
        else if (gameObject.name == "lava__0 " + "(5)")
            animator.Play("Lava", -1, 0.25f);
        else if (gameObject.name == "lava__0 " + "(6)")
            animator.Play("Lava", -1, 0.5f);
        else if (gameObject.name == "lava__0 " + "(7)")
            animator.Play("Lava", -1, 0.8f);
        else if (gameObject.name == "lava__0 " + "(8)")
            animator.Play("Lava", -1, 0.3f);
        else if (gameObject.name == "lava__0 " + "(9)")
            animator.Play("Lava", -1, 0.65f);
    }
}
