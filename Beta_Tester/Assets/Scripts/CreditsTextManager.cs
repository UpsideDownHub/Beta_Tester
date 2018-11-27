using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsTextManager : MonoBehaviour
{
    Text creditsText;
    int i = 0;

    private void Start()
    {
        creditsText = GetComponent<Text>();
    }

    public void CreditsTextChanges()
    {
        if (i == 0)
            creditsText.text = "Cleber Araujo - Designer";
        else if (i == 1)
            creditsText.text = "Max da Mata Novo Guterres - Programmer";
        else if (i == 2)
            creditsText.text = "Lucas Francisco - Programmer";
        else
            creditsText.text = "";
        i++;
    }
}
