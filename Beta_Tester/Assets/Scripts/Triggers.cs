using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour {

    //public GameOverBetaTester gameOverBT;
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
            //gameOverBT.slider.value += 0.05f;
            this.GetComponent<Triggers>().enabled = false;
        }

        else if (other.tag == "Player" && gameObject.name == "BigBall(Clone)")
        {
            print("morreu esmagado");
            Destroy(this.gameObject);
            PlayerScript3D.life--;
            //gameOverBT.slider.value += 0.05f;
            this.GetComponent<Triggers>().enabled = false;
        }

        else if (other.tag == "Player" && gameObject.tag == "Poison")
        {
            print("morreu pelo veneno");
            PlayerScript3D.life--;
            //gameOverBT.slider.value += 0.05f;
            this.GetComponent<Triggers>().enabled = false;
        }

        else if (other.tag == "Player" && gameObject.tag == "Portal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
