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
    public GameObject loading;

    [SerializeField] GameObject totem_flecha;
    [SerializeField] GameObject poison;
    [SerializeField] GameObject totem;
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
    private List<Assets.Scripts.Models.PhasesIndexViewModel> phases = new List<Assets.Scripts.Models.PhasesIndexViewModel>();
    private List<GameObject> createdObjs = new List<GameObject>();

    UnityAction ua;
    public static List<List<Assets.Scripts.DAL.PhaseData>> data = new List<List<Assets.Scripts.DAL.PhaseData>>();
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
        GetRecentPhases();
        //GoogleDriveFiles.List().Send().OnDone += BuildResults;
    }

    private void BuildResults() //UnityGoogleDrive.Data.FileList fileList
    {
        GameOver.inGame = false;
        foreach (Transform child in panelContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

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

            obj.GetComponentInChildren<Text>().text = phases[i].Name + (phases[i].Tested ? "" : " (Não testado)");//fileList.Files[i].Name;

            Fileids.Add(obj, phases[i].FileId);//fileList.Files[i].Id);

            obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { MountPhase(obj); });

        }
        loading.SetActive(false);

    }

    private void SetResult(UnityGoogleDrive.Data.File file)
    {
        result = Encoding.UTF8.GetString(file.Content);
        data = new List<List<Assets.Scripts.DAL.PhaseData>>();
        createdObjs.ForEach(x => Destroy(x));

        var lines = result.Split('\n');

        List<Assets.Scripts.DAL.PhaseData> line;
        foreach (var _line in lines)
        {
            var __line = _line.Replace("\r", string.Empty);
            line = new List<Assets.Scripts.DAL.PhaseData>();
            var val = __line.Split('_');
            if (val.Count() < 10) continue;

            for (var i = 0; i < 10; i++)
                line.Add(JsonUtils.FromJsonPrivateCamel<Assets.Scripts.DAL.PhaseData>(val[i]));

            data.Add(line);
        }
        GameObject character = null;
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = data[i].Count - 1; j >= 0; j--)
            {
                int prevData = 0;
                if (j != 0)
                    prevData = data[i][j - 1].Block;
                int _data = data[i][j].Block;
                int? trap = data[i][j].Trap;

                if (_data == 0 && !trap.HasValue) continue;

                if (_data != 0)
                {
                    float _v = _data == 3 || _data == 4 ? -0.5f : 0;
                    if (_data == 2)
                    {
                        character = Instantiate(GetObject(_data), tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), GetObject(_data).transform.rotation, tileM.transform.parent);
                        createdObjs.Add(character);
                    }
                    else if (j != 0 && _data == 1 && prevData != 0 && prevData != 2)
                    {
                        createdObjs.Add(Instantiate(floor2, tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), floor2.transform.rotation, tileM.transform.parent));
                    }
                    else
                        createdObjs.Add(Instantiate(GetObject(_data), tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), GetObject(_data).transform.rotation, tileM.transform.parent));
                }

                if (trap.HasValue)
                {
                    var obj = Instantiate(GetObject(trap.Value), tileM.GetCellCenterLocal(new Vector3Int(i, Mathf.Abs(j - 10), 0)), GetObject(trap.Value).transform.rotation, tileM.transform.parent);
                    createdObjs.Add(obj);
                    obj.transform.position = new Vector3(obj.transform.position.x + (trap.Value == 8 ? .5f : 0), obj.transform.position.y + (trap.Value == 8 ? 1.3f : 0));
                    var sr = obj.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.flipX = data[i][j].HFlipTrap;
                        sr.flipY = data[i][j].VFlipTrap;
                    }
                    var cSr = obj.GetComponentInChildren<SpriteRenderer>();
                    if (cSr != null)
                    {
                        cSr.flipX = data[i][j].HFlipTrap;
                        cSr.flipY = data[i][j].VFlipTrap;
                    }
                }
            }
        }

        for (var i = 1; i <= 3; i++)
            Instantiate(montanha1, new Vector3(- (i * 8), 0), Quaternion.identity);

        for (var i = 0; i < Mathf.RoundToInt(data.Count / 8) + 3; i++)
            Instantiate(montanha1, new Vector3(i * 8, 0), Quaternion.identity);
        

        Instantiate(lava, lava.transform.position, Quaternion.identity);
        Instantiate(lava, lava.transform.position + new Vector3(0, 2, 0), Quaternion.identity);

        //MONTATNHA - 9
        //Instantiate(montanha1, montanha1.transform.position, Quaternion.identity);
        //Instantiate(montanha2, montanha2.transform.position, Quaternion.identity);

        var vitrualCamera = Camera.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        vitrualCamera.m_Follow = character.transform;
        Canvas.SetActive(false);
        Canvas2.SetActive(true);
        loading.SetActive(false);
        Assets.Scripts.PlayerAttrs.life = 5;
        Assets.Scripts.PhaseCreationTimeManager.time = Mathf.RoundToInt(data.Count * 2.5f); 
        Assets.Scripts.PhaseCreationTimeManager.timeCount.CoolDown = Mathf.RoundToInt(data.Count * 2.5f); 
        GameObject.Find("Scripts").GetComponent<GameOver>().ended = false;
        GameOver.inGame = true;
    }

    void MountPhase(GameObject go)
    {
        loading.SetActive(true);

        string id = "";
        Fileids.TryGetValue(go, out id);
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogError("Houve um erro ao carregar o arquivo!");
            return;
        }

        Assets.Scripts.DAL.BetaTesterContext.FileId = id;

        var phase = Assets.Scripts.DAL.BetaTesterContext.Phase.GetData().Single(x => x.FileId == Assets.Scripts.DAL.BetaTesterContext.FileId);
        phase.Played++;

        Assets.Scripts.DAL.BetaTesterContext.Phase.Update(phase);

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
            case 7:
                return totem;
            case 8:
                return poison;
            case 9:
                return totem_flecha;
            default:
                return floor;
        }
    }

    public void GetRecentPhases()
    {
        loading.SetActive(true);
        var UserId = Assets.Scripts.DAL.BetaTesterContext.UserId;
        phases = (from y in Assets.Scripts.DAL.BetaTesterContext.PhasesIndexView.GetData()
                  join upf in Assets.Scripts.DAL.BetaTesterContext.UserPhaseFav.GetData() on new { y.PhaseId, UserId } equals new { upf.PhaseId, upf.UserId } into up
                  join pr in Assets.Scripts.DAL.BetaTesterContext.PhaseRate.GetData() on new { y.PhaseId, UserId } equals new { pr.PhaseId, pr.UserId } into _pr
                  from upf in up.DefaultIfEmpty()
                  from pr in _pr.DefaultIfEmpty()
                  where y.Tested
                  orderby y.PhaseId descending
                  select new Assets.Scripts.Models.PhasesIndexViewModel
                  {
                      Completed = y.Completed,
                      UserId = y.UserId,
                      Dies = y.Dies,
                      FileId = y.FileId,
                      Name = y.Name,
                      PhaseId = y.PhaseId,
                      Played = y.Played,
                      Rating = y.Rating,
                      UserRate = pr != null ? pr.Rate : 0,
                      Tested = y.Tested,
                      Fav = upf == null ? false : true
                  }).Take(10).ToList();

        BuildResults();
    }

    public void GetFavPhases()
    {
        loading.SetActive(true);
        var UserId = Assets.Scripts.DAL.BetaTesterContext.UserId;
        phases = (from y in Assets.Scripts.DAL.BetaTesterContext.PhasesIndexView.GetData()
                  join upf in Assets.Scripts.DAL.BetaTesterContext.UserPhaseFav.GetData() on y.PhaseId equals upf.PhaseId
                  where y.UserId == UserId && y.Tested
                  orderby y.PhaseId descending
                  select new Assets.Scripts.Models.PhasesIndexViewModel
                  {
                      Completed = y.Completed,
                      UserId = y.UserId,
                      Dies = y.Dies,
                      FileId = y.FileId,
                      Name = y.Name,
                      PhaseId = y.PhaseId,
                      Played = y.Played,
                      Rating = y.Rating,
                      Tested = y.Tested
                  }).ToList();

        BuildResults();
    }

    public void GetMyPhases()
    {
        loading.SetActive(true);
        var UserId = Assets.Scripts.DAL.BetaTesterContext.UserId;
        phases = (from y in Assets.Scripts.DAL.BetaTesterContext.PhasesIndexView.GetData()
                  where y.UserId == UserId
                  orderby y.PhaseId descending
                  select new Assets.Scripts.Models.PhasesIndexViewModel
                  {
                      Completed = y.Completed,
                      UserId = y.UserId,
                      Dies = y.Dies,
                      FileId = y.FileId,
                      Name = y.Name,
                      PhaseId = y.PhaseId,
                      Played = y.Played,
                      Rating = y.Rating,
                      Tested = y.Tested
                  }).ToList();

        BuildResults();
    }
}
