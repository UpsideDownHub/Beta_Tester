using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityGoogleDrive;

public class PhaseCreationManager : MonoBehaviour
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

    UnityAction ua;
    List<List<int>> data = new List<List<int>>();
    private Dictionary<GameObject, string> Fileids = new Dictionary<GameObject, string>();

    private string result = string.Empty;

    void Start()
    {
        GoogleDriveFiles.List().Send().OnDone += BuildResults;
    }


    void Update()
    {
    }


    private void BuildResults(UnityGoogleDrive.Data.FileList fileList)
    {
        for (var i = 0; i < fileList.Files.Count; i++)
        {
            if (fileList.Files[i].MimeType == "application/vnd.google-apps.folder") continue;

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

            obj.GetComponentInChildren<Text>().text = fileList.Files[i].Name;

            Fileids.Add(obj, fileList.Files[i].Id);

            //ua += MountPhase;

            obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { MountPhase(obj); });

        }
    }

    private void SetResult(UnityGoogleDrive.Data.File file)
    {
        result = Encoding.UTF8.GetString(file.Content);

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
                    character = Instantiate(GetObject(data[i][j]), new Vector3(i, Mathf.Abs(j - 10 - _v), 0), GetObject(data[i][j]).transform.rotation);
                else if (data[i][j] == 1 && data[i][j - 1] != 0) {
                    Instantiate(floor2, new Vector3(i, Mathf.Abs(j - 10 - _v), 0), floor2.transform.rotation);
                }
                else
                    Instantiate(GetObject(data[i][j]), new Vector3(i, Mathf.Abs(j - 10 - _v), 0), GetObject(data[i][j]).transform.rotation);
            }
        }
        var vitrualCamera = Camera.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        vitrualCamera.m_Follow = character.transform;
        Canvas.SetActive(false);
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
            default:
                return floor;
        }
    }
}
