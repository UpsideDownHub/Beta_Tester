using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {

    private void Start()
    {
        Invoke("Destroy", 3);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
