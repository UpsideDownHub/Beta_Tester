using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    ParticleSystem p;
    Animator animator;

    private void Start()
    {
        p = GetComponent<ParticleSystem>();
        animator = transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetBool("moving2") && !p.isPlaying)
            p.Play();
        else if (!animator.GetBool("moving2") && p.isPlaying)
            p.Stop();
    }
}
