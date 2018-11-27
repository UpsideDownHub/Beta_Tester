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
    [SerializeField] GameObject serra;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject floor2;
    [SerializeField] GameObject LeftStair;
    [SerializeField] GameObject RightStair;
    [SerializeField] GameObject Character;
    [SerializeField] GameObject Camera;
    [SerializeField] Tilemap tileM;
    public static List<List<string>> data = new List<List<string>>();
    private string result = string.Empty;

    private void Start()
    {
        SetResult();
    }

    private void SetResult()
    {
        result = "0,0,0,2,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,1,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,1,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,0,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n0,0,0,0,0,0,1,0,0,0\r\n";

        var lines = result.Split('\n');

        List<string> line;
        foreach (var _line in lines)
        {
            var __line = _line.Replace("\r", string.Empty);
            line = new List<string>();
            var val = __line.Split(',');
            if (val.Count() < 10) continue;
            line.AddRange(val);
            data.Add(line);
        }

        GameObject character = null;
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = data[i].Count - 1; j >= 0; j--)
            {
                var prevData = data[i][j - 1].IndexOf("-") != -1 ? int.Parse(data[i][j - 1].Split('-')[0]) : int.Parse(data[i][j - 1]);
                int _data;
                int? trap = null;
                if (data[i][j].IndexOf("-") != -1)
                {
                    _data = int.Parse(data[i][j].Split('-')[0]);
                    trap = int.Parse(data[i][j].Split('-')[1]);
                }
                else
                {
                    _data = int.Parse(data[i][j]);
                }

                if (_data == 0 && !trap.HasValue) continue;
                float _v = _data == 3 || _data == 4 ? -0.5f : 0;
                //var _data = _data.
                if (_data == 2)
                    character = Instantiate(GetObject(_data), tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), GetObject(_data).transform.rotation, tileM.transform.parent);
                //character = Instantiate(GetObject(_data), new Vector3(i, Mathf.Abs(j - 10 - _v), 0), GetObject(_data).transform.rotation);
                else if (j != 0 && _data == 1 && prevData != 0)
                {
                    Instantiate(floor2, tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), floor2.transform.rotation, tileM.transform.parent);
                    //Instantiate(floor2, new Vector3(i, Mathf.Abs(j - 10 - _v), 0), floor2.transform.rotation);
                }
                else
                    Instantiate(GetObject(_data), tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), GetObject(_data).transform.rotation, tileM.transform.parent);
                //Instantiate(GetObject(_data), new Vector3(i, Mathf.Abs(j - 10 - _v), 0), GetObject(_data).transform.rotation);

                if (trap.HasValue)
                {
                    Instantiate(GetObject(trap.Value), tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), GetObject(trap.Value).transform.rotation, tileM.transform.parent);
                }
            }
        }
        var vitrualCamera = Camera.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        vitrualCamera.m_Follow = character.transform;
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
            case 5:
                return flag;
            case 6:
                return serra;
            default:
                return floor;
        }
    }
}
