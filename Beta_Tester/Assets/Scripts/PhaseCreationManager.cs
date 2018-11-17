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

public class PhaseCreationManager : MonoBehaviour
{
    [SerializeField] GameObject serra;
    [SerializeField] GameObject lava;
    [SerializeField] GameObject montanha1;
    [SerializeField] GameObject montanha2;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject panelContent;
    [SerializeField] GameObject panelItem;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject floor2;
    [SerializeField] GameObject LeftStair;
    [SerializeField] GameObject RightStair;
    [SerializeField] GameObject Character;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject Canvas2;
    [SerializeField] GameObject Camera;
    [SerializeField] Tilemap tileM;
    private List<Assets.Scripts.DAL.Phase> phases = new List<Assets.Scripts.DAL.Phase>();

    UnityAction ua;
    public static List<List<string>> data = new List<List<string>>();
    private Dictionary<GameObject, string> Fileids = new Dictionary<GameObject, string>();

    private string result = string.Empty;

    void Start()
    {
        //GoogleDriveFiles.List().Send().OnDone += BuildResults;
    }

    void Update()
    {
    }

    public void StartBuild()
    {
        Assets.Scripts.DAL.BetaTesterContext.Phase.GetData();
        phases = Assets.Scripts.DAL.BetaTesterContext.Phase.Data.Where(x => x.UserId == Assets.Scripts.DAL.BetaTesterContext.UserId).ToList();
        BuildResults();
        //GoogleDriveFiles.List().Send().OnDone += BuildResults;
    }

    private void BuildResults() //UnityGoogleDrive.Data.FileList fileList
    {
        for (var i = 0; i < phases.Count; i++)
        {
            //if (fileList.Files[i].MimeType == "application/vnd.google-apps.folder") continue;

            var obj = GameObject.Instantiate(panelItem, panelContent.transform);
            var rect = obj.GetComponent<RectTransform>();
            var contentRect = panelContent.GetComponent<RectTransform>();
            var width = contentRect.rect.width / 1;
            var ratio = width / rect.rect.width;
            var height = rect.rect.height * ratio;

            var x = -contentRect.rect.width / 2 + width * (i % 1);
            var y = contentRect.rect.height / 2 - height * (1 + i);
            rect.offsetMin = new Vector2(x, y);

            x = rect.offsetMin.x + width;
            y = rect.offsetMin.y + height;

            rect.offsetMax = new Vector2(x, y);

            obj.GetComponentInChildren<Text>().text = phases[i].Name;//fileList.Files[i].Name;

            Fileids.Add(obj, phases[i].FileId);//fileList.Files[i].Id);

            obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { MountPhase(obj); });

        }
    }

    private void SetResult(UnityGoogleDrive.Data.File file)
    {
        result = Encoding.UTF8.GetString(file.Content);

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
                int prevData = 0;
                if (j != 0) 
                    prevData = data[i][j - 1].IndexOf("-") != -1 ? int.Parse(data[i][j - 1].Split('-')[0]) : int.Parse(data[i][j - 1]);
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
        Instantiate(lava, lava.transform.position, Quaternion.identity);
        Instantiate(lava, lava.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        Instantiate(montanha1, montanha1.transform.position, Quaternion.identity);
        Instantiate(montanha2, montanha2.transform.position, Quaternion.identity);

        var vitrualCamera = Camera.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        vitrualCamera.m_Follow = character.transform;
        Canvas.SetActive(false);
        Canvas2.SetActive(true);
    }

    void MountPhase(GameObject go)
    {
        string id = "";
        Fileids.TryGetValue(go, out id);
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogError("Houve um erro ao carregar o arquivo!");
            return;
        }

        GoogleDriveFiles.Download(id).Send().OnDone += SetResult;
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
