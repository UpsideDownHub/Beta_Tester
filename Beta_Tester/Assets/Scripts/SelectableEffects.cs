using UnityEngine;
using System.Collections;

public class SelectableEffects : MonoBehaviour
{
    public static bool isCursorInsideObject;

    private void OnMouseEnter()
    {
        isCursorInsideObject = true;
    }

    private void OnMouseExit()
    {
        isCursorInsideObject = false;
    }
}
