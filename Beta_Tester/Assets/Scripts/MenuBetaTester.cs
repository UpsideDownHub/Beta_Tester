using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBetaTester : MonoBehaviour {

    public GameObject canvas1;
    public GameObject canvas2;
    public Button button;
    bool isAnyKeyPressed = false;

    private void Update()
    {
        if (Input.anyKeyDown && !isAnyKeyPressed)
        {
            canvas1.SetActive(false);
            canvas2.SetActive(true);
            button.Select();
            isAnyKeyPressed = true;
        }
    }

    public void Character(int characterIndex)
    {
        if (characterIndex == 1)
        {
            //configurações do personagem 1
        }
        else if (characterIndex == 2)
        {
            //configurações do personagem 2
        }
        else if (characterIndex == 3)
        {
            //configurações do personagem 3
        }

        SceneManager.LoadScene(1);
    }
}
