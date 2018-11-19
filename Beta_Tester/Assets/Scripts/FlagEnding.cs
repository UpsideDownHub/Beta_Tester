using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlagEnding : MonoBehaviour
{
    GameObject player;
    GameObject walkSmoke;
    AIPlayerScript playerScript;
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;
    public Sprite victorySprite;
    Button backToLevelEditor;
    Text congratulations;

    private void Start()
    {
        player = GameObject.Find("_Player(Clone)");
        playerScript = player.GetComponent<AIPlayerScript>();
        playerAnimator = player.GetComponent<Animator>();
        walkSmoke = GameObject.Find("walksmoke");
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        backToLevelEditor = GameObject.Find("BackToLevelEditorButton").GetComponent<Button>();
        congratulations = GameObject.Find("CongratulationsText").GetComponent<Text>();
    }

    public void BackToLevelEditor()
    {
        SceneManager.LoadScene(7);
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
        }
    }
}
