using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour {

    bool isColliding;

    private void FixedUpdate()
    {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;

        if (other.tag == "Player" && gameObject.tag == "Spikes")
        {
            print("morreu nos espinhos");
            PlayerScript3D.life--;
            this.GetComponent<Triggers>().enabled = false;
        }

        if (other.tag == "Player" && gameObject.name == "BigBall(Clone)")
        {
            print("morreu esmagado");
            Destroy(this.gameObject);
            PlayerScript3D.life--;
            this.GetComponent<Triggers>().enabled = false;
        }

        if (other.tag == "Player" && gameObject.tag == "Poison")
        {
            print("morreu pelo veneno");
            PlayerScript3D.life--;
            this.GetComponent<Triggers>().enabled = false;
        }
    }
}
