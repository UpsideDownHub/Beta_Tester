using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Spikes")
        {
            print("morreu nos espinhos");
        }

        if (other.tag == "Player" && gameObject.name == "BigBall(Clone)")
        {
            print("morreu esmagado");
            Destroy(this.gameObject);
        }

        if (other.tag == "Player" && gameObject.tag == "Poison")
        {
            print("morreu pelo veneno");
        }
    }
}
