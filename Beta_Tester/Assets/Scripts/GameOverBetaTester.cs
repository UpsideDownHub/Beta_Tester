using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBetaTester : MonoBehaviour {

    public Slider slider;

    private void Update()
    {
        slider.value -= 0.001f;
    }
}
