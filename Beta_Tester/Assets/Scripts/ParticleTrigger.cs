using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour {

    PlayerScript3D playerScript;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript3D>();
    }

    void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            //p.startColor = new Color32(255, 0, 0, 255);
            enter[i] = p;
            if (gameObject.name == "RaioFogoEffect")
            {
                print("morreu pelo raio");
            }
            if (gameObject.name == "fumaça" || gameObject.name == "fumaça (1)")
            {
                if (playerScript.canGetDamage)
                {
                    playerScript.GetDamage();
                    playerScript.canGetDamage = false;
                }
            }
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 255, 0, 255);
            exit[i] = p;
        }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}
