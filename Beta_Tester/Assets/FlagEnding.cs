using UnityEngine;
using System.Collections;

public class FlagEnding : MonoBehaviour
{
    GameObject player;
    AIPlayerScript playerScript;

    private void Start()
    {
        player = GameObject.Find("_Player(Clone)");
        playerScript = player.GetComponent<AIPlayerScript>();
    }

    private void Update()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            playerScript.speed = 0;
        }
    }
}
