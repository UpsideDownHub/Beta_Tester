using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscenes : MonoBehaviour {

    public GameObject level;
    public GameObject canvasCutscene;
    public Text narrator;
    public float letterPause = 0.2f;
    //Coroutine textCoroutine;

    private void Update()
    {
        
    }

    public void StartLevel()
    {
        level.SetActive(true);
        canvasCutscene.SetActive(false);
    }

    //public void Interaction()
    //{
    //    textCoroutine = StartCoroutine(TypeText());
    //    startInteraction.Invoke();
    //}

    /*IEnumerator TypeText()
    {
        foreach (char letter in ("Bem vindo Harry!").ToCharArray())
        {
            narrator.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }*/
}
