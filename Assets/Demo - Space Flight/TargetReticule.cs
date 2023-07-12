using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReticule : MonoBehaviour
{
    public Texture2D target;

    void Start()
    {
        Cursor.SetCursor(target, new Vector2(32f,32f), CursorMode.Auto);
    }
}
