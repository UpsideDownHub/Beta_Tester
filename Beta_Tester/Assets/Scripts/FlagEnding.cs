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
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;
    public Sprite victorySprite;
    Button backToLevelEditor;
    Text congratulations;
    bool isPhaseCompleted;

    private void Start()
    {
        player = GameObject.Find("_Player(Clone)");
        playerScript = player.GetComponent<PlayerScript3D>();
        playerAnimator = player.GetComponent<Animator>();
        walkSmoke = GameObject.Find("walksmoke");
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        backToLevelEditor = GameObject.Find("BackToLevelEditorButton").GetComponent<Button>();
        congratulations = GameObject.Find("CongratulationsText").GetComponent<Text>();
    }

    private void Update()
    {
        if (isPhaseCompleted)
            playerScript.canGetDamage = false;
    }

    public void BackToLevelEditor()
    {
        SceneManager.LoadScene(5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerScript.speed = 0;
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
    }
}
