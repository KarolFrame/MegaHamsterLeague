using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D[] spriteMouse;
    Vector2 hotcursor = new Vector2(0,0);
    
    public void BotonSeleccion()
    {
        Cursor.SetCursor(spriteMouse[0], hotcursor, CursorMode.Auto);
    }
    public void SobreBoton()
    {
        Cursor.SetCursor(spriteMouse[1], hotcursor, CursorMode.Auto);
    }
    public void MouseMirilla()
    {
        Cursor.SetCursor(spriteMouse[2], hotcursor, CursorMode.Auto);
    }
    public void MouseMirillaSinVida()
    {
        Cursor.SetCursor(spriteMouse[3], hotcursor, CursorMode.Auto);
    }
}
