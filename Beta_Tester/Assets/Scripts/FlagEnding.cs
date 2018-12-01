using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class FlagEnding : MonoBehaviour
{
    GameObject player;
    GameObject walkSmoke;
    PlayerScript3D playerScript;
    AIPlayerScript _playerScript;
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;
    public Sprite victorySprite;
    Button backToLevelEditor;
    Text congratulations;
    bool isPhaseCompleted;

    GameObject obj;
    GameObject loadingText;
    GameObject canvas;
    GameObject gridObj;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 7)
        {
            player = GameObject.Find("Player(Clone)");
            if (player == null) player = GameObject.Find("_Player(Clone)");
            playerScript = player.GetComponent<PlayerScript3D>();
            _playerScript = player.GetComponent<AIPlayerScript>();
            playerAnimator = player.GetComponent<Animator>();
            walkSmoke = GameObject.Find("walksmoke");
            playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
            backToLevelEditor = GameObject.Find("BackToLevelEditorButton").GetComponent<Button>();
            congratulations = GameObject.Find("CongratulationsText").GetComponent<Text>();
        }
        else
        {
            //obj = GameObject.Find("obj");
            //loadingText = obj.transform.Find("LoadingText").parent.GetComponent<GameObject>();
            canvas = GameObject.Find("Canvas");
            gridObj = GameObject.Find("Grid");
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 7)
        {
            if (isPhaseCompleted)
            {
                if (playerScript != null)
                    playerScript.canGetDamage = false;
                if (_playerScript != null)
                    _playerScript.canGetDamage = false;
            }
        }
        else
        {
            //loadingText.transform.position = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect + 3.5f, loadingText.transform.position.y);
        }
    }

    public void BackToLevelEditor()
    {
        SceneManager.LoadScene(5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex != 7)
            {
                if (playerScript != null)
                    playerScript.speed = 0;
                if (_playerScript != null)
                    _playerScript.speed = 0;
                playerAnimator.enabled = false;
                walkSmoke.SetActive(false);
                playerSpriteRenderer.sprite = victorySprite;
                backToLevelEditor.interactable = true;
                congratulations.enabled = true;
                isPhaseCompleted = true;

                var phase = Assets.Scripts.DAL.BetaTesterContext.Phase.GetData().Single(x => x.FileId == Assets.Scripts.DAL.BetaTesterContext.FileId);
                if (!phase.Tested)
                    phase.Tested = true;
                phase.Completed++;

                Assets.Scripts.DAL.BetaTesterContext.Phase.Update(phase);

                GameObject.Find("Scripts").GetComponent<GameOver>().ended = true;
            }
            else
            {
                StartCoroutine(LoadAsynchronously(2));
            }
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex) //Tela de loading
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        gridObj.SetActive(false);
        canvas.SetActive(false);
        //loadingText.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
