using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuJimmy : MonoBehaviour {

    public GameObject loadingText;
    public GameObject canvas;
    bool isAnyKeyPressed = false;

    private void Update()
    {
        if (Input.anyKeyDown && !isAnyKeyPressed)
        {
            StartGame();
            isAnyKeyPressed = true;
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadAsynchronously(2));
    }

    IEnumerator LoadAsynchronously(int sceneIndex) //Tela de loading
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        canvas.SetActive(false);
        loadingText.SetActive(true);
        loadingText.transform.position = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + 3.5f, loadingText.transform.position.y);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
