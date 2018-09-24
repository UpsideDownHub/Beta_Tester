using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhaseCreationManager : MonoBehaviour
{

    [SerializeField] GameObject floor;
    [SerializeField] GameObject LeftStair;
    [SerializeField] GameObject RightStair;
    [SerializeField] GameObject Character;
    [SerializeField] GameObject TextError;
    [SerializeField] GameObject Canvas;
    [SerializeField] Text Text;

    List<List<int>> data = new List<List<int>>();

    void Start()
    {
        //MountPhase();

    }


    void Update()
    {

    }

    public void MountPhase()
    {

        if (string.IsNullOrEmpty(Text.text) || !System.IO.File.Exists("C:\\Desenvolvimento\\Projeto\\PhaseCreation\\PhaseCreation\\Assets\\Phases\\" + Text.text))
        {
            TextError.SetActive(true);
            return;
        }
        else
            TextError.SetActive(false);

        using (StreamReader sr = File.OpenText("C:\\Desenvolvimento\\Projeto\\PhaseCreation\\PhaseCreation\\Assets\\Phases\\" + Text.text))
        {
            string s = "";
            List<int> line;
            while ((s = sr.ReadLine()) != null)
            {
                line = new List<int>();
                var val = s.Split(',');
                line.AddRange(val.Select(x => int.Parse(x)));
                data.Add(line);
            }
        }
        if (data.Count == 0)
        {
            print("Erro ao pegar os dados");
            return;
        }

        for (int i = 0; i < data.Count; i++)
        {
            for (int j = data[i].Count - 1; j > 0; j--)
            {
                if (data[i][j] == 0) continue;
                float _v = data[i][j] == 3 || data[i][j] == 4 ? -0.5f : 0;
                Instantiate(GetObject(data[i][j]), new Vector3(i, Mathf.Abs(j - 10 - _v), 0), GetObject(data[i][j]).transform.rotation);
            }
        }

        Canvas.SetActive(false);
    }

    GameObject GetObject(int value)
    {
        switch (value)
        {
            case 1:
                return floor;
            case 2:
                return Character;
            case 3:
                return LeftStair;
            case 4:
                return RightStair;
            default:
                return floor;
        }
    }

}
