using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSpikes : MonoBehaviour {

    public GameObject prefab;

    private void Start()
    {
        InvokeRepeating("SpikeInstance", 0, 3);
    }

    void SpikeInstance()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
