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
    
    //MouseParticle
    ParticleSystem mouseParticle;

    //PoisonParticle
    public ParticleSystem poisonParticle;

    private void Start()
    {
        //WalkSmokeParticle
        if (gameObject.name == "walksmoke" || gameObject.name == "walksmoke(Clone)")
        {
            particleWalkSmoke = GetComponent<ParticleSystem>();
            playerAnimator = transform.parent.GetComponent<Animator>();
            playerSpriterenderer = transform.parent.GetComponent<SpriteRenderer>();
        }

        //MouseParticle
        if (gameObject.name == "pedra puf")
            mouseParticle = GetComponent<ParticleSystem>();

        //PoisonParticle
        if (gameObject.name == "poison")
        {
            poisonParticle = GetComponent<ParticleSystem>();
            poisonParticle.Stop();
        }
    }

    private void Update()
    {
        //WalkSmokeParticle
        if (gameObject.name == "walksmoke" || gameObject.name == "walksmoke(Clone)")
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (playerAnimator.GetBool("moving2") && !particleWalkSmoke.isPlaying)
                    particleWalkSmoke.Play();
                else if (!playerAnimator.GetBool("moving2") && particleWalkSmoke.isPlaying)
                    particleWalkSmoke.Stop();
            }

            else
            {
                if (playerAnimator.GetBool("jump"))
                    particleWalkSmoke.Stop();

                if (playerAnimator.GetBool("moving") && !playerAnimator.GetBool("jump") && !particleWalkSmoke.isPlaying)
                    particleWalkSmoke.Play();
                else if (!playerAnimator.GetBool("moving") && !playerAnimator.GetBool("jump") && particleWalkSmoke.isPlaying)
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

        //MouseParticle
        if (gameObject.name == "pedra puf")
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            if (SelectableEffects.isCursorInsideObject)
            {
                mouseParticle.Play();
            }
            else
            {
                mouseParticle.Stop();
            }
        }

        //PoisonParticle
        if (gameObject.name == "poison")
        {
            if (poisonParticle.isPlaying)
            {
                Invoke("StopParticle", 2f);
            }
        }
    }

    void StopParticle()
    {
        if (gameObject.name == "poison")
        {
            poisonParticle.Stop();
        }
    }
}
