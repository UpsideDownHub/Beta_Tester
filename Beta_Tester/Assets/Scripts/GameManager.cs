using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform circleT;
    float x;
    float y;
    public static bool isLevelCompleted;
    bool isLevelInBeginning;

    private void Start()
    {
        x = circleT.localScale.x;
        y = circleT.localScale.x;

        isLevelInBeginning = true;
    }

    private void Update()
    {
        if (isLevelInBeginning)
        {
            if (circleT.localScale.x >= 0 && circleT.localScale.x <= 70)
            {
                Time.timeScale = 0f;
                x += 0.8f;
                y += 0.8f;
                circleT.localScale = new Vector3(x, y, circleT.localScale.z);
            }
            else
            {
                Time.timeScale = 1f;
                circleT.localScale = new Vector3(70, 70, circleT.localScale.z);
                isLevelInBeginning = false;
            }
        }

        if (isLevelCompleted)
        {
            if (circleT.localScale.x > 0)
            {
                x -= 24 * Time.deltaTime;
                y -= 24 * Time.deltaTime;
                circleT.localScale = new Vector3(x, y, circleT.localScale.z);
            }
            else
            {
                circleT.localScale = new Vector3(0, 0, 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //Invoke("LoadScene", 1.5f);
                isLevelCompleted = false;
            }
        }
    }

    //void LoadScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}
