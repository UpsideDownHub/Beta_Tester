using UnityEngine;
using System.Collections;

public class FlagEnding : MonoBehaviour
{
    GameObject player;
    GameObject walkSmoke;
    AIPlayerScript playerScript;
    Animator playerAnimator;

    private void Start()
    {
        player = GameObject.Find("_Player(Clone)");
        playerScript = player.GetComponent<AIPlayerScript>();
        playerAnimator = player.GetComponent<Animator>();
        walkSmoke = GameObject.Find("walksmoke");
    }

    private void Update()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            playerScript.speed = 0;
            playerAnimator.enabled = false;
            walkSmoke.SetActive(false);
        }
    }
}
