using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{
    public Texture2D pointer;
    public Texture2D click;

    private void Start()
    {
        Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Cutscenes.isPlayingCutscene)
        {
            Cursor.SetCursor(click, Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0) && !Cutscenes.isPlayingCutscene)
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }

        if (Cutscenes.isPlayingCutscene)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
