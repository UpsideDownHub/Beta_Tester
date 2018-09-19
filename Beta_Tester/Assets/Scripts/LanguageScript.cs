using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageScript : MonoBehaviour {

    public Text pressAnyKey;
    public Text chooseCharacter;


    private void Start()
    {
        if (PlayerPrefs.GetInt("Language") == 0)
        {
            pressAnyKey.text = "PRESS ANY KEY TO START";
            chooseCharacter.text = "CHOOSE YOUR CHARACTER";
        }
        else
        {
            pressAnyKey.text = "APERTE QUALQUER TECLA PARA COMEÇAR";
            chooseCharacter.text = "ESCOLHA SEU PERSONAGEM";
        }
    }
}
