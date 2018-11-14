using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleManager : MonoBehaviour
{   
    //WalkSmokeParticle
    ParticleSystem particleWalkSmoke;
    Animator playerAnimator;
    SpriteRenderer playerSpriterenderer;
    public Material material;

    private void Start()
    {   
        //WalkSmokeParticle
        particleWalkSmoke = GetComponent<ParticleSystem>();
        playerAnimator = transform.parent.GetComponent<Animator>();
        playerSpriterenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //WalkSmokeParticle
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (playerAnimator.GetBool("moving2") && !particleWalkSmoke.isPlaying)
                particleWalkSmoke.Play();
            else if (!playerAnimator.GetBool("moving2") && particleWalkSmoke.isPlaying)
                particleWalkSmoke.Stop();
        }
            
        else
        {
            if (playerAnimator.GetBool("moving") && !particleWalkSmoke.isPlaying)
                particleWalkSmoke.Play();
            else if (!playerAnimator.GetBool("moving") && particleWalkSmoke.isPlaying)
                particleWalkSmoke.Stop();
        }

        if (playerSpriterenderer.flipX)
        {
            material.mainTextureScale = new Vector2(-1, 1);
            particleWalkSmoke.transform.localPosition = new Vector2(0.55f, particleWalkSmoke.transform.localPosition.y);
            particleWalkSmoke.transform.localRotation = new Quaternion(particleWalkSmoke.transform.localRotation.x, 180, particleWalkSmoke.transform.localRotation.z, particleWalkSmoke.transform.localRotation.w);
        }
        else
        {
            material.mainTextureScale = new Vector2(1, 1);
            particleWalkSmoke.transform.localPosition = new Vector2(-0.55f, particleWalkSmoke.transform.localPosition.y);
            particleWalkSmoke.transform.localRotation = new Quaternion(particleWalkSmoke.transform.localRotation.x, 0, particleWalkSmoke.transform.localRotation.z, particleWalkSmoke.transform.localRotation.w);
        }
    }
}
