using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorSprite;
    
    private void Update()
    {
        Cursor.SetCursor(cursorSprite,Vector2.zero, CursorMode.Auto);
    }
}
