using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityGoogleDrive;

public class OurPhases : MonoBehaviour
{
    [SerializeField] GameObject panelContent;
    [SerializeField] GameObject panelItem;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject floor2;
    [SerializeField] GameObject LeftStair;
    [SerializeField] GameObject RightStair;
    [SerializeField] GameObject Character;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject Camera;
    [SerializeField] Tilemap tileM;
    public static List<List<int>> data = new List<List<int>>();
    private string result = string.Empty;

    private void Start()
    {
        //SetResult();
    }

    private void SetResult()
    {
        result = "0,0,0,2,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n";

        var lines = result.Split('\n');

        List<int> line;
        foreach (var _line in lines)
        {
            var __line = _line.Replace("\r", string.Empty);
            line = new List<int>();
            var val = __line.Split(',');
            if (val.Count() < 10) continue;
            line.AddRange(val.Select(x => int.Parse(x)));
            data.Add(line);
        }
        GameObject character = null;
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = data[i].Count - 1; j >= 0; j--)
            {
                if (data[i][j] == 0) continue;
                float _v = data[i][j] == 3 || data[i][j] == 4 ? -0.5f : 0;
                if (data[i][j] == 2)
                    character = Instantiate(GetObject(data[i][j]), tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), GetObject(data[i][j]).transform.rotation, tileM.transform.parent);
                //character = Instantiate(GetObject(data[i][j]), new Vector3(i, Mathf.Abs(j - 10 - _v), 0), GetObject(data[i][j]).transform.rotation);
                else if (j != 0 && data[i][j] == 1 && data[i][j - 1] != 0)
                {
                    Instantiate(floor2, tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), floor2.transform.rotation, tileM.transform.parent);
                    //Instantiate(floor2, new Vector3(i, Mathf.Abs(j - 10 - _v), 0), floor2.transform.rotation);
                }
                else
                    Instantiate(GetObject(data[i][j]), tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), GetObject(data[i][j]).transform.rotation, tileM.transform.parent);
                //Instantiate(GetObject(data[i][j]), new Vector3(i, Mathf.Abs(j - 10 - _v), 0), GetObject(data[i][j]).transform.rotation);
            }
        }
        var vitrualCamera = Camera.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        vitrualCamera.m_Follow = character.transform;
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
